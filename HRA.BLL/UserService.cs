using HRA.Common;
using HRA.Common.Helpers;
using HRA.Common.Interfaces;
using HRA.DAL.Convertors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;
using HRA.Common.Models;
using HRA.DAL.Enums;
using User = HRA.DAL.Entity.User;

namespace HRA.BLL
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHRAService _hraService;
        private readonly IGroupService _groupService;

        public UserService(UserManager<User> userManager, IEmailService emailService, IOptionsSnapshot<AppSettings> appSettings, IHRAService hraService, IGroupService groupService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _hraService = hraService ?? throw new ArgumentNullException(nameof(hraService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        public async Task<ResultMessage<UserToken>> Register(Common.Models.User user)
        {
            var userEntity = user.ToEntity();

            var result = new ResultMessage<UserToken>();
            var identityResult = await _userManager.CreateAsync(userEntity, user.Password);
            if (!identityResult.Succeeded)
            {
                result.Messages = GetIdentityErrors(identityResult);

                return result;
            }

            identityResult = await _userManager.AddToRoleAsync(userEntity, RoleType.Member.ToString());
            if (identityResult.Succeeded)
            {
                result.Item = new UserToken
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Ssn = user.Ssn,
                    GroupId = _groupService.GetGroupById(user.GroupId).Result?.ExternalId,
                    Roles = new[] { RoleType.Member.ToString() },
                    Token = GetUserAccessToken(userEntity.UserName, userEntity.Id, new[] { RoleType.Member.ToString() }),
                    IsHRACompleted = _hraService.GetHRADetailBySsn(user.Ssn).Result != null
                };
            }
            else
            {
                result.Messages = GetIdentityErrors(identityResult);
            }

            return result;
        }

        public async Task<UserToken> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserToken
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles.ToArray(),
                Ssn = user.Ssn,
                GroupId = _groupService.GetGroupById(user.GroupId).Result?.ExternalId,
                Token = GetUserAccessToken(user.UserName, user.Id, roles),
                IsHRACompleted = _hraService.GetHRADetailBySsn(user.Ssn).Result != null
            };
        }

        public async Task<HRA.Common.Models.User> GetUserDetails(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user?.ToModel();
        }

        public async Task<ResultMessage<bool>> RecoverPassword(string userName)
        {
            var result = new ResultMessage<bool>();
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                result.Messages.Add(new Message("No such user found!"));
                result.Item = false;
                return result;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = new Uri($"{_appSettings?.UIBaseUrl}/reset-password/{HttpUtility.UrlEncode(token)}"); 
            await _emailService.SendEmail(new MailRequest { Body = url.AbsoluteUri.ToString(), Subject = "Reset Password", ToEmail = user.Email });
            result.Item = true;
            return result;
        }

        public async Task<ResultMessage<bool>> ResetPassword(ResetPassword resetPassword)
        {
            var result = new ResultMessage<bool>();
            var user = await _userManager.FindByNameAsync(resetPassword.UserName);
            if (user == null)
            {
                result.Messages.Add(new Message("Invalid Token."));
                result.Item = false;
                return result;
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);
            result.Item = resetPassResult.Succeeded;
            if (!resetPassResult.Succeeded)
            {
                result.Messages = GetIdentityErrors(resetPassResult);
            }

            return result;
        }

        public async Task<ResultMessage<bool>> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var result = new ResultMessage<bool>();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                result.Messages.Add(new Message("Invalid UserName."));
                return result;
            }

            var changePassResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            result.Item = changePassResult.Succeeded;
            if (!changePassResult.Succeeded)
            {
                result.Messages = GetIdentityErrors(changePassResult);
            }

            return result;
        }

        private string GetUserAccessToken(string userName, string userId, IEnumerable<string> role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.NameIdentifier, userId),
                        new Claim(ClaimTypes.Role, string.Join(',', role)),
                        new Claim(ClaimTypes.Version, "V3.1")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static List<Message> GetIdentityErrors(IdentityResult identityResult)
        {
            return identityResult.Errors.Select(error => new Message(error.Code, error.Description)).ToList();
        }

    }
}
