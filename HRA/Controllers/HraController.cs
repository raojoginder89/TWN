using HRA.Common.Interfaces;
using HRA.Contracts;
using HRA.Convertors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace HRA.Controllers
{
    [Route("api/hra")]
    [ApiController]
    public class HraController : ControllerBase
    {
        private readonly IHRAService _hraService;
        private readonly IGroupService _groupService;
        private readonly IAddressService _addressService;

        public HraController(IHRAService hraService, IGroupService groupService, IAddressService addressService)
        {
            _hraService = hraService ?? throw new ArgumentNullException(nameof(hraService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        [HttpPost("{groupId}")]
        public async Task<IActionResult> SaveHraDetails(string groupId, HRADetails details)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var group = await _groupService.GetGroup(referenceId);
            if (group == null)
            {
                throw new SecurityException("Group does not exists or you do not have permission to manage it.");
            }

            details.GroupId = groupId;
            var detail = await _hraService.GetHRADetailBySsn(details.Ssn);
            if (detail != null)
            {
                return BadRequest("Wellness Engagement Questionnaire was already submitted");
            }

            var info = await _hraService.AddHRADetails(details.ToModel(group.Id));
            return Ok();
        }


        [HttpGet("{groupId}/{ssn}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> GetHraDetails(string groupId, string ssn)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var group = await _groupService.GetGroup(referenceId);
            if (group == null)
            {
                throw new SecurityException("Group does not exists or you do not have permission to manage it.");
            }

            var detail = await _hraService.GetHRADetailBySsn(ssn);
            return Ok(detail.ToContract(groupId));
        }

        [HttpGet("{groupId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> GetAllHraDetailsForGroup(string groupId)
        {
            if (!Guid.TryParse(groupId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var group = await _groupService.GetGroup(referenceId);
            if (group == null)
            {
                throw new SecurityException("Group does not exists or you do not have permission to manage it.");
            }

            var details = await _hraService.GetAllHras();
            return Ok(details.Select(x => x.ToContract(groupId)));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> GetAllHraDetails()
        {
            var details = await _hraService.GetAllHras();
            var groups = await _groupService.GetAllGroups();
            return Ok(details.Select(x => x.ToContract(groups.First(y => y.Id == x.GroupId)?.ReferenceId.ToString())));
        }

        [HttpDelete("{groupId}/{ssn}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> GetAllHraDetails(string groupId, string ssn)
        {
            var detail = await _hraService.GetHRADetailBySsn(ssn);
            return Ok();
        }
    }
}
