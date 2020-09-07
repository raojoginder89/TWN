using HRA.Contracts;
using Models = HRA.Common.Models;

namespace HRA.Convertors
{
    public static class GroupConvertor
    {
        public static Group ToContract(this Models.Group group)
        {
            if (group == null)
            {
                return null;
            }

            return new Group
            {
                ReferenceId = group.ReferenceId.ToString(),
                Name = group.Name,
                Email = group.Email,
                PhoneNumber = group.PhoneNumber,
                ContactPerson = group.ContactPerson,
                Address = group.Address,
                WebSiteUrl = group.WebSiteUrl,
                //ExternalId = group.ExternalId,
                //IsActive = group.IsActive,
                //CreatedBy = group.CreatedBy,
                //ModifiedBy = group.ModifiedBy,
                CreatedDate = group.CreatedDate,
                ModifiedDate = group.ModifiedDate
            };
        }

        public static Models.Group ToModel(this Group group)
        {
            if (group == null)
            {
                return null;
            }

            return new Models.Group
            {
                Name = group.Name,
                Email = group.Email,
                PhoneNumber = group.PhoneNumber,
                ContactPerson = group.ContactPerson,
                Address = group.Address,
                WebSiteUrl = group.WebSiteUrl,
                //ExternalId = group.ExternalId,
            };
        }
    }
}
