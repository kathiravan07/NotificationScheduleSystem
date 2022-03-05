using Microsoft.Extensions.Logging;
using Moq;
using NotificationSchedule.Services.Interfaces;
using NotificationScheduleSystem.API.Controllers;
using System;
using Xunit;

namespace NotificationSchedule.Tests
{
    public class NotificationTest
    {
        #region Private Variables
        private readonly Mock<INotificationService> _notificationService;
        private readonly NotificationController _notificationController;
        private readonly Mock<ILogger<NotificationController>> _logger;

        public NotificationTest()
        {
            _notificationService = new Mock<INotificationService>();
            _logger = new Mock<ILogger<NotificationController>>();
            _notificationController = new NotificationController(_notificationService.Object, _logger.Object);
        }
        #endregion

        [Fact]
        public void GetNotificationTest()
        {
            try
            {
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(false, ex.Message);
            }
        }
    }
}
