using HRA.Common.Enums;
using HRA.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IUserVideosService
    {
        Task<IEnumerable<UserVideos>> GetAll(string userId);

        Task<IEnumerable<UserVideos>> GetAllByStatus(string userId, UserVideoStatus videoStatus);

        Task<UserVideos> GetById(string userId, Guid videoReferenceId);

        Task<UserVideos> AddVideo(string userId, Guid videoReferenceId, UserVideoStatus status);

        Task<UserVideos> UpdateVideoStatus(string userId, Guid videoReferenceId, UserVideoStatus videoStatus);

        Task<UserVideos> RemoveVideo(string userId, Guid videoReferenceId);

        Task<IEnumerable<VideoWithUserDetails>> GetAllVideosWithUserStatus(string userId);
    }
}
