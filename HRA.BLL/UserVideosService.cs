using HRA.Common.Enums;
using HRA.Common.Interfaces;
using HRA.Common.Models;
using HRA.DAL;
using HRA.DAL.Convertors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.BLL
{
    public class UserVideosService : IUserVideosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVideosService _videosService;

        public UserVideosService(IUnitOfWork unitOfWork, IVideosService videosService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _videosService = videosService ?? throw new ArgumentNullException(nameof(videosService));
        }

        public async Task<IEnumerable<UserVideos>> GetAll(string userId)
        {
            var videos = await _videosService.GetAll();
            var userVideos = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().Where(x => x.UserId == userId).ToList();
            return userVideos.Select(x => x.ToModel(videos.FirstOrDefault(y => y.Id == x.Id)?.ReferenceId ?? Guid.Empty));
        }

        public async Task<IEnumerable<UserVideos>> GetAllByStatus(string userId, UserVideoStatus videoStatus)
        {
            var videos = await _videosService.GetAll();
            var userVideos = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().Where(x => x.UserId == userId && x.Status == videoStatus).ToList();
            return userVideos.Select(x => x.ToModel(videos.FirstOrDefault(y => y.Id == x.Id)?.ReferenceId ?? Guid.Empty));
        }

        public async Task<UserVideos> GetById(string userId, Guid videoReferenceId)
        {
            var video = await _videosService.GetById(videoReferenceId);

            if (video == null)
            {
                return null;
            }

            var userVideo = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().FirstOrDefault(x => x.VideoId == video.Id && x.UserId == userId);
            return userVideo.ToModel(videoReferenceId);
        }

        public async Task<UserVideos> AddVideo(string userId, Guid videoReferenceId, UserVideoStatus status)
        {
            var video = await _videosService.GetById(videoReferenceId);

            if (video == null)
            {
                throw new InvalidDataException($"Invalid Video Id {videoReferenceId}");
            }

            var userVideo = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().FirstOrDefault(x => x.VideoId == video.Id && x.UserId == userId);
            if (userVideo != null)
            {
                if (status == UserVideoStatus.Completed && userVideo.Status != UserVideoStatus.Completed)
                {
                    userVideo.Status = status;
                    userVideo.ModifiedDate = DateTime.UtcNow; 
                    userVideo.CompletedOn = DateTime.UtcNow;
                    _unitOfWork.Update(userVideo); 
                }
            }
            else
            {
                userVideo = new HRA.DAL.Entity.UserVideos
                {
                    VideoId = video.Id,
                    //CreatedBy = userId,
                    //ModifiedBy = userId,
                    Status = status,
                    UserId = userId,
                   
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                if (status == UserVideoStatus.Completed)
                {
                    userVideo.CompletedOn = DateTime.UtcNow;
                }

                _unitOfWork.Add(userVideo);
            }
            _unitOfWork.Commit();
            return userVideo.ToModel(videoReferenceId);
        }

        public async Task<UserVideos> UpdateVideoStatus(string userId, Guid videoReferenceId, UserVideoStatus videoStatus)
        {
            var video = await _videosService.GetById(videoReferenceId);

            if (video == null)
            {
                throw new InvalidDataException($"Invalid Video Id {videoReferenceId}");
            }

            var userVideo = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().FirstOrDefault(x => x.VideoId == video.Id && x.UserId == userId);
            if (videoStatus == UserVideoStatus.Completed && userVideo.Status != UserVideoStatus.Completed)
            {
                userVideo.Status = videoStatus;
                userVideo.ModifiedDate = DateTime.UtcNow;

                userVideo.CompletedOn = DateTime.UtcNow;
                _unitOfWork.Update(userVideo);
                _unitOfWork.Commit();
            }

            return userVideo.ToModel(videoReferenceId);
        }

        public async Task<UserVideos> RemoveVideo(string userId, Guid videoReferenceId)
        {
            var video = await _videosService.GetById(videoReferenceId);

            if (video == null)
            {
                throw new InvalidDataException($"Invalid Video Id {videoReferenceId}");
            }

            var userVideo = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().FirstOrDefault(x => x.VideoId == video.Id && x.UserId == userId);

            userVideo.IsDeleted = true;
            userVideo.ModifiedDate = DateTime.UtcNow;
            _unitOfWork.Update(userVideo);
            _unitOfWork.Commit();
            return userVideo.ToModel(videoReferenceId);
        }

        public async Task<IEnumerable<VideoWithUserDetails>> GetAllVideosWithUserStatus(string userId)
        {
            var videos = await _videosService.GetAll();
            var userVideos = _unitOfWork.Query<HRA.DAL.Entity.UserVideos>().Where(x => x.UserId == userId && !x.Video.IsDeleted).ToList();
            return videos.Select(x => x.ToVideoWithUserDetailsModel(userVideos.FirstOrDefault(y => y.VideoId == x.Id)));
        }
    }
}
