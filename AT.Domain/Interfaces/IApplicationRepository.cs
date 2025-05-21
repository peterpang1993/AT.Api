using AT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Domain.Interfaces
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        public IQueryable<Application> GetAllApplicationWithApplicationStatus();
        public Task<Application?> GetApplicationWithApplicationStatusByIdAsync(int id);
        public Task<IEnumerable<Application>> GetAllApplicationWithApplicationStatusAsync();
    }
}
