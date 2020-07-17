using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KadcoPortal.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }


    }
}