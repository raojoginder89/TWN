using HRA.Common.Interfaces;
using HRA.Contracts;
using HRA.Convertors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.Controllers
{
    [Route("api/groups")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpGet("")]
        //[Authorize(Policy = AccessPolicy.SuperAdminLevel)]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroups();
            return Ok(groups.Select(x => x.ToContract()));
        }

        [AllowAnonymous]
        [HttpGet("{groupId}/name")]
        public async Task<IActionResult> GetGroupName(string groupId)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var group = await _groupService.GetGroup(referenceId);
            return Ok(new StringResponse { Value = group.Name });
        }

        [HttpGet("{groupId}")]
        [Authorize(Policy = AccessPolicy.GroupLevel)]
        public async Task<IActionResult> GetGroup(string groupId)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var group = await _groupService.GetGroup(referenceId);
            return Ok(group.ToContract());
        }

        [HttpPost]
        [Authorize(Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> AddGroup(Group groupInfo)
        {
            //Register them in the Users Table and add role group to it
            var group = await _groupService.AddGroup(groupInfo.ToModel());
            return Ok(group.ToContract());
        }

        [HttpPut("{groupId}")]
        [Authorize(Policy = AccessPolicy.GroupLevel)]
        public async Task<IActionResult> UpdateGroup(string groupId, Group groupInfo)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var group = await _groupService.UpdateGroup(referenceId, groupInfo.ToModel());
            return Ok(group.ToContract());
        }

        [Authorize(Policy = AccessPolicy.AdminLevel)]
        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteGroup(string groupId)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            await _groupService.DeleteGroup(referenceId);
            return Ok();
        }
    }
}
