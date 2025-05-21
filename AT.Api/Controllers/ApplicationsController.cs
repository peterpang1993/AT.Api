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
        //getall
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetApplicationDTO>>> GetAll()
        {
            var result = await applicationService.GetAllApplicationAsync();
            return Ok(result);
        }

        //get by id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetApplicationDTO>> GetById([FromRoute] int id)
        {
            var result = await applicationService.GetApplicationByIdAsync(id);
            return Ok(result);
        }

        //create application
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

        //update application
        [HttpPatch]
        [Route("{id}/ApplicationStatus")]
        public async Task<ActionResult<GetApplicationDTO>> UpdateApplicationStatus([FromRoute] int id, [FromBody] UpdateApplicationStatusDTO dto)
        {
            var result = await applicationService.UpdateApplicationStatusAsync(id, dto);
            return Ok(result);
        }        
    }
}
