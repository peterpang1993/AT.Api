using AT.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationStatusController(IApplicationStatusService applicationStatusService) : ControllerBase
    {
        //update application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllApplicationStatuses()
        {
            var result = await applicationStatusService.GetApplicationStatusNamesAsync();
            return Ok(result);
        }
    }
}
