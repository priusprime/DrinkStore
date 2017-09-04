﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStore.Models
{
    public class Order
    {
        [BindNever]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [Required]
        [StringLength(160)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [StringLength(160)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(70,MinimumLength =3)]
        public string Address { get; set; }

        [Required]
        [StringLength(40)]
        public string City { get; set; }

        [Required]
        [StringLength(40)]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(10, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(40)]
        public string Country { get; set; }

        [Required]
        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindNever]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
