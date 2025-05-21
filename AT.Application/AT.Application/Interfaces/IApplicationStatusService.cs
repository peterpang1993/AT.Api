using AT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.Interfaces
{
    public interface IApplicationStatusService 
    {
        public Task<IEnumerable<string>> GetApplicationStatusNamesAsync();
        public Task<ApplicationStatus> GetApplicationStatusByNameAsync(string name);
    }
}
