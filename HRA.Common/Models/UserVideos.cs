using HRA.Common.Enums;
using System;

namespace HRA.Common.Models
{
    public class UserVideos : BaseModel
    {
        public string UserId { get; set; }

        public int VideoId { get; set; }

        public Guid VideoReferenceId { get; set; }

        public DateTime? CompletedOn { get; set; }

        public UserVideoStatus Status { get; set; }
    }
}
