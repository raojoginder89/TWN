using HRA.Contracts;
using System;
using Models = HRA.Common.Models;

namespace HRA.Convertors
{
    public static class UserVideosConvertor
    {
        public static UserVideos ToContract(this Models.UserVideos video)
        {
            if (video == null)
            {
                return null;
            }

            return new UserVideos
            {
                VideoReferenceId = video.VideoReferenceId,
                CompletedOn = video.CompletedOn,
                Status = (Contracts.Enums.UserVideoStatus)video.Status,               
                CreatedDate = video.CreatedDate,
                ModifiedDate = video.ModifiedDate
            };
        }

        public static Models.UserVideos ToModel(this UserVideos video)
        {
            if (video == null)
            {
                return null;
            }

            return new Models.UserVideos
            {
                VideoReferenceId = video.VideoReferenceId,
                CompletedOn = video.CompletedOn,
                Status = (Common.Enums.UserVideoStatus)video.Status,
                CreatedDate = video.CreatedDate,
                ModifiedDate = video.ModifiedDate
            };
        }
    }
}
