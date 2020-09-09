using HRA.Common.Interfaces;
using HRA.Contracts;
using HRA.Contracts.Enums;
using HRA.Convertors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HRA.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IMemberService _memberService;

        public UserController(IUserService userService, IGroupService groupService, IMemberService memberService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var member = await _memberService.GetMemberBySsn(model.Ssn);
                if (member == null)
                {
                    return BadRequest("Invalid Employee Id");
                }
                var user = model.ToModel();
                user.GroupId = member.GroupId;
                var result = await _userService.Register(user);
                if (result.IsError)
                {
                    return BadRequest(result.Messages);
                }

                return Ok(result.Item.ToContract());
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var userWithToken = await _userService.Login(model.UserName, model.Password);
            if (userWithToken == null || string.IsNullOrWhiteSpace(userWithToken.Token))
            {
                return BadRequest("Invalid Username or Password");
            }

            return Ok(userWithToken.ToContract());
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> RecoverPassword(ForgotPassword model)
        {
            var result = await _userService.RecoverPassword(model.UserName);
            if (result.IsError)
            {
                return BadRequest(result.Messages);
            }

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> RecoverPassword(ResetPassword model)
        {
            var result = await _userService.ResetPassword(model.ToModel());
            if (result.IsError)
            {
                return BadRequest(result.Messages);
            }

            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            var userName = User.Identity.Name;
            var result = await _userService.ChangePassword(userName, model.CurrentPassword, model.NewPassword);
            if (result.IsError)
            {
                return BadRequest(result.Messages);
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserDetails()
        {
            var userName = User.Identity.Name;
            var userDetails = await _userService.GetUserDetails(userName);
            return Ok(userDetails.ToContract());
        }
    }
}
