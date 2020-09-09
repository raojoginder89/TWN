using HRA.DAL.Entity;
using UserModel = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class UserConvertor
    {
        public static User ToEntity(this UserModel.User user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Ssn = user.Ssn,
                IsActive = user.IsActive,
                GroupId = user.GroupId,
            };
        }

        public static UserModel.User ToModel(this User user)
        {
            return new UserModel.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Ssn = user.Ssn,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate,
                IsActive = user.IsActive,
                GroupId = user.GroupId,
            };
        }
    }
}
