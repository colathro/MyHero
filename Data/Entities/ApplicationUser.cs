using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyHero.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomTag { get; set; }
    }
}