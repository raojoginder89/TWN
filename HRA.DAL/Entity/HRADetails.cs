using HRA.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRA.DAL.Entity
{
    public class HraDetails : BaseEntity
    {
        public HraDetails()
        {
            Addresses = new HashSet<Address>();
        }

        [Column(Order = 1)]
        public string FirstName { get; set; }

        [Column(Order = 2)]
        public string MiddleName { get; set; }

        [Column(Order = 3)]
        public string LastName { get; set; }

        [Column(Order =4, TypeName = "datetime2")]
        public DateTime Dob { get; set; }

        [Required]
        [StringLength(300)]
        [Column(Order = 5)]
        [Index("INDEX_SSN", IsClustered = true, IsUnique = true)]
        public string Ssn { get; set; }

        [Column(Order = 6)]
        public string Email { get; set; }

        [Column(Order = 7)]
        public string PhoneNumber { get; set; }

        [Column(Order = 8)]
        public Gender Gender { get; set; }

        [Column(Order = 9)]
        public string ExtraInfo { get; set; }

        [Column(Order = 10)]
        public int GroupId { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual Group Group { get; set; }
    }
}
