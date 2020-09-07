using HRA.DAL.Entity;
using System;
using HRA.Common.Enums;
using Model = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class UserVideosConvertor
    {
        public static UserVideos ToEntity(this Model.UserVideos userVideo)
        {
            return new UserVideos
            {
                Id = userVideo.Id,
                Status = userVideo.Status,
                CompletedOn = userVideo.CompletedOn,
                UserId = userVideo.UserId,
                VideoId = userVideo.VideoId,
                CreatedBy = userVideo.CreatedBy,
                ModifiedBy = userVideo.ModifiedBy,
                CreatedDate = userVideo.CreatedDate,
                ModifiedDate = userVideo.ModifiedDate
            };
        }

        public static Model.UserVideos ToModel(this UserVideos userVideo, Guid videoReferenceId)
        {
            return new Model.UserVideos
            {
                Id = userVideo.Id,
                VideoReferenceId = videoReferenceId,
                Status = userVideo.Status,
                CompletedOn = userVideo.CompletedOn,
                UserId = userVideo.UserId,
                VideoId = userVideo.VideoId,
                CreatedBy = userVideo.CreatedBy,
                ModifiedBy = userVideo.ModifiedBy,
                CreatedDate = userVideo.CreatedDate,
                ModifiedDate = userVideo.ModifiedDate
            };
        }
    }
}
