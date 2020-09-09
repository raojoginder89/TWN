namespace HRA.Contracts
{
    public class UserToken
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public string[] Roles { get; set; }

        public string GroupId { get; set; }

        public string Ssn { get; set; }

        public bool IsHRACompleted { get; set; }
    }
}
