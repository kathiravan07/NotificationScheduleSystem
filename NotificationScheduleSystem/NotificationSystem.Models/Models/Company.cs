using System;

namespace NotificationSystem.infrastructure.Models
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long CompanyNumber { get; set; }
        public int CompanyTypeId { get; set; }
        public int MarketId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual CompanyType CompanyType { get; set; }
        public virtual Market Market { get; set; }
    }
}
