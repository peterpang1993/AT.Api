using AT.Application.Exceptions;
using AT.Application.Interfaces;
using AT.Domain.Entities;
using AT.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AT.Application.Services
{
    public class ApplicationStatusService : IApplicationStatusService
    {        
        private readonly IApplicationStatusRepository _applicationStatusRepository;        

        public ApplicationStatusService(IApplicationStatusRepository applicationStatusRepository)
        {
            _applicationStatusRepository = applicationStatusRepository;
        }        

        public async Task<IEnumerable<string>> GetApplicationStatusNamesAsync()
        {
            var statuses = await _applicationStatusRepository.GetAllAsync();
            return statuses.Select(s => s.ApplicationStatusName).ToList();
        }

        public async Task<ApplicationStatus> GetApplicationStatusByNameAsync(string name)
        {
            var status = (await _applicationStatusRepository.GetAsync(x => x.ApplicationStatusName == name)).FirstOrDefault();

            if (status is null)
                throw new NotFoundException($"Application status with name :{name} does not exist.");

            return status;
        }
    }
}
