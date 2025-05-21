using AT.Application.Convertors;
using AT.Application.DTOs;
using AT.Application.Exceptions;
using AT.Application.Extensions;
using AT.Application.Interfaces;
using AT.Domain.Entities;
using AT.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly IApplicationStatusService _applicationStatusService;

        public ApplicationService(IApplicationRepository applicationRepository,
            IApplicationStatusService applicationStatusService)
        {
            _applicationRepository = applicationRepository;
            _applicationStatusService = applicationStatusService;
        }

        public async Task<GetApplicationDTO> GetApplicationByIdAsync(int id)
        {
            var applicationItem = await _applicationRepository.GetApplicationWithApplicationStatusByIdAsync(id);

            if (applicationItem is null)
                throw new NotFoundException($"Application with Id {id} does not exist");

            return applicationItem.ConvertToGetApplicationDTO();
        }

        public async Task<PaginatedResult<GetApplicationDTO>> GetAllApplicationAsync(int page, int pageSize)
        {
            IQueryable<Domain.Entities.Application> allApplicationQuery = _applicationRepository.GetAllApplicationWithApplicationStatus();

            PaginatedResult<GetApplicationDTO> result = await allApplicationQuery.GetPagedAsync(page, pageSize, x => x.ConvertToGetApplicationDTO());           

            return result;
        }

        public async Task<GetApplicationDTO> CreateApplicationAsync(CreateApplicationDTO dto)
        {
            ApplicationStatus status = (await _applicationStatusService.GetApplicationStatusByNameAsync(dto.ApplicationStatus));

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

            var status = await _applicationStatusService.GetApplicationStatusByNameAsync(dto.ApplicationStatus);

            if (status is null)
                throw new NotFoundException($"Application Status with name {dto.ApplicationStatus} does not exist.");

            application.UpdateApplicationStatus(status);
            await _applicationRepository.UpdateAsync(application);

            return application.ConvertToGetApplicationDTO();
        }
    }
}
