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

            // Ensure folder exists
            var dbFolder = Path.GetDirectoryName(connectionString!.Replace("Data Source=", "").Trim());
            if (!Directory.Exists(dbFolder))
                throw new Exception("Specified Database folder from the connection string does not exist.");

            // Register DbContext
            builder.Services.AddDbContext<KimboTasksDbContext>(options =>
                options.UseSqlite(connectionString));

            builder.Services.AddScoped<IKimboTaskSvc, KimboTaskSvc>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

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

            string EndPointURL = "/kimbotasks";

            // Get all tasks
            app.MapGet(EndPointURL, async (IKimboTaskSvc svc) =>
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
            app.MapPost(EndPointURL, async (KimboTask task, IKimboTaskSvc svc) =>
            {
                await svc.AddTaskAsync(task);
                return Results.Created($"{EndPointURL}/{task.Id}", task);
            });

            // Update task
            app.MapPut("{EndPointURL}/{id:int}", async (int id, KimboTask updatedTask, IKimboTaskSvc svc) =>
            {
                var existingTask = await svc.GetTaskByIdAsync(id);
                if (existingTask is null) return Results.NotFound();

                updatedTask.Id = id;
                await svc.UpdateTaskAsync(updatedTask);
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


            app.Run();
        }
    }
}
