using System;

namespace NotificationSystem.infrastructure.Models
{
    public class CompanyNotification
    {
        public int CompanyNotificationId { get; set; }
        public DateTime SendDate { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsSent { get; set; }
        public virtual Company Companies { get; set; }
    }
}
