using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyHero.Data;
using Microsoft.EntityFrameworkCore;
using MyHero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Options;

namespace MyHero.Controllers
{
    public class UserController
    {
        private ApplicationDbContext dbcontext;
        private NotificationController notification;

        public UserController(ApplicationDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public UserController(ApplicationDbContext _dbcontext, IOptions<NotificationSettings> notificationSettingsAccessor)
        {
            dbcontext = _dbcontext;
            notification = new NotificationController(notificationSettingsAccessor);
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

        public Task<bool> SendRequest(int requesterId, int heroId)
        {
            try
            {
                var requester = dbcontext.Requestor.Include(r => r.User).Where(r => r.Id == requesterId).FirstOrDefault();
                var hero = dbcontext.Hero.Include(h => h.User).Where(h => h.Id == heroId).FirstOrDefault();

                var request = new Request()
                {
                    Requestor = requester,
                    Hero = hero,
                    DateRequested = DateTime.Now,
                    Status = 0,
                    Description = "Test Description"
                };

                dbcontext.Request.Add(request);

                notification.SendRequestAcceptedEmailAsync(request.Requestor.User.FirstName + " " + request.Requestor.User.LastName, request.Requestor.User.Email, request.Hero.User.FirstName + " " + request.Hero.User.LastName, request.Hero.User.Email);

                dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Task.FromResult(true);
        }

        public List<Request> GetRequests(int requesterId)
        {
            var requests = dbcontext.Request.Include(r => r.Hero)
                .Include(r => r.Requestor)
                .Include(r => r.Hero.User)
                .Where(r => r.Requestor.Id == requesterId).Select(r => r).ToList();
            return requests;
        }

        public void deleteHeroRequestor(ApplicationUser user)
        {
            Hero h = dbcontext.Hero.SingleOrDefault(a => a.User == user);
            if(h != null)
            {
                dbcontext.Remove(h);
            }
            Requestor r = dbcontext.Requestor.SingleOrDefault(a => a.User == user);
            if(r != null)
            {
                dbcontext.Remove(r);
            }
            dbcontext.SaveChanges();
        }
    }
}
