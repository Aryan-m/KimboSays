using DataRepo.Models;

namespace DataRepo.Services
{
    public interface IKimboTaskSvc
    {
        Task AddTaskAsync(KimboTask task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<KimboTask>> GetAllTasksAsync();
        Task<KimboTask?> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(KimboTask task);
    }
}