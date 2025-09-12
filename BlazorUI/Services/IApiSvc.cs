using BlazorUI.Models;

namespace BlazorUI.Services
{
    public interface IApiSvc
    {
        Task<KimboTask> AddTaskAsync(KimboTask newTask);
        Task DeleteTaskAsync(int id);
        Task<List<KimboTask>> GetAllTasksAsync();
        Task<List<TaskEffort>> GetEffortOptionsAsync();
        Task<KimboTask> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(KimboTask updatedTask);
    }
}