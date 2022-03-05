using NotificationSchedule.Contracts.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationSchedule.Services.Interfaces
{
    public interface ICompanyTypeService
    {
        Task<string> CreateCompanyType(CompanyTypeDTO companyTypeDTO);
        Task<string> UpdateCompanyType(CompanyTypeDTO companyTypeDTO);
        Task<List<CompanyTypeDTO>> GetAllCompanyType();
    }
}
