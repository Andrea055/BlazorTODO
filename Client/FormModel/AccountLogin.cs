using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_blazor_auth.Client.FormModel
{
    public class AccountLogin
    {
        [Required]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid email address.")]

        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}