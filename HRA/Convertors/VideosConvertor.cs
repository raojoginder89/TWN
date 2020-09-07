using HRA.Contracts;
using System;
using Models = HRA.Common.Models;

namespace HRA.Convertors
{
    public static class VideosConvertor
    {
        public static Videos ToContract(this Models.Videos video)
        {
            if (video == null)
            {
                return null;
            }

            return new Videos
            {
                ReferenceId = video.ReferenceId.ToString(),
                Url = video.Url,
                Title = video.Title,
                Description = video.Description,
                ExtraInfo = video.ExtraInfo,
                CreatedBy = video.CreatedBy,
                ModifiedBy = video.ModifiedBy,
                CreatedDate = video.CreatedDate,
                ModifiedDate = video.ModifiedDate
            };
        }

        public static Models.Videos ToModel(this Videos video)
        {
            if (video == null)
            {
                return null;
            }

            Guid.TryParse(video.ReferenceId, out var referenceId);
            return new Models.Videos
            {
                ReferenceId = referenceId,
                Url = video.Url,
                Title = video.Title,
                Description = video.Description,
                ExtraInfo = video.ExtraInfo
            };
        }

        public static VideoWithUserDetails ToContract(this Models.VideoWithUserDetails video)
        {
            if (video == null)
            {
                return null;
            }

            return new VideoWithUserDetails
            {
                ReferenceId = video.ReferenceId,
                Url = video.Url,
                Title = video.Title,
                Description = video.Description,
                ExtraInfo = video.ExtraInfo,
                Status = (Contracts.Enums.UserVideoStatus)video.Status,
                CompletedOn = video.CompletedOn,
                CreatedDate = video.CreatedDate,
                ModifiedDate = video.ModifiedDate
            };
        }
    }
}
