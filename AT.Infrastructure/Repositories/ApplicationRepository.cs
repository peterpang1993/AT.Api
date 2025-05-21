using AT.Domain.Entities;
using AT.Domain.Interfaces;
using AT.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Infrastructure.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {        
        public ApplicationRepository(ATDbContext dbContext) : base(dbContext)
        {            
        }

        public IQueryable<Application> GetAllApplicationWithApplicationStatus()
        {
            return _dbContext.Applications.Include(x => x.ApplicationStatus);
        }

        public async Task<Application?> GetApplicationWithApplicationStatusByIdAsync(int id)
        {
            return await GetAllApplicationWithApplicationStatus().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Application>> GetAllApplicationWithApplicationStatusAsync()
        {
            return await GetAllApplicationWithApplicationStatus().ToListAsync();
        }
    }
}
