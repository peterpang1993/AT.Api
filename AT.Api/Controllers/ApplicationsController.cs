using AT.Application.DTOs;
using AT.Application.DTOs.Validators;
using AT.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController(IApplicationService applicationService, 
        IApplicationStatusService applicationStatusService) 
        : ControllerBase
    {        
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetApplicationDTO>>> GetAll([FromQuery]PagingParameter pagingParameter)
        {
            var validator = new PagingParameterValidator();

            var validationResult = await validator.ValidateAsync(pagingParameter);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await applicationService.GetAllApplicationAsync(pagingParameter.Page, pagingParameter.PageSize);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetApplicationDTO>> GetById([FromRoute] int id)
        {
            var result = await applicationService.GetApplicationByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<GetApplicationDTO>> CreateApplication([FromBody] CreateApplicationDTO dto)
        {
            var validator = new CreateApplicationDTOValidator(applicationStatusService);

            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await applicationService.CreateApplicationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        
        [HttpPatch]
        [Route("{id}/ApplicationStatus")]
        public async Task<ActionResult<GetApplicationDTO>> UpdateApplicationStatus([FromRoute] int id, [FromBody] UpdateApplicationStatusDTO dto)
        {
            var result = await applicationService.UpdateApplicationStatusAsync(id, dto);
            return Ok(result);
        }        
    }
}
