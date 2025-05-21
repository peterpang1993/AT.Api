using AT.Application.Interfaces;
using AT.Domain.Entities;
using AT.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AT.Application.DTOs.Validators
{
    public class CreateApplicationDTOValidator : AbstractValidator<CreateApplicationDTO>
    {        
        public CreateApplicationDTOValidator(IApplicationStatusService applicationStatusService)
        {            
            RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Must not be empty.")
                .MaximumLength(100).WithMessage("Must not exceed 100 characters");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Must not be empty.")
                .MaximumLength(100).WithMessage("Must not exceed 100 characters");

            RuleFor(x => x.DateApplied)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Cannot be in the future.");

            RuleFor(x => x.ApplicationStatus)
                .NotEmpty().WithMessage("Must not be empty.")
                .MustAsync(async (x, _, context) =>
                {
                    var statuses = await applicationStatusService.GetApplicationStatusNamesAsync();
                    return statuses.Contains(x.ApplicationStatus);

                }).WithMessage("Invalid application Status");
        }

    }
}
