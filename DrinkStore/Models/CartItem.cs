using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required()]
        public string CartId { get; set; }
        public int DrinkID { get; set; }
        public int Count { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public virtual Drink Drink { get; set; }
    }
}
