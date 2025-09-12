using DataRepo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepo.Services
{
    public class TaskEffortSvc : ITaskEffortSvc
    {
        private readonly KimboTasksDbContext _context;

        public TaskEffortSvc(KimboTasksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEffort>> GetAll() =>
            await _context.TaskEfforts.AsNoTracking().ToListAsync();
    }
}
