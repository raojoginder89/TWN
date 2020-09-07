using System;

namespace HRA.Common.Models
{
    public class Group : BaseModel
    {
        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public string Address { get; set; }

        public string WebSiteUrl { get; set; }

        public string ExternalId { get; set; }

        public bool IsActive { get; set; }
    }
}
