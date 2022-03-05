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
    public class NotificationController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly INotificationService notificationService;
        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            this.notificationService = notificationService;
            this.logger = logger;
        }
        [HttpPost("Create-Notification")]
        public IActionResult CreateNotification([FromBody] NotificationDTO notificationDTO)
        {
            try
            {
                var response = notificationService.CreateNotification(notificationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Create Notification unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Get-Notification/{id}")]
        public async Task<IActionResult> GetCompanyNotification(Guid id)
        {
            try
            {
                var respose = await notificationService.GetNotification(id);
                if (respose == null)
                {
                    return NotFound("No result found");
                }
                return Ok(respose);
            }
            catch (Exception ex)
            {
                logger.LogError("Get Notification unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
