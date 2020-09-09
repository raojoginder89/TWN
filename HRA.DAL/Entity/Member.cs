using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRA.DAL.Entity
{
    public class Member : BaseEntity
    {
        public Guid ReferenceId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [StringLength(300)]
        public string Ssn { get; set; }

        public int GroupId { get; set; }
    }
}
