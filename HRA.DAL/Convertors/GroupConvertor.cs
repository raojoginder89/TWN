using HRA.DAL.Entity;
using Model = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class GroupConvertor
    {
        public static Group ToEntity(this Model.Group group)
        {
            if (group == null)
            {
                return null;
            }

            return new Group
            {
                Id = group.Id,
                ReferenceId = group.ReferenceId,
                Name = group.Name,
                Email = group.Email,
                PhoneNumber = group.PhoneNumber,
                ContactPerson = group.ContactPerson,
                Address = group.Address,
                WebSiteUrl = group.WebSiteUrl,
                ExternalId = group.ExternalId,
                IsActive = group.IsActive,
                CreatedBy = group.CreatedBy,
                ModifiedBy = group.ModifiedBy,
                CreatedDate = group.CreatedDate,
                ModifiedDate = group.ModifiedDate
            };
        }

        public static Model.Group ToModel(this Group group)
        {
            if (group == null)
            {
                return null;
            }

            return new Model.Group
            {
                Id = group.Id,
                ReferenceId = group.ReferenceId,
                Name = group.Name,
                Email = group.Email,
                PhoneNumber = group.PhoneNumber,
                ContactPerson = group.ContactPerson,
                Address = group.Address,
                WebSiteUrl = group.WebSiteUrl,
                ExternalId = group.ExternalId,
                IsActive = group.IsActive,
                CreatedBy = group.CreatedBy,
                ModifiedBy = group.ModifiedBy,
                CreatedDate = group.CreatedDate,
                ModifiedDate = group.ModifiedDate
            };
        }
    }
}
