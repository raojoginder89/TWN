using System;
using HRA.Common.Enums;

namespace HRA.Common.Models
{
    public class Videos : BaseModel
    {
        public Guid ReferenceId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }
    }
}
