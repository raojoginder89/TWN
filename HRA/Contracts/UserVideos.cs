using HRA.Contracts.Enums;
using System;

namespace HRA.Contracts
{
    public class UserVideos
    {
        public Guid VideoReferenceId { get; set; }

        public DateTime? CompletedOn { get; set; }

        public UserVideoStatus Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
