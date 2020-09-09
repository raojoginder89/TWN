using HRA.Common.Interfaces;
using HRA.Common.Models;
using HRA.DAL;
using HRA.DAL.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.BLL
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            var groups = _unitOfWork.Query<HRA.DAL.Entity.Group>().Where(x => !x.IsDeleted).ToList();
            return groups.Select(x => x.ToModel());
        }

        public async Task<Group> GetGroup(Guid groupId)
        {
            var group = GetGroupById(groupId);
            return group.ToModel();
        }

        public async Task<Group> GetGroupById(int groupId)
        {
            var group = _unitOfWork.Query<HRA.DAL.Entity.Group>().FirstOrDefault(x => x.Id == groupId && !x.IsDeleted);
            return group.ToModel();
        }

        public async Task<Group> AddGroup(Group groupInfo)
        {
            HRA.DAL.Entity.Group group = groupInfo.ToEntity();
            _unitOfWork.Add(group);
            await _unitOfWork.CommitAsync();
            return group.ToModel();
        }

        public async Task<Group> UpdateGroup(Guid groupId, Group groupInfo)
        {
            groupInfo.ReferenceId = groupId;

            var group = GetGroupById(groupId);
            if (group == null)
            {
                return null;
            }

            var groupEntity = groupInfo.ToEntity();
            groupEntity.Id = group.Id;
            groupEntity.CreatedBy = group.CreatedBy;
            groupEntity.CreatedDate = group.CreatedDate;
            _unitOfWork.Update(groupEntity);
            await _unitOfWork.CommitAsync();
            return groupEntity.ToModel();
        }

        public async Task DeleteGroup(Guid groupId)
        {
            var group = GetGroupById(groupId);
            if (group == null)
            {
                return;
            }

            group.IsDeleted = true;
            _unitOfWork.Update(group);
            await _unitOfWork.CommitAsync();
        }

        private HRA.DAL.Entity.Group GetGroupById(Guid groupId)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.Group>().FirstOrDefault(x => x.ReferenceId == groupId && !x.IsDeleted);
        }

        public async Task<Group> GetGroupByExternalId(string groupExternalId)
        {
            var group = _unitOfWork.Query<HRA.DAL.Entity.Group>().FirstOrDefault(x => x.ExternalId == groupExternalId && !x.IsDeleted);
            return group.ToModel();
        }
    }
}
