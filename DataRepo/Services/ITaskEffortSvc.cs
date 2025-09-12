using DataRepo.Models;

namespace DataRepo.Services
{
    public interface ITaskEffortSvc
    {
        Task<IEnumerable<TaskEffort>> GetAll();
    }
}