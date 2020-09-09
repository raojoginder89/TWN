using System.ComponentModel.DataAnnotations;

namespace HRA.Contracts
{
    public class HRADetails
    {
        public string GroupId { get; set; }

        public string SavedForm { get; set; }

        public string Ssn { get; set; }
    }

    public class Questions
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
