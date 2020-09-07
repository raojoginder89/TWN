using HRA.Common.Interfaces;
using HRA.Common.Models;
using HRA.DAL;
using HRA.DAL.Convertors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.BLL
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Address> AddAddress(Address address)
        {
            HRA.DAL.Entity.Address addressEntity = address.ToEntity();
            _unitOfWork.Add(addressEntity);
            await _unitOfWork.CommitAsync();
            return addressEntity.ToModel();
        }

        public async Task DeleteAddressByHraDetailId(int hraDetailId)
        {
            var address = GetByHraDetailsId(hraDetailId);
            if (address == null)
            {
                return;
            }

            address.IsDeleted = true;
            _unitOfWork.Update(address);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAddressBySsn(string ssn)
        {
            var address = GetBySsn(ssn);
            if (address == null)
            {
                return;
            }

            address.IsDeleted = true;
            _unitOfWork.Update(address);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAddressByUserId(string userId)
        {
            var address = GetByUserId(userId);
            if (address == null)
            {
                return;
            }

            address.IsDeleted = true;
            _unitOfWork.Update(address);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Address> GetAddressByHraDetailId(int hraDetailId)
        {
            return GetByHraDetailsId(hraDetailId).ToModel();
        }

        public async Task<Address> GetAddressBySsn(string ssn)
        {
            return GetBySsn(ssn).ToModel();
        }

        public async Task<Address> GetAddressByUserId(string userId)
        {
            return GetByUserId(userId).ToModel();
        }

        public async Task<Address> UpdateAddressByHraDetailId(int hraDetailId, Address address)
        {
            address.HraDetailsId = hraDetailId; 
            var addressInfo = GetByHraDetailsId(hraDetailId);
            if (addressInfo == null)
            {
                return null;
            }

            return await UpdateAddress(address, addressInfo);
        }

        public async Task<Address> UpdateAddressBySsn(string ssn, Address address)
        {
            address.Ssn = ssn;
            var addressInfo = GetBySsn(ssn);
            if (addressInfo == null)
            {
                return null;
            }

            return await UpdateAddress(address, addressInfo);
        }

        public async Task<Address> UpdateAddressByUserId(string userId, Address address)
        {
            address.UserId = userId;
            var addressInfo = GetByUserId(userId);
            if (addressInfo == null)
            {
                return null;
            }

            return await UpdateAddress(address, addressInfo);
        }

        public async Task<Address> UpdateUserId(string userId, string ssn)
        {
            var addressInfo = GetBySsn(ssn);
            if (addressInfo == null)
            {
                return null;
            }

            addressInfo.UserId = userId;
            _unitOfWork.Update(addressInfo);
            await _unitOfWork.CommitAsync();
            return addressInfo.ToModel();

        }

        private HRA.DAL.Entity.Address GetBySsn(string ssn)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.Address>().FirstOrDefault(x => x.Ssn.ToLower() == ssn.ToLower() && !x.IsDeleted);
        }

        private HRA.DAL.Entity.Address GetByUserId(string userId)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.Address>().FirstOrDefault(x => x.UserId.ToLower() == userId.ToLower() && !x.IsDeleted);
        }

        private HRA.DAL.Entity.Address GetByHraDetailsId(int hraDetailsId)
        {
            return _unitOfWork.Query<HRA.DAL.Entity.Address>().FirstOrDefault(x => x.HraDetailsId == hraDetailsId && !x.IsDeleted);
        }

        private async Task<Address> UpdateAddress(Address address, HRA.DAL.Entity.Address addressInfo)
        {
            var addressEntity = address.ToEntity();
            addressEntity.Id = addressInfo.Id;
            addressEntity.CreatedBy = addressInfo.CreatedBy;
            addressEntity.CreatedDate = addressInfo.CreatedDate;
            _unitOfWork.Update(addressEntity);
            await _unitOfWork.CommitAsync();
            return addressEntity.ToModel();
        }
    }
}
