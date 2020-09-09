using HRA.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembers();

        Task<Member> GetMember(Guid memberReferenceId);

        Task<Member> GetMemberById(int memberId);

        Task<Member> AddMember(Member memberInfo);

        Task<Member> UpdateMember(Guid memberReferenceId, Member memberInfo);

        Task DeleteMember(Guid memberReferenceId);

        Task<Member> GetMemberBySsn(string ssn);
    }
}
