using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHero.Data
{ 
    public class SampleDataEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string ISOCode { get; set; }
    }
}
