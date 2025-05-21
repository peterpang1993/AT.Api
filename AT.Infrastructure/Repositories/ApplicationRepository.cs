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
        public new readonly ATDbContext _dbContext;
        public ApplicationRepository(ATDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Application?> GetApplicationWithApplicationStatusByIdAsync(int id)
        {
            return await _dbContext.Applications.Include(x => x.ApplicationStatus).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Application>> GetAllApplicationWithApplicationStatusAsync()
        {
            return await _dbContext.Applications.Include(x => x.ApplicationStatus).ToListAsync();
        }
    }
}
