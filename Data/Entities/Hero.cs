using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MyHero.Data
{
    public class Hero
    {
        [Key]
        public int Id { get; set; }

        public string Location { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Phone { get; set; }

        public string Tags { get; set; }

        public string Description { get; set; }

        public int Radius { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<HeroPhotos> HeroPhotos { get; set; }
    }
}
