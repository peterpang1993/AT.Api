using AT.Application.Convertors;
using AT.Application.DTOs;
using AT.Application.Exceptions;
using AT.Application.Interfaces;
using AT.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationStatusRepository _applicationStatusRepository;

        public ApplicationService(IApplicationRepository applicationRepository,
            IApplicationStatusRepository applicationStatusRepository)
        {
            _applicationRepository = applicationRepository;
            _applicationStatusRepository = applicationStatusRepository;
        }

        public async Task<GetApplicationDTO> GetApplicationByIdAsync(int id)
        {
            var applicationItem = await _applicationRepository.GetApplicationWithApplicationStatusByIdAsync(id);

            if (applicationItem is null)
                throw new NotFoundException($"Application with Id {id} does not exist");

            return applicationItem.ConvertToGetApplicationDTO();
        }
        public async Task<IEnumerable<GetApplicationDTO>> GetAllApplicationAsync()
        {
            var applicationItemList = await _applicationRepository.GetAllApplicationWithApplicationStatusAsync();

            return applicationItemList.Select(x => x.ConvertToGetApplicationDTO());
        }
                
        public async Task<GetApplicationDTO> CreateApplicationAsync(CreateApplicationDTO dto)
        {
            var status = (await _applicationStatusRepository.GetAsync(x => x.ApplicationStatusName == dto.ApplicationStatus)).FirstOrDefault();

            if (status is null)
                throw new NotFoundException($"Application Status with name {dto.ApplicationStatus} does not exist.");

            Domain.Entities.Application application = AT.Domain.Entities.Application
                .Create(dto.Company, dto.Position, dto.DateApplied, status);

            await _applicationRepository.AddAsync(application);

            return application.ConvertToGetApplicationDTO();
        }

        public async Task<GetApplicationDTO> UpdateApplicationStatusAsync(int id, UpdateApplicationStatusDTO dto)
        {
            var application = await _applicationRepository.GetApplicationWithApplicationStatusByIdAsync(id);

            if(application is null)
                throw new NotFoundException($"Application with Id {id} does not exist");

            var status = (await _applicationStatusRepository.GetAsync(x => x.ApplicationStatusName == dto.ApplicationStatus)).FirstOrDefault();

            if (status is null)
                throw new NotFoundException($"Application Status with name {dto.ApplicationStatus} does not exist.");

            application.UpdateApplicationStatus(status);
            await _applicationRepository.UpdateAsync(application);

            return application.ConvertToGetApplicationDTO();
        }
    }
}
