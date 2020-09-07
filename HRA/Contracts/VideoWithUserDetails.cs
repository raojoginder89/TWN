using HRA.Contracts.Enums;
using System;

namespace HRA.Contracts
{
    public class VideoWithUserDetails
    {
        public Guid ReferenceId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public DateTime? CompletedOn { get; set; }

        public UserVideoStatus Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
