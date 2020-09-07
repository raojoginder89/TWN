using HRA.Common.Models;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(MailRequest mailRequest);
    }
}
