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
            double? lat = _user.Requestor.Latitude;
            double? lng = _user.Requestor.Longitude;
            FormattableString sql = $@"with c as (Select *, (Longitude - {lng}) * COS(({lat} + Latitude) / 2) as A, Latitude - {lat} as B From Hero Where Latitude < {lat} + 0.0253 and Latitude > {lat} - 0.0253 and Longitude < {lng} + 0.0371 and Longitude > {lng} - 0.0371) Select * From C Where SQRT(A * A + B * B) * 3958.8 < Radius";
            return dbcontext.Hero.FromSqlInterpolated(sql).ToList();
            // Custom query to return heros in radius

            //return heros
        }
    }
}
