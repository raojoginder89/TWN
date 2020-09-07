using HRA.Common.Interfaces;
using HRA.Common.Models;
using HRA.DAL;
using HRA.DAL.Convertors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.BLL
{
    public class VideosService : IVideosService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Videos> AddVideo(Videos videos)
        {
            HRA.DAL.Entity.Videos video = videos.ToEntity();
            _unitOfWork.Add(video);
            await _unitOfWork.CommitAsync();
            return video.ToModel();
        }

        public async Task DeleteVideo(Guid referenceId)
        {
            var video = GetVideo(referenceId);

            if (video == null)
            {
                return;
            }

            video.IsDeleted = true;
            _unitOfWork.Update(video);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Videos>> GetAll()
        {
            var entity = await _unitOfWork.Query<HRA.DAL.Entity.Videos>().Where(x => !x.IsDeleted).ToListAsync();
            return entity.Select(x => x.ToModel());
        }

        public async Task<Videos> GetById(Guid referenceId)
        {
            var entity = await _unitOfWork.Query<HRA.DAL.Entity.Videos>().FirstOrDefaultAsync(x => x.ReferenceId == referenceId && !x.IsDeleted);
            return entity.ToModel();
        }

        public async Task<Videos> GetById(int id)
        {
            var entity = await _unitOfWork.Query<HRA.DAL.Entity.Videos>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            return entity.ToModel();
        }

        public async Task<Videos> UpdateVideo(Guid videoReferenceId, Videos videos)
        {
            var video = GetVideo(videoReferenceId);

            if (video == null)
            {
                throw new InvalidDataException($"Invalid Video Id {videoReferenceId}");
            }

            var videoEntity = videos.ToEntity();
            videoEntity.ReferenceId = videoReferenceId;
            videoEntity.Id = video.Id;
            videoEntity.CreatedBy = videoEntity.CreatedBy;
            videoEntity.CreatedDate = videoEntity.CreatedDate;
            _unitOfWork.Update(videoEntity);
            await _unitOfWork.CommitAsync();

            return videoEntity.ToModel();
        }

        private HRA.DAL.Entity.Videos GetVideo(Guid videoId)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.Videos>().FirstOrDefault(x => x.ReferenceId == videoId && !x.IsDeleted);
        }
    }
}
