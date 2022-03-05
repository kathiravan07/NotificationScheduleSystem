using System;
using System.ComponentModel.DataAnnotations;

namespace NotificationSchedule.Contracts.DTO
{
    public class NotificationDTO
    {
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        [StringLength(50)]
        public string CompanyType { get; set; }
        [Required]
        [StringLength(50)]
        public string Market { get; set; }
        [Required]
        public long CompanyNumber { get; set; }
        [Required]
        public DateTime CallDate { get; set; }
    }
}
