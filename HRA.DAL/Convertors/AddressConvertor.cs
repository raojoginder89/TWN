using HRA.DAL.Entity;
using Model = HRA.Common.Models;

namespace HRA.DAL.Convertors
{
    public static class AddressConvertor
    {
        public static Address ToEntity(this Model.Address address)
        {
            if (address == null)
            {
                return null;
            }

            return new Address
            {
                ApartmentNumber = address.ApartmentNumber,
                MailingAddress = address.MailingAddress,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                Ssn = address.Ssn,
                HraDetailsId = address.HraDetailsId,
                UserId = address.UserId
            };
        }

        public static Model.Address ToModel(this Address address)
        {
            if (address == null)
            {
                return null;
            }

            return new Model.Address
            {
                Id = address.Id,
                ApartmentNumber = address.ApartmentNumber,
                MailingAddress = address.MailingAddress,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                Ssn = address.Ssn,
                HraDetailsId = address.HraDetailsId,
                UserId = address.UserId,
                CreatedBy = address.CreatedBy,
                ModifiedBy = address.ModifiedBy,
                CreatedDate = address.CreatedDate,
                ModifiedDate = address.ModifiedDate
            };
        }
    }
}
