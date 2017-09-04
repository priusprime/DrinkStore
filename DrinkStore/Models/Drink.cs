using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Models
{
    public class Drink
    {
        public int DrinkID { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Title { get; set; }

        public int StyleID { get; set; }

        public virtual Style Style { get; set; }

        [Required]
        [Range(0.01, 100.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name ="Drink Art Url")]
        [StringLength(1024)]
        public string DrinkArtUrl { get; set; }
       
        [BindNever]
        [Required]
        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }

        public Drink()
        {
            Created = DateTime.UtcNow;
        }
    }
}
