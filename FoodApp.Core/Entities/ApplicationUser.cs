using FoodApp.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class ApplicationUser : IdentityUser
        {
        public string Fname { get; set; }

        public string Lname { get; set; }
        
        public Role roles { get; set; } 
    }
}
