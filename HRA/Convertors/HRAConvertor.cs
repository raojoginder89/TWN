using HRA.Contracts;
using Models = HRA.Common.Models;

namespace HRA.Convertors
{
    public static class HRAConvertor
    {
        public static HRADetails ToContract(this Models.HRADetails hraDetail, string groupId)
        {
            if (hraDetail == null)
            {
                return null;
            }

            return new HRADetails
            {
                SavedForm = hraDetail.ExtraInfo,
                GroupId = groupId,
                Ssn = hraDetail.Ssn
            };
        }

        public static Models.HRADetails ToModel(this HRADetails hraDetail, int groupId)
        {
            if (hraDetail == null)
            {
                return null;
            }
            
            return new Models.HRADetails
            {
                Ssn = hraDetail.Ssn,
                ExtraInfo = hraDetail.SavedForm,
                GroupId = groupId
            };
        }
    }
}
