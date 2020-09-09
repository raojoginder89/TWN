using HRA.Common.Interfaces;
using HRA.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HRA.Controllers
{
    [Route("api/members")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("")]
        [Authorize(Policy = AccessPolicy.SuperAdminLevel)]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }

        [HttpGet("{memberId}")]
        [Authorize(Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> GetMember(string memberId)
        {
            if (!Guid.TryParse(memberId, out var referenceId))
            {
                return BadRequest("Invalid Member Id");
            }

            var member = await _memberService.GetMember(referenceId);
            return Ok(member);
        }

        [HttpPost]
        [Authorize(Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> AddMember(Member memberInfo)
        {
            var member = await _memberService.AddMember(memberInfo);
            return Ok(member);
        }

        [HttpPost("{memberId}")]
        [Authorize(Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> UpdateMember(string memberId, Member memberInfo)
        {
            if (!Guid.TryParse(memberId, out var referenceId))
            {
                return BadRequest("Invalid Member Id");
            }
            var member = await _memberService.UpdateMember(referenceId, memberInfo);
            return Ok(member);
        }

        [HttpDelete("{memberId}")]
        [Authorize(Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> DeleteMember(string memberId)
        {
            if (!Guid.TryParse(memberId, out var referenceId))
            {
                return BadRequest("Invalid Member Id");
            }
            
            await _memberService.DeleteMember(referenceId);
            return Ok();
        }
    }
}
