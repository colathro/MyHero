using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyHero.Data;

namespace MyHero.Data.Demo
{
    public class demo
    {
        public ApplicationDbContext Context;
        public demo(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public void init()
        {
            ApplicationUser u1 = new ApplicationUser { UserName = "Bob Saget", Email = "BobSaget@gmail.com" };
            Context.User.Add(u1);
            Requestor r1 = new Requestor { User = u1, Longitude = -96.7870939, Latitude = 46.8778118, Location = "Black Coffee and Waffle Bar, 2nd Avenue North, Fargo, ND, USA", Description = "Hi, my son has been diagnosted with cancer and needs a hero to improve his spirits. He would love it if he got to meet a real life firefighter." };
            Context.Requestor.Add(r1);
            ApplicationUser u3 = new ApplicationUser { UserName = "Bilbo Baggins", Email = "BilboBaggins@gmail.com" };
            Context.User.Add(u3);
            Hero h1 = new Hero { User = u3, Longitude = -96.78980339999998, Latitude = 46.8771863, Location = "Fargo, ND, USA", Phone = "701-123-4567", Tags = "Wizard;Spider Man;Deadpool", Description = "Hi, I'm Bilbo Baggins. I'm from Middle Earth and am new to Fargo. Looking to do some community service in the area.", Radius = 50 };
            Context.Hero.Add(h1);
            Photos p1 = new Photos { Image = "Spiderman" };
            Context.Photos.Add(p1);
            HeroPhotos hp1 = new HeroPhotos { Photos = p1, Hero = h1 };
            Context.HeroPhotos.Add(hp1);
            ApplicationUser u2 = new ApplicationUser { UserName = "Paul Hewson", Email = "Bono@gmail.com" };
            Context.User.Add(u2);
            Requestor r2 = new Requestor { User = u2, Longitude = -96.7870939, Latitude = 46.8778118, Location = "Black Coffee and Waffle Bar, 2nd Avenue North, Fargo, ND, USA", Description = "Hi, my son has been diagnosted with cancer and needs a hero to improve his spirits. He would love it if he got to meet a real life firefighter." };
            Context.Requestor.Add(r2);



            Context.SaveChanges();

        }

    }
}