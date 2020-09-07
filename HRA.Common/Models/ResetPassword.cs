namespace HRA.Common.Models
{
    public class ResetPassword
    {
        public string NewPassword { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }
    }
}
