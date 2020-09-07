using HRA.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRA.DAL.Entity
{
    public class UserVideos : BaseEntity
    {
        [StringLength(450)]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Column(Order = 2)]
        public int VideoId { get; set; }
        [Column(Order = 3)]
        public UserVideoStatus Status { get; set; }

        [Column(Order = 4, TypeName = "datetime2")]
        public DateTime? CompletedOn { get; set; }

        public virtual User User { get; set; }

        public virtual Videos Video { get; set; }
    }
}
