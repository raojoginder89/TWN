using System;

namespace HRA.Contracts
{
    public class Group
    {
        public string ReferenceId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public string Address { get; set; }

        public string WebSiteUrl { get; set; }

        //public string ExternalId { get; set; }

        //public bool IsActive { get; set; }

        //public string CreatedBy { get; set; }

        //public string ModifiedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
