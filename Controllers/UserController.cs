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

        public ApplicationUser GetUser(string _userid)
        {
            var user = dbcontext.User.Where(r => r.Id == _userid).FirstOrDefault();
            return user;
        }

        private Boolean NeedsToPopulateHero(ApplicationUser user)
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
        private Boolean NeedsToPopulateRequestor(ApplicationUser user)
        {
            if (dbcontext.User.Where(i => i.Id == user.Id).Include(e => e.Requestor).First().Requestor is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PopulateHeroRequestor(ApplicationUser user, bool hero, bool requestor)
        {
            bool needsUpdate = false;
            if (hero && this.NeedsToPopulateHero(user))
            {
                Hero hro = new Hero { Latitude = 1.00, Longitude = 1.00, Radius = 10 };
                user.Hero = hro;
                needsUpdate = true;
            }

            if (requestor && this.NeedsToPopulateRequestor(user))
            {
                Requestor rqstr = new Requestor { Latitude = 1.00, Longitude = 1.00 };
                user.Requestor = rqstr;
                needsUpdate = true;
            }

            if (needsUpdate)
            {
                dbcontext.SaveChanges();
            }
        }

        public Task<bool> SendRequest(int heroId)
        {
            //var request = new Request()
            //{
            //    Requestor = new Requestor()
            //    {
            //        r
            //    }
            //}

            return Task.FromResult(true);
        }
    }
}
