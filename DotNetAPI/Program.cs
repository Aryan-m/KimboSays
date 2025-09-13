using DataRepo.Models;
using DataRepo.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get connection string from config
            var connectionString = builder.Configuration.GetConnectionString("KimboTasksDb");

            // Register DbContext
            builder.Services.AddDbContext<KimboTasksDbContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    )
                )
            );

            builder.Services.AddScoped<IKimboTaskSvc, KimboTaskSvc>();
            builder.Services.AddScoped<ITaskEffortSvc, TaskEffortSvc>();

            builder.Services.AddAuthorization();

            string UIBaseAddress = builder.Configuration["UIClient:BaseUrl"]!;
            string CorsPolicyName = "AllowUIClient";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder => builder.WithOrigins(UIBaseAddress)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors(CorsPolicyName);

            // Global Exception Handling
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
                    if (exception != null)
                    {
                        context.Response.StatusCode = exception is ArgumentException || exception is KeyNotFoundException ? 400 : 500;
                        await context.Response.WriteAsJsonAsync(new { error = exception.Message });
                    }
                });
            });

            // --- Minimal API Endpoints ---

            string TaskEndPointURL = "/kimbotasks";

            // Get all tasks
            app.MapGet(TaskEndPointURL, async (IKimboTaskSvc svc) =>
            {
                var tasks = await svc.GetAllTasksAsync();
                return Results.Ok(tasks);
            });

            // Get task by id
            app.MapGet("{EndPointURL}/{id:int}", async (int id, IKimboTaskSvc svc) =>
            {
                var task = await svc.GetTaskByIdAsync(id);
                return task is not null ? Results.Ok(task) : Results.NotFound();
            });

            // Create new task
            app.MapPost(TaskEndPointURL, async (KimboTask task, IKimboTaskSvc svc) =>
            {
                task.DateAdded = DateOnly.FromDateTime(DateTime.UtcNow);

                await svc.AddTaskAsync(task);
                return Results.Created($"{TaskEndPointURL}/{task.Id}", task);
            });

            // Update task
            app.MapPut("{EndPointURL}", async (KimboTask updatedTask, IKimboTaskSvc svc) =>
            {
                var existingTask = await svc.GetTaskByIdAsync(updatedTask.Id);
                if (existingTask is null) return Results.NotFound();

                // Copy values from updatedTask into the tracked existingTask
                existingTask.Priority = updatedTask.Priority;
                existingTask.Task = updatedTask.Task;
                existingTask.Description = updatedTask.Description;
                existingTask.EffortId = updatedTask.EffortId;

                // Save
                await svc.UpdateTaskAsync(existingTask);

                return Results.NoContent();
            });

            // Delete task
            app.MapDelete("{EndPointURL}/{id:int}", async (int id, IKimboTaskSvc svc) =>
            {
                var task = await svc.GetTaskByIdAsync(id);
                if (task is null) return Results.NotFound();

                await svc.DeleteTaskAsync(id);
                return Results.NoContent();
            });

            string EffortEndPointURL = "/effortoptions";

            app.MapGet(EffortEndPointURL, async (ITaskEffortSvc svc) =>
            {
                var effortOptions = await svc.GetAll();
                return Results.Ok(effortOptions);
            });

            app.Run();
        }
    }
}
