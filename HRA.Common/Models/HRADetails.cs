using HRA.Common.Enums;
using System;

namespace HRA.Common.Models
{
    public class HRADetails : BaseModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Ssn { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public string ExtraInfo { get; set; }

        public int GroupId { get; set; }
    }
}
