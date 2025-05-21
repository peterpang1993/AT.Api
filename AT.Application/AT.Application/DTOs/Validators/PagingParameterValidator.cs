using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.DTOs.Validators
{
    public class PagingParameterValidator : AbstractValidator<PagingParameter>
    {
        public PagingParameterValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Must be greater than 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Must be less than or equal to 100.");
        }
    }
}
