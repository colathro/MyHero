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
    public class HeroController 
    {
        private ApplicationDbContext dbcontext;

        public HeroController(ApplicationDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public ICollection<Hero> GetHeros(ApplicationUser _user)
        {
            // Fetch User Location

            // Custom query to return heros in radius

            //return heros

            throw new NotImplementedException();
        }
    }
}
