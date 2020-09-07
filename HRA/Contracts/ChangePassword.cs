﻿using System.ComponentModel.DataAnnotations;

namespace HRA.Contracts
{
    public class ChangePassword
    {
        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
