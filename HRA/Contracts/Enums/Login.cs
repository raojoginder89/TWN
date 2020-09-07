using System.ComponentModel.DataAnnotations;

namespace HRA.Contracts.Enums
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
