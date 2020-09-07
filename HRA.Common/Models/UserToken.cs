﻿namespace HRA.Common.Models
{
    public class UserToken
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }
        
        public string[] Roles { get; set; }

        public bool IsHRACompled { get; set; }
    }
}
