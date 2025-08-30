using DataRepo.Models;

namespace DataRepo.Services
{
    public interface IKimboTaskSvc
    {
        Task AddTaskAsync(KimboTask task);
        Task DeleteTaskAsync(int id);
        Task<List<KimboTask>> GetAllTasksAsync();
        Task<KimboTask?> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(KimboTask task);
    }
}