using HRA.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllGroups();
        
        Task<Group> GetGroup(Guid groupId);

        Task<Group> AddGroup(Group groupInfo);

        Task<Group> UpdateGroup(Guid groupId, Group groupInfo);

        Task DeleteGroup(Guid groupId);
    }
}
