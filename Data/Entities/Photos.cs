using System;
using System.ComponentModel.DataAnnotations;

namespace MyHero.Data
{
    public class Photos
    {
        [Key]
        public int Id { get; set; }

        public string Image { get; set; }
    }
}
