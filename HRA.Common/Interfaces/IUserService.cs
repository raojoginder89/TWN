using HRA.Common.Helpers;
using HRA.Common.Models;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IUserService
    {
        Task<ResultMessage<string>> Register(User user);

        Task<UserToken> Login(string userName, string password);

        Task<User> GetUserDetails(string userName);

        Task<ResultMessage<bool>> RecoverPassword(string userName);

        Task<ResultMessage<bool>> ResetPassword(ResetPassword resetPassword);

        Task<ResultMessage<bool>> ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
