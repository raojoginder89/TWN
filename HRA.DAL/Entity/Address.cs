using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRA.DAL.Entity
{
    public class Address : BaseEntity
    {
        [Column(Order = 1)]
        public string ApartmentNumber { get; set; }

        [Column(Order = 2)]
        public string MailingAddress { get; set; }
        
        [Column(Order = 3)]
        public string Landmark { get; set; }

        [Column(Order = 4)]
        [StringLength(200)]
        public string City { get; set; }

        [Column(Order = 5)]
        [StringLength(200)]
        public string State { get; set; }

        [Column(Order = 6)]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Column(Order = 7)]
        [StringLength(300)]
        public string Country { get; set; }

        [Required]
        [Column(Order = 8)]
        [StringLength(300)]
        public string Ssn { get; set; }

        [Column(Order = 9)]
        public int HraDetailsId { get; set; }

        [Column(Order = 10)]
        [StringLength(450)]
        public string UserId { get; set; }

        public virtual HraDetails HraDetail { get; set; }
    }
}
