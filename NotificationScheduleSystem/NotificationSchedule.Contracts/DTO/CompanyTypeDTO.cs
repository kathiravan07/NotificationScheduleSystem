using System.ComponentModel.DataAnnotations;

namespace NotificationSchedule.Contracts.DTO
{
    public class CompanyTypeDTO
    {
        [Required]
        public int CompanyTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
