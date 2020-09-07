using HRA.Common.Interfaces;
using HRA.Contracts;
using HRA.Convertors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRA.Controllers
{
    [Route("api/videos")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videoService;
        private readonly IUserVideosService _userVideoService;
        private readonly IUserService _userService;

        public VideosController(IUserService userService, IVideosService videoService, IUserVideosService userVideoService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _videoService = videoService ?? throw new ArgumentNullException(nameof(videoService));
            _userVideoService = userVideoService ?? throw new ArgumentNullException(nameof(userVideoService));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var videos = await _videoService.GetAll();
            return Ok(videos.Select(x=>x.ToContract()));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllVideosWithUserStatus()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var videos = await _userVideoService.GetAllVideosWithUserStatus(userId);
            return Ok(videos.OrderByDescending(x=>x.Id).Select(x => x.ToContract()));
        }

        [HttpGet("{videoReferenceId}")]
        public async Task<IActionResult> GetById(string videoReferenceId)
        {
            if (!Guid.TryParse(videoReferenceId, out Guid referenceId))
            {
                return BadRequest();
            };

            var video = await _videoService.GetById(referenceId);
            return Ok(video.ToContract());
        }


        [HttpPost]
        [Authorize(Policy = AccessPolicy.AdminLevel)]
        public async Task<IActionResult> AddVideo(Videos videoInfo)
        {
            var video = await _videoService.AddVideo(videoInfo.ToModel());
            return Ok(video.ToContract());
        }

        [HttpPut("{videoId}")]
        [Authorize(Policy = AccessPolicy.GroupLevel)]
        public async Task<IActionResult> UpdateVideo(string videoId, Videos videoInfo)
        {
            if (!Guid.TryParse(videoId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            var video = await _videoService.UpdateVideo(referenceId, videoInfo.ToModel());
            return Ok(video.ToContract());
        }

        [Authorize(Policy = AccessPolicy.AdminLevel)]
        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteVideo(string videoId)
        {
            if (!Guid.TryParse(videoId, out var referenceId))
            {
                return BadRequest("Invalid Group Id");
            }

            await _videoService.DeleteVideo(referenceId);
            return Ok();
        }
    }
}
