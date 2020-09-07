using HRA.DAL.Entity;
using Model = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class VideosConvertor
    {
        public static Videos ToEntity(this Model.Videos video)
        {
            return new Videos
            {
                Id = video.Id,
                ReferenceId = video.ReferenceId,
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

        public static Model.Videos ToModel(this Videos video)
        {
            return new Model.Videos
            {
                Id = video.Id,
                ReferenceId = video.ReferenceId,
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

        public static Model.VideoWithUserDetails ToVideoWithUserDetailsModel(this Model.Videos video, UserVideos userVideo)
        {
            return new Model.VideoWithUserDetails
            {
                Id = video.Id,
                ReferenceId = video.ReferenceId,
                Url = video.Url,
                Title = video.Title,
                Description = video.Description,
                ExtraInfo = video.ExtraInfo,
                CompletedOn = userVideo?.CompletedOn,
                Status = userVideo?.Status ?? Common.Enums.UserVideoStatus.None,
                CreatedBy = video.CreatedBy,
                ModifiedBy = video.ModifiedBy,
                CreatedDate = video.CreatedDate,
                ModifiedDate = video.ModifiedDate
            };
        }
    }
}
