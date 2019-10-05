using System;
using System.ComponentModel.DataAnnotations;

namespace MyHero.Data
{
    public class HeroPhotos
    {
        [Key]
        public int Id { get; set; }

        public Photos Photos { get; set; }

        public Hero Hero { get; set; }
    }
}
