using NotificationSchedule.Contracts.DTO;
using System;
using System.Threading.Tasks;

namespace NotificationSchedule.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<string> CompanyCreate(CompanyDTO companyDTO);
        CompanyDTO GetCompany(Guid companyId);
    }
}
