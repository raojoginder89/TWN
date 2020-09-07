using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRA.DAL.Entity
{
    public partial class Group: BaseEntity
    {
        public Group()
        {
            HraDetails = new HashSet<HraDetails>();
        }

        [Column(Order = 1)]
        public Guid ReferenceId { get; set; }

        [Column(Order = 2)]
        public string Name { get; set; }

        [Column(Order = 3)]
        [StringLength(256)]
        public string Email { get; set; }

        [Column(Order = 4)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Column(Order = 5)]
        [StringLength(400)]
        public string ContactPerson { get; set; }

        [Column(Order = 6)]
        public string Address { get; set; }

        [Column(Order = 7)]
        [StringLength(400)]
        public string WebSiteUrl { get; set; }

        [Column(Order = 8)]
        [StringLength(100)]
        public string ExternalId { get; set; }

        [Column(Order = 9)]
        public bool IsActive { get; set; }

        public virtual ICollection<HraDetails> HraDetails { get; set; }
    }
}
