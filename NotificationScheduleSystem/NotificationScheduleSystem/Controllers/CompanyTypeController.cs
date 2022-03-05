using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationSchedule.Contracts.DTO;
using NotificationSchedule.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace NotificationScheduleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTypeController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ICompanyTypeService companyTypeService;
        public CompanyTypeController(ICompanyTypeService companyTypeService, ILogger<CompanyTypeController> logger)
        {
            this.companyTypeService = companyTypeService;
            this.logger = logger;
        }

        [HttpGet("GetAll-CompanyType")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetAllCompanyType()
        {
            try
            {

                var response = await companyTypeService.GetAllCompanyType();
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Get CompanyType unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("Create-CompanyType")]
        public async Task<IActionResult> CreateCompanyType([FromBody] CompanyTypeDTO companyTypeDTO)
        {
            try
            {
                var response = await companyTypeService.CreateCompanyType(companyTypeDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Create CompanyType unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("Update-CompanyType")]
        public async Task<IActionResult> UpdateCompanyType([FromBody] CompanyTypeDTO companyTypeDTO)
        {
            try
            {
                var response = await companyTypeService.CreateCompanyType(companyTypeDTO);

                if (response == "Company Type Not Fount")
                {
                    return NotFound("No result found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Update CompanyType unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
