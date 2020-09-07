namespace HRA.Common.Models
{
    public class Address: BaseModel
    {
        public string ApartmentNumber { get; set; }

        public string MailingAddress { get; set; }

        public string Landmark { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string Ssn { get; set; }

        public int HraDetailsId { get; set; }

        public string UserId { get; set; }
    }
}
