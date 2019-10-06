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
            Requestor r1 = new Requestor { User = u1, Longitude = -1, Latitude = 1, Location = "Black Coffee and Waffle Bar, 2nd Avenue North, Fargo, ND, USA", Description = "Hi, my son has been diagnosted with cancer and needs a hero to improve his spirits. He would love it if he got to meet a real life firefighter." };
            Context.Requestor.Add(r1);
            ApplicationUser u3 = new ApplicationUser { UserName = "Bilbo Baggins", Email = "BilboBaggins@gmail.com" };
            Context.User.Add(u3);
            Hero h1 = new Hero { User = u3, Longitude = -1, Latitude = 1, Location = "Fargo, ND, USA", Phone = "701-123-4567", Tags = "Wizard;Spider Man;Deadpool", Description = "Hi, I'm Bilbo Baggins. I'm from Middle Earth and am new to Fargo. Looking to do some community service in the area.", Radius = 50 };
            Context.Hero.Add(h1);
            Photos p1 = new Photos { Image = "Spiderman" };
            Context.Photos.Add(p1);
            HeroPhotos hp1 = new HeroPhotos { Photos = p1, Hero = h1 };
            Context.HeroPhotos.Add(hp1);
            ApplicationUser u2 = new ApplicationUser { UserName = "Paul Hewson", Email = "Bono@gmail.com" };
            Context.User.Add(u2);
            Requestor r2 = new Requestor { User = u2, Longitude = -1, Latitude = 1, Location = "Black Coffee and Waffle Bar, 2nd Avenue North, Fargo, ND, USA", Description = "Hi, my son has been diagnosted with cancer and needs a hero to improve his spirits. He would love it if he got to meet a real life firefighter." };
            Context.Requestor.Add(r2);

            int lng = -16903;
            int lat = 08177;
            String[] tags = {"Deadpool", "Fire Fighter", "Spider Man", "Princess", "Black Panther", "Superman", "Wonder Woman", "Policeman", "Doctor", "Antman", "Aquaman", "Green Lantern", "Wizard"};
            String[] names = { "Bob", "Alice", "Tom", "Cindy", "Mohammed", "Alex", "Charles", "Dale", "Daniel", "Jonathon", "Johnson", "Akshay" };
            String[] lastn = {"Johnson", "Wilson", "Dantas", "Panthrope", "Forest", "Danielson", "Brown", "Miller", "Thompson"};
            for (int i = 0; i < 50; i++)
            {
                Random rnd = new Random();
                var first = names[rnd.Next(0, names.Length - 1)];
                var last = lastn[rnd.Next(0, lastn.Length - 1)];
                double rlat = (rnd.Next((int)lat-371,(int)lat+371)+0.0)/10000.0;
                double rlng = (rnd.Next((int)lng-253,(int)lng+253)+0.0)/10000.0;
                String name = first + "." + last;
                string tagList;
                if(rnd.Next(0,2) == 1)
                {
                    tagList = tags[rnd.Next(0,tags.Length-1)];
                }
                else
                {
                    tagList = tags[rnd.Next(0,8)] + ";" + tags[rnd.Next(8,tags.Length-1)];
                }
                ApplicationUser ru = new ApplicationUser { UserName = name, Email = name + "@gmail.com", FirstName=first, LastName=last };
                Context.User.Add(ru);
                Hero rh = new Hero { User = ru, Longitude = rlng, Latitude = rlat, Location = "Fargo, ND, USA", Phone = "701-321-4567", Tags = tagList, Description = "Hi, I'm "+ name + ". I'm new to Fargo. Looking to do some community service in the area.", Radius = 50 };
                Context.Hero.Add(rh);
                first = names[rnd.Next(0, names.Length - 1)];
                last = lastn[rnd.Next(0, lastn.Length - 1)];
                name = first + "." + last;
                rlat = (rnd.Next((int)lat-371,(int)lat+371)+0.0)/10000.0;
                rlng = (rnd.Next((int)lng-253,(int)lng+253)+0.0)/10000.0;
                ApplicationUser ru2 = new ApplicationUser { UserName = name, Email = name + "@gmail.com", FirstName=first, LastName=last  };
                Context.User.Add(ru2);
                Requestor rr = new Requestor { User = ru2, Longitude = rlng, Latitude = rlat, Location = "Black Coffee and Waffle Bar, 2nd Avenue North, Fargo, ND, USA", Description = "Hi, my son has been diagnosted with cancer and needs a hero to improve his spirits. He would love it if he got to meet a real life firefighter." };
                Context.Requestor.Add(rr);
            }


            Context.SaveChanges();

        }

    }
}