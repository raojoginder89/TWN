using HRA.DAL.Entity;
using Model = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class HRAConvertor
    {
        public static HraDetails ToEntity(this Model.HRADetails hraDetail)
        {
            if (hraDetail == null)
            {
                return null;
            }

            return new HraDetails
            {
                FirstName = hraDetail.FirstName,
                MiddleName = hraDetail.MiddleName,
                LastName = hraDetail.LastName,
                Dob = hraDetail.Dob,
                Ssn = hraDetail.Ssn,
                Email = hraDetail.Email,
                PhoneNumber = hraDetail.PhoneNumber,
                Gender = (DAL.Enums.Gender)hraDetail.Gender,
                ExtraInfo = hraDetail.ExtraInfo,
                GroupId = hraDetail.GroupId,
                CreatedBy = hraDetail.CreatedBy,
                ModifiedBy = hraDetail.ModifiedBy,
                CreatedDate = hraDetail.CreatedDate,                
                ModifiedDate = hraDetail.ModifiedDate
            };
        }

        public static Model.HRADetails ToModel(this HraDetails hraDetail)
        {
            if (hraDetail == null)
            {
                return null;
            }

            return new Model.HRADetails
            {
                Id = hraDetail.Id,
                FirstName = hraDetail.FirstName,
                MiddleName = hraDetail.MiddleName,
                LastName = hraDetail.LastName,
                Dob = hraDetail.Dob,
                Ssn = hraDetail.Ssn,
                Email = hraDetail.Email,
                PhoneNumber = hraDetail.PhoneNumber,
                Gender = (Common.Enums.Gender)hraDetail.Gender,
                ExtraInfo = hraDetail.ExtraInfo,
                GroupId = hraDetail.GroupId,
                CreatedBy = hraDetail.CreatedBy,
                ModifiedBy = hraDetail.ModifiedBy,
                CreatedDate = hraDetail.CreatedDate,
                ModifiedDate = hraDetail.ModifiedDate
            };
        }
    }
}
