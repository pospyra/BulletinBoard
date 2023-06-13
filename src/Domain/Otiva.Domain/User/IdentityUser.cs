using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Domain.User
{
    public class IdentityUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public DateTime DateRegistration { get; set; } = DateTime.UtcNow;
    }
}
