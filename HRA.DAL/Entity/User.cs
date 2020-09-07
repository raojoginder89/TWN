using HRA.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRA.DAL.Entity
{
    public class User : IdentityUser
    {
        public User()
        {
            UserVideos = new HashSet<UserVideos>();
        }

        public int GroupId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        [Required]
        [StringLength(300)]
        public string Ssn { get; set; }

        public Gender Gender { get; set; }

        [StringLength(100)]
        public string ExternalId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<UserVideos> UserVideos { get; set; }
    }
}
