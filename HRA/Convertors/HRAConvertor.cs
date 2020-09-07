using HRA.Common.Enums;
using HRA.Contracts;
using Newtonsoft.Json;
using System;
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

            var extraInfo = GetExtraInfo(hraDetail.ExtraInfo);

            return new HRADetails
            {
                GroupId = groupId,
                FirstName = hraDetail.FirstName,
                MiddleInitial = hraDetail.MiddleName,
                LastName = hraDetail.LastName,
                Dob = hraDetail.Dob,
                Ssn = hraDetail.Ssn,
                Email = hraDetail.Email,
                CellPhone = hraDetail.PhoneNumber,
                Gender = hraDetail.Gender.ToString(),

                //Dob = extraInfo.Dob,
                Language = extraInfo.Language,
                Department = extraInfo.Department,
                Position = extraInfo.Position,
                AlternateEmail = extraInfo.AlternateEmail,
                ApartmentNumber = extraInfo.ApartmentNumber,
                MailingAddress = extraInfo.MailingAddress,
                City = extraInfo.City,
                State = extraInfo.State,
                ZipCode = extraInfo.ZipCode,
                Country = extraInfo.Country,
                CurrentDiagnosis = extraInfo.CurrentDiagnosis,
                OtherDiagnosis = extraInfo.OtherDiagnosis,
                ContactMethodPreference = extraInfo.ContactMethodPreference,
                AlternateCellPhone = extraInfo.AlternateCellPhone,
                BestTimeToCall = extraInfo.BestTimeToCall,
                HeightInInches = extraInfo.HeightInInches,
                HeightInFeet = extraInfo.HeightInFeet,
                WeightInLbs = extraInfo.WeightInLbs,
                BMI = extraInfo.BMI,
                MostRecentCheckupDate = extraInfo.MostRecentCheckupDate,
                HealthDiagnosis = extraInfo.HealthDiagnosis,
                Comments = extraInfo.Comments
            };
        }

        private static HRADetails GetExtraInfo(string extraInfo)
        {
            if (string.IsNullOrWhiteSpace(extraInfo))
            {
                return new HRADetails();
            }

            return JsonConvert.DeserializeObject<HRADetails>(extraInfo);
        }

        public static Models.HRADetails ToModel(this HRADetails hraDetail, int groupId)
        {
            if (hraDetail == null)
            {
                return null;
            }

            Enum.TryParse(hraDetail.Gender, true, out Gender gender);

            return new Models.HRADetails
            {
                FirstName = hraDetail.FirstName,
                MiddleName = hraDetail.MiddleInitial,
                LastName = hraDetail.LastName,
                Dob = hraDetail.Dob,
                Ssn = hraDetail.Ssn,
                Email = hraDetail.Email,
                PhoneNumber = hraDetail.CellPhone,
                Gender = gender,
                ExtraInfo = JsonConvert.SerializeObject(hraDetail),
                GroupId = groupId
            };
        }

        public static Models.Address ToAddressModel(this HRADetails hraDetail, int hraDetailId)
        {
            if (hraDetail == null)
            {
                return null;
            }
            
            return new Models.Address
            {
                ApartmentNumber = hraDetail.ApartmentNumber,
                MailingAddress = hraDetail.MailingAddress,
                City = hraDetail.City,
                State = hraDetail.State,
                ZipCode = hraDetail.ZipCode,
                Country = hraDetail.Country,
                Ssn = hraDetail.Ssn,
                HraDetailsId = hraDetailId
            };
        }
    }
}
