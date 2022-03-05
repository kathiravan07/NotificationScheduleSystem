using Microsoft.Extensions.Logging;
using Notification.DataAccess.GenericRepository.Interface;
using NotificationSchedule.Contracts.DTO;
using NotificationSchedule.Services.Interfaces;
using NotificationSystem.infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NotificationSchedule.Services
{
    public class CompanyTypeService : ICompanyTypeService
    {
        private const string COMPANYTYPE_SUCCESS = "CompanyTyp Saved Successfully";
        private const string COMPANYTYPE_NOTFOUNT = "Company Type Not Fount";
        private readonly IGenericRepository<CompanyType> genericRepository;
        private readonly ILogger logger;

        public CompanyTypeService(IGenericRepository<CompanyType> genericRepository, ILogger<CompanyTypeService> logger)
        {
            this.genericRepository = genericRepository;
            this.logger = logger;
        }

        /// <summary>
        /// CreateCompanyType
        /// </summary>
        /// <param name="companyTypeDTO"></param>
        /// <returns>Response string</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> CreateCompanyType(CompanyTypeDTO companyTypeDTO)
        {
            try
            {
                CompanyType companyType = new()
                {
                    CompanyTypeId = companyTypeDTO.CompanyTypeId,
                    Type = companyTypeDTO.Type,
                };
                genericRepository.Insert(companyType);
                await genericRepository.SaveAsync();
                return COMPANYTYPE_SUCCESS;
            }
            catch (Exception ex)
            {
                logger.LogError("Create CompanyType Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrive all the record related to CompanyType
        /// </summary>
        /// <returns>Return list of companytype record</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CompanyTypeDTO>> GetAllCompanyType()
        {
            try
            {
                var response = await genericRepository.GetAsync();
                List<CompanyTypeDTO> listOfCompanyType = null;
                if (response.Any())
                {
                    listOfCompanyType = new List<CompanyTypeDTO>();
                    response.ToList().ForEach(companyType => { CompanyTypeDTO companyTypeDTO = new() { Type = companyType.Type, CompanyTypeId = companyType.CompanyTypeId }; listOfCompanyType.Add(companyTypeDTO); });
                    return listOfCompanyType;
                }
                return listOfCompanyType;
            }
            catch (Exception ex)
            {
                logger.LogError("GetAll CompanyType Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// UpdateCompanyType
        /// </summary>
        /// <param name="companyTypeDTO"></param>
        /// <returns>Response string</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> UpdateCompanyType(CompanyTypeDTO companyTypeDTO)
        {
            try
            {
                var companyType = genericRepository.GetByID(companyTypeDTO.CompanyTypeId);
                if (companyType != null)
                {
                    companyType.Type = companyTypeDTO.Type;
                    genericRepository.Update(companyType);
                    await genericRepository.SaveAsync();
                    return COMPANYTYPE_SUCCESS;
                }
                return COMPANYTYPE_NOTFOUNT;
            }
            catch (Exception ex)
            {
                logger.LogError("Update CompanyType Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
