using System.ComponentModel.DataAnnotations;

namespace HRA.Contracts
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
    }
}
