using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_blazor_auth.Shared.Models;

namespace Server.Security
{
    public static class FirewallTODO
    {
        public static bool IsTheSameAuthor (ApplicationUser User, Work Work) => User.Email == Work.Email;
    }
}