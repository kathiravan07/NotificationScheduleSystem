using Microsoft.Extensions.Logging;
using Notification.DataAccess.GenericRepository.Interface;
using NotificationSchedule.Contracts.DTO;
using NotificationSchedule.Services.Interfaces;
using NotificationSystem.infrastructure.Models;
using System;
using System.Threading.Tasks;


namespace NotificationSchedule.Services
{
    public class CompanyService : ICompanyService
    {
        private const string SUCCESS_MESSAGE = "Company Created Successfully";
        private readonly ILogger logger;
        private readonly IGenericRepository<Company> genericRepository;
        public CompanyService(IGenericRepository<Company> genericRepository, ILogger<CompanyService> logger)
        {
            this.genericRepository = genericRepository;
            this.logger = logger;
        }
        /// <summary>
        /// CompanyCreate
        /// </summary>
        /// <param name="companyDTO"></param>
        /// <returns>Response message</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> CompanyCreate(CompanyDTO companyDTO)
        {
            try
            {
                Company company = new()
                {
                    CompanyId = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    CompanyName = companyDTO.CompanyName,
                    CompanyTypeId = companyDTO.CompanyTypeId,
                    MarketId = companyDTO.MarketId,
                    CompanyNumber = Convert.ToInt64(DateTime.UtcNow.Ticks.ToString()[8..])
                };

                await genericRepository.InsertAsync(company);
                await genericRepository.SaveAsync();
                return SUCCESS_MESSAGE;
            }
            catch (Exception ex)
            {
                logger.LogError("Create Company Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// GetCompany
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>Returns given company information</returns>
        /// <exception cref="NotImplementedException"></exception>
        public CompanyDTO GetCompany(Guid companyId)
        {
            try
            {
                var company = genericRepository.GetByID(companyId);
                CompanyDTO companyDTO = null;
                if (company != null)
                {
                    companyDTO = new()
                    {
                        CompanyId = company.CompanyId,
                        CompanyName = company.CompanyName,
                        CompanyNumber = company.CompanyNumber,
                        CompanyTypeId = company.CompanyTypeId,
                        MarketId = company.MarketId,
                    };
                    return companyDTO;
                }
                return companyDTO;
            }
            catch (Exception ex)
            {
                logger.LogError("Get Company Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
