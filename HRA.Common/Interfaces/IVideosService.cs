using HRA.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IVideosService
    {
        Task<IEnumerable<Videos>> GetAll();

        Task<Videos> GetById(Guid referenceId);

        Task<Videos> GetById(int id);
        
        Task<Videos> AddVideo(Videos videos);
        
        Task<Videos> UpdateVideo(Guid referenceId, Videos videos);
        
        Task DeleteVideo(Guid referenceId);
    }
}
