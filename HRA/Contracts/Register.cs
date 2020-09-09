using System;
using System.ComponentModel.DataAnnotations;

namespace HRA.Contracts
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        public DateTime? Dob { get; set; }
        
        [Required]
        public string Ssn { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
