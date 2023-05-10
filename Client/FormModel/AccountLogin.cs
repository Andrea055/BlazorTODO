using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_blazor_auth.Client.FormModel
{
    public class AccountLogin
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}