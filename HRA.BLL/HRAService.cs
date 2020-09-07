using HRA.Common.Interfaces;
using HRA.Common.Models;
using HRA.DAL;
using HRA.DAL.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace HRA.BLL
{
    public class HRAService : IHRAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupService _groupService;

        public HRAService(IUnitOfWork unitOfWork, IGroupService groupService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        public async Task<HRADetails> AddHRADetails(HRADetails hraDetail)
        {
            HRA.DAL.Entity.HraDetails hraDetailEntity = hraDetail.ToEntity();
            _unitOfWork.Add(hraDetailEntity);
            await _unitOfWork.CommitAsync();
            return hraDetailEntity.ToModel();
        }

        public async Task DeleteGroup(string ssn)
        {
            var hraDetail = GetBySsn(ssn);
            if (hraDetail == null)
            {
                return;
            }

            hraDetail.IsDeleted = true;
            _unitOfWork.Update(hraDetail);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<HRADetails>> GetAllHras()
        {
            var hraDetails = _unitOfWork.Query<HRA.DAL.Entity.HraDetails>().Where(x => !x.IsDeleted).ToList();
            return hraDetails.Select(x => x.ToModel());
        }

        public async Task<IEnumerable<HRADetails>> GetAllHrasForGroup(Guid groupId)
        {
            var group = _groupService.GetGroup(groupId);
            if (group == null)
            {
                throw new SecurityException("Group does not exists or you do not have permission to manage it.");
            }

            return await GetAllHrasForGroup(group.Id);
        }

        public async Task<IEnumerable<HRADetails>> GetAllHrasForGroup(int groupId)
        { 
            var hraDetails = _unitOfWork.Query<HRA.DAL.Entity.HraDetails>().Where(x => x.GroupId == groupId && !x.IsDeleted).ToList();
            return hraDetails.Select(x => x.ToModel());
        }

        public async Task<HRADetails> GetHRADetailById(int id)
        {
            var hraDetail = _unitOfWork.Query<HRA.DAL.Entity.HraDetails>().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return hraDetail.ToModel();
        }

        public async Task<HRADetails> GetHRADetailBySsn(string ssn)
        {
            var hraDetail = GetBySsn(ssn);
            return hraDetail.ToModel();
        }

        public async Task<HRADetails> UpdateGroup(string ssn, HRADetails hraDetail)
        {
            hraDetail.Ssn = ssn;

            var hraDetailEntity = GetBySsn(ssn);
            if (hraDetailEntity == null)
            {
                return null;
            }

            hraDetailEntity = hraDetail.ToEntity();
            hraDetailEntity.Id = hraDetail.Id;
            hraDetailEntity.CreatedBy = hraDetail.CreatedBy;
            hraDetailEntity.CreatedDate = hraDetail.CreatedDate;
            _unitOfWork.Update(hraDetail);
            await _unitOfWork.CommitAsync();
            return hraDetailEntity.ToModel();
        }

        private HRA.DAL.Entity.HraDetails GetBySsn(string ssn)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.HraDetails>().FirstOrDefault(x => x.Ssn.ToLower() == ssn.ToLower() && !x.IsDeleted);
        }
    }
}
