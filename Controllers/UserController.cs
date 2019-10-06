using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyHero.Data;
using Microsoft.EntityFrameworkCore;
using MyHero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHero.Controllers
{
    public class UserController 
    {
        private ApplicationDbContext dbcontext;

        public UserController(ApplicationDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public Requestor GetRequestor(string _userid)
        {
            var req = dbcontext.User.Where(r => r.Id == _userid).Include(e => e.Requestor).FirstOrDefault();
            return req.Requestor;
        }

        public Hero GetHero(string _userid)
        {
            var hero = dbcontext.User.Where(r => r.Id == _userid).Include(e => e.Hero).FirstOrDefault();
            return hero.Hero;
        }

        private Boolean NeedsToPopulate(ApplicationUser user)
        {
            if (dbcontext.User.Where(i => i.Id == user.Id).Include(e => e.Hero).First().Hero is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PopulateHeroRequestor(ApplicationUser user)
        {
            if (this.NeedsToPopulate(user))
            {
                Hero hero = new Hero { Latitude = 1.00, Longitude = 1.00, Radius = 10 };
                Requestor requestor = new Requestor { Latitude = 1.00, Longitude = 1.00 };

                user.Hero = hero;
                user.Requestor = requestor;

                dbcontext.SaveChanges();
            }
        }
    }
}
