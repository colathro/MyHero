using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHero.Data
{
    public class Requestor
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
    }
}
