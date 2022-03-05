using System;

namespace NotificationSchedule.Contracts.DTO
{
    public class NotificationResponseDTO
    {
        public Guid CompanyId { get; set; }
        public string[] Notifications { get; set; }
    }
}
