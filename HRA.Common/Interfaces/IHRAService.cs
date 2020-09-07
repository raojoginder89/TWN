using HRA.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IHRAService
    {
        Task<IEnumerable<HRADetails>> GetAllHras();

        Task<IEnumerable<HRADetails>> GetAllHrasForGroup(Guid groupId);

        Task<IEnumerable<HRADetails>> GetAllHrasForGroup(int groupId);

        Task<HRADetails> GetHRADetailBySsn(string ssn);

        Task<HRADetails> GetHRADetailById(int id);

        Task<HRADetails> AddHRADetails(HRADetails hraDetail);

        Task<HRADetails> UpdateGroup(string Ssn, HRADetails hraDetail);

        Task DeleteGroup(string Ssn);
    }
}
