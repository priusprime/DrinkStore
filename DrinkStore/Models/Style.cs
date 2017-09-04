using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Models
{
    public class Style
    {
        public int StyleID { get; set; }

        [Required]
        [StringLength(160,MinimumLength =2)]
        public string Title { get; set; }

        public List<Drink> Drinks { get; set; }
    }
}
