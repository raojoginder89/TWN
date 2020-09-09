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
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Member> AddMember(Member memberInfo)
        {
            HRA.DAL.Entity.Member member = memberInfo.ToEntity();
            _unitOfWork.Add(member);
            await _unitOfWork.CommitAsync();
            return member.ToModel();
        }

        public async Task DeleteMember(Guid memberReferenceId)
        {
            var member = GetMemberById(memberReferenceId);
            if (member == null)
            {
                return;
            }

            member.IsDeleted = true;
            _unitOfWork.Update(member);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            var members = _unitOfWork.Query<HRA.DAL.Entity.Member>().Where(x => !x.IsDeleted).ToList();
            return members.Select(x => x.ToModel());
        }

        public async Task<Member> GetMember(Guid memberReferenceId)
        {
            var member = GetMemberById(memberReferenceId);
            return member.ToModel();
        }

        public async Task<Member> GetMemberById(int memberId)
        {
            var member = _unitOfWork.Query<HRA.DAL.Entity.Member>().FirstOrDefault(x => x.Id == memberId && !x.IsDeleted);
            return member.ToModel();
        }

        public async Task<Member> GetMemberBySsn(string ssn)
        {
            var member = _unitOfWork.Query<HRA.DAL.Entity.Member>().FirstOrDefault(x => x.Ssn == ssn && !x.IsDeleted);
            return member.ToModel();
        }

        public async Task<Member> UpdateMember(Guid memberReferenceId, Member memberInfo)
        {
            memberInfo.ReferenceId = memberReferenceId;

            var member = GetMemberById(memberReferenceId);
            if (member == null)
            {
                return null;
            }

            var memberEntity = memberInfo.ToEntity();
            memberEntity.Id = member.Id;
            _unitOfWork.Update(memberEntity);
            await _unitOfWork.CommitAsync();
            return memberEntity.ToModel();
        }

        private HRA.DAL.Entity.Member GetMemberById(Guid memberRefernceId)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.Member>().FirstOrDefault(x => x.ReferenceId == memberRefernceId && !x.IsDeleted);
        }
    }
}
