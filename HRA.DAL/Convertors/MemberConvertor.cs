using HRA.DAL.Entity;
using Model = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class MemberConvertor
    {
        public static Member ToEntity(this Model.Member member)
        {
            if (member == null)
            {
                return null;
            }

            return new Member
            {
                Id = member.Id,
                ReferenceId = member.ReferenceId,
                GroupId = member.GroupId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Ssn = member.Ssn
            };
        }

        public static Model.Member ToModel(this Member member)
        {
            if (member == null)
            {
                return null;
            }

            return new Model.Member
            {
                Id = member.Id,
                ReferenceId = member.ReferenceId,
                GroupId = member.GroupId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Ssn = member.Ssn
            };
        }
    }
}
