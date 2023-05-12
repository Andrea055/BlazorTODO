using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_blazor_auth.Client.FormModel
{
    public class AccountRegistration
    {
        [Required]
        public string Email { get; set; } = null!;


        
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,32}$",
                    ErrorMessage = "Password doesn't meet security rules.")]
        public string Password { get; set; } = null!;



        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,32}$",
                    ErrorMessage = "Password doesn't meet security rules.")]
        public string PasswordConfirm { get; set; } = null!;

    }
}