using System;
using HRA.Common.Enums;

namespace HRA.Common.Models
{
    public class User
    {
        public string Id { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime Dob { get; set; }

        public string Ssn { get; set; }

        public RoleType RoleType { get; set; }

        public Gender Gender { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
