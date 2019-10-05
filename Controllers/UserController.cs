using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyHero.Data;
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
        public Task<RequesterModel> GetRequesterAsync(int requesterId)
        {
            var requester = dbcontext.Requestor.Where(r =>  r.Id == requesterId).Select(r => new RequesterModel()).FirstOrDefault();
            return Task.FromResult(requester);
        }

        public Task<RequesterModel> GetHeroAsync(int heroId)
        {
            var requester = dbcontext.Hero.Where(r => r.Id == heroId).Select(r => new RequesterModel()).FirstOrDefault();
            return Task.FromResult(requester);
        }
    }
}
