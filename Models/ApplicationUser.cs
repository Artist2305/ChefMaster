using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AcconutName { get; set; }
        public string AvatarPhotoPatch { get; set; }
    }
}
