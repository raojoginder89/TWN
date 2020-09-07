using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRA.Contracts
{
    public class HRADetails
    {
        public string GroupId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial { get; set; }

        public DateTime Date { get; set; } //purpose of this field

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string Ssn { get; set; }

        public string Language { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        [Required]
        public string Gender { get; set; } // change it to enum

        [Required]
        public string Email { get; set; }

        public string AlternateEmail { get; set; }

        [Required]
        public string ApartmentNumber { get; set; }

        [Required]
        public string MailingAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public IEnumerable<Questions> CurrentDiagnosis { get; set; }

        public string OtherDiagnosis { get; set; }
        
        public string TextMessageConfirmation { get; set; }
        
        [Required]
        public string ContactMethodPreference { get; set; } // Convert to Enum

        [Required]
        public string CellPhone { get; set; }

        public string AlternateCellPhone { get; set; }

        [Required]
        public string BestTimeToCall { get; set; }

        public decimal HeightInInches { get; set; }

        public decimal HeightInFeet { get; set; }

        public int WeightInLbs { get; set; }

        public decimal BMI { get; set; }

        [Required]
        public DateTime MostRecentCheckupDate { get; set; }

        public IEnumerable<Questions> HealthDiagnosis { get; set; }

        public string Comments { get; set; }
    }

    public class Questions
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
