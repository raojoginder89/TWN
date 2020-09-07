using HRA.Common.Enums;
using HRA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Linq;
using HRA.Convertors;

namespace HRA.Controllers
{
    [Route("api/user/videos")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserVideoController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IVideosService _videoService;
        private readonly IUserVideosService _userVideoService;

        public UserVideoController(IUserService userService, IVideosService videoService, IUserVideosService userVideoService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _videoService = videoService ?? throw new ArgumentNullException(nameof(videoService));
            _userVideoService = userVideoService ?? throw new ArgumentNullException(nameof(userVideoService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideosByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var videos = await _userVideoService.GetAll(userId);
            return Ok(videos.OrderByDescending(x=>x.Id).Select(x=>x.ToContract()));
        }

        [HttpGet("{videoReferenceId}")]
        public async Task<IActionResult> GetAllVideosByUserId(string videoReferenceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(videoReferenceId, out var referenceId))
            {
                return BadRequest();
            }

            var video = await _userVideoService.GetById(userId, referenceId);
            return Ok(video.ToContract());
        }

        [HttpGet("status/{videoStatus}")]
        public async Task<IActionResult> GetAllVideosByUserId(HRA.Contracts.Enums.UserVideoStatus videoStatus)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var videos = await _userVideoService.GetAllByStatus(userId, (UserVideoStatus)videoStatus);
            return Ok(videos.Select(x => x.ToContract()));
        }

        [HttpPut("{videoReferenceId}/{videoStatus}")]
        public async Task<IActionResult> UpdateVideoStatus(string videoReferenceId, HRA.Contracts.Enums.UserVideoStatus videoStatus)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(videoReferenceId, out var referenceId))
            {
                return BadRequest();
            }
            
            var video = await _userVideoService.UpdateVideoStatus(userId, referenceId, (UserVideoStatus)videoStatus);
            return Ok(video.ToContract());
        }

        [HttpPost("{videoReferenceId}/{videoStatus}")]
        public async Task<IActionResult> AddVideoToLibrary(string videoReferenceId, HRA.Contracts.Enums.UserVideoStatus videoStatus)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(videoReferenceId, out var referenceId))
            {
                return BadRequest();
            }

            var video = await _userVideoService.AddVideo(userId, referenceId, (UserVideoStatus)videoStatus);
            return Ok(video.ToContract());
        }
    }
}
