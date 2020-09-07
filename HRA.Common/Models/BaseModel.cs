using System;

namespace HRA.Common.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
