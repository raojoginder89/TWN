using System;
using System.Collections.Generic;
using System.Text;
using HRA.Common.Enums;

namespace HRA.Common.Models
{
    public class VideoWithUserDetails: BaseModel
    {
        public Guid ReferenceId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public DateTime? CompletedOn { get; set; }

        public UserVideoStatus Status { get; set; }
    }
}
