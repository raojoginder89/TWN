using System;

namespace HRA.Common.Models
{
    public class Member
    {
        public int Id { get; set; }
                
        public Guid ReferenceId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Ssn { get; set; }

        public int GroupId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
