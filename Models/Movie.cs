using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id{ get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name{ get; set; }

        public Genre Genre { get; set; }
        public byte GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        public DateTime? Date { get; set; }
        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1,20)]
        public int NumberInStock { get; set; }

    }
}