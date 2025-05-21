using AT.Application.DTOs;
using AT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.Convertors
{
    public static class ApplicationConvertor
    {
        public static GetApplicationDTO ConvertToGetApplicationDTO(this AT.Domain.Entities.Application application)
        {
            return new GetApplicationDTO
            {
                Id = application.Id,
                ApplicationStatus = application.ApplicationStatus.ToString(),
                Company = application.Company,
                DateApplied = application.DateApplied,
                Position = application.Position

            };
        }
    }
}
