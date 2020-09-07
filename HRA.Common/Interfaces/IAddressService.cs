using HRA.Common.Models;
using System.Threading.Tasks;

namespace HRA.Common.Interfaces
{
    public interface IAddressService
    {
        Task<Address> GetAddressByUserId(string userId);

        Task<Address> GetAddressBySsn(string ssn);

        Task<Address> AddAddress(Address address);

        Task<Address> GetAddressByHraDetailId(int hraDetailId);

        Task<Address> UpdateUserId(string userId, string ssn);

        Task<Address> UpdateAddressByUserId(string userId, Address address);

        Task<Address> UpdateAddressBySsn(string ssn, Address address);

        Task<Address> UpdateAddressByHraDetailId(int hraDetailId, Address address);

        Task DeleteAddressByUserId(string userId);

        Task DeleteAddressBySsn(string ssn);

        Task DeleteAddressByHraDetailId(int hraDetailId);
    }
}
