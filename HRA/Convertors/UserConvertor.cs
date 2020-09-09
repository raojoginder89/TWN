using System;
using HRA.Contracts;
using Models = HRA.Common.Models;

namespace HRA.Convertors
{
    public static class UserConvertor
    {
        public static Models.User ToModel(this Register model)
        {
            return new Models.User
            {
                FirstName = model.Name,
                Password = model.Password,
                Dob = model.Dob,
                Email = model.UserName,
                UserName = model.UserName,
                Ssn = model.Ssn,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsActive = true
            };
        }

        public static User ToContract(this Models.User model)
        {
            return new User
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Dob = model.Dob,
                UserName = model.UserName,
                Ssn = model.Ssn,
                Gender = model.Gender.ToString(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsActive = true,
            };
        }

        public static Models.ResetPassword ToModel(this ResetPassword model)
        {
            return new Models.ResetPassword
            {
                NewPassword = model.NewPassword,
                UserName = model.UserName,
                Token = model.Token
            };
        }


        public static UserToken ToContract(this Models.UserToken model)
        {
            return new UserToken
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Token = model.Token,
                Roles = model.Roles,
                GroupId = model.GroupId,
                Ssn = model.Ssn,
                IsHRACompleted = model.IsHRACompleted
            };
        }
    }
}
