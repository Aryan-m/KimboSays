using DataRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace DataRepo.Services
{
    public class KimboTaskSvc : IKimboTaskSvc
    {
        private readonly KimboTasksDbContext _context;

        public KimboTaskSvc(KimboTasksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KimboTask>> GetAllTasksAsync() =>
            await _context.KimboTasks.Include(t => t.Effort).OrderBy(t => t.DateAdded).AsNoTracking().ToListAsync();

        public async Task<KimboTask?> GetTaskByIdAsync(int id) =>
            await _context.KimboTasks.FindAsync(id);

        public async Task AddTaskAsync(KimboTask task)
        {
            _context.KimboTasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(KimboTask task)
        {
            _context.KimboTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.KimboTasks.FindAsync(id);
            if (task != null)
            {
                _context.KimboTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

    }
}
