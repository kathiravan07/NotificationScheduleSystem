using Microsoft.Extensions.Logging;
using Moq;
using NotificationSchedule.Contracts.DTO;
using NotificationSchedule.Services.Interfaces;
using NotificationScheduleSystem.API.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NotificationSchedule.Tests
{
    public class CompanyTypeTest
    {
        #region Private Variables
        private readonly Mock<ICompanyTypeService> _companyTypeService;
        private readonly CompanyTypeController _companyController;
        private readonly Mock<ILogger<CompanyTypeController>> _logger;

        public CompanyTypeTest()
        {
            _companyTypeService = new Mock<ICompanyTypeService>();
            _logger = new Mock<ILogger<CompanyTypeController>>();
            _companyController = new CompanyTypeController(_companyTypeService.Object, _logger.Object);
        }
        #endregion

        #region Mock Objects
        List<CompanyTypeDTO> companyTypeList = new()
        {
            new CompanyTypeDTO()
            {
                CompanyTypeId = 1,
                Type = "small"
            },
            new CompanyTypeDTO()
            {
                CompanyTypeId = 2,
                Type = "medium"
            },
            new CompanyTypeDTO()
            {
                CompanyTypeId = 3,
                Type = "large"
            }
        };
        #endregion

        [Fact]
        public async Task GetAllCompanyTypeTest()
        {
            try
            {
                //Act
                _companyTypeService.Setup(f => f.GetAllCompanyType()).ReturnsAsync(companyTypeList);
                var output = await _companyController.GetAllCompanyType();

                //Arrange Assert
                Assert.NotNull(output);
            }
            catch (Exception ex)
            {
                Assert.False(false, ex.Message);
            }

        }
    }

}
