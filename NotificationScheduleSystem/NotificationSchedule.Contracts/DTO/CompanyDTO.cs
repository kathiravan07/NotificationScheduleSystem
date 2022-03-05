using System;
using System.ComponentModel.DataAnnotations;

namespace NotificationSchedule.Contracts.DTO
{
    public class CompanyDTO
    {
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }
        public long CompanyNumber { get; set; }
        [Required]
        public int CompanyTypeId { get; set; }
        [Required]
        public int MarketId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
