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
        public Task<string> GetApplicationStatusNameByIdAsync(int id);
    }
}
