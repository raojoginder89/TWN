using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRA.DAL.Entity
{
    public class Videos: BaseEntity
    {
        public Videos()
        {
            UserVideos = new HashSet<UserVideos>();
        }

        [Column(Order = 1)]
        public Guid ReferenceId { get; set; }

        [Column(Order = 2)]
        public string Url { get; set; }

        [Column(Order = 3)]
        public string Title { get; set; }

        [Column(Order = 4)]
        public string Description { get; set; }

        [Column(Order = 5)]
        public string ExtraInfo { get; set; }

        public virtual ICollection<UserVideos> UserVideos { get; set; }
    }
}
