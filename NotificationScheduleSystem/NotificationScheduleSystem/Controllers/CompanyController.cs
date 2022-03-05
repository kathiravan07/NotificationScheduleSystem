using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationSchedule.Contracts.DTO;
using NotificationSchedule.Services.Interfaces;


namespace NotificationScheduleSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger logger;

        private readonly ICompanyService companyService;
        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            this.companyService = companyService;
            this.logger = logger;
        }
        /// <summary>
        /// GetCompanyInformation
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>Company Service</returns>
        [HttpGet("Get-Company/{companyId}")]
        public IActionResult GetCompanyInformation([FromRoute] Guid companyId)
        {
            try
            {
                var response = companyService.GetCompany(companyId);
                if (response == null)
                {
                    return NotFound("No result found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Get Company unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="companyDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /create-company
        ///     {
        ///         "companyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "companyName": "XYZ",
        ///         "companyNumber": 1234567890,
        ///         "companyTypeId": 1,
        ///         "marketId": 1,
        ///         "creationDate": "2022-03-05"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [Produces("application/json")]
        [HttpPost]
        [Route("Create-Company")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(CompanyDTO))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                var response = await companyService.CompanyCreate(companyDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Create Company unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
