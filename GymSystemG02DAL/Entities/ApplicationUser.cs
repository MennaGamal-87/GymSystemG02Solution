using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Entities
{
    //IdentityUser is a class provided by ASP.NET Core Identity that represents a user in the identity system.
    /// <summary>
    /// /It includes properties such as UserName, Email, PasswordHash, etc. By inheriting from IdentityUser class by adding additional properties specific to the application's needs
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
