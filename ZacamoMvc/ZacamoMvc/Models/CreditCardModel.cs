using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities;

namespace ZacamoMvc.Models
{
    public class CreditCardModel
    {
        [Required(ErrorMessage = "Please Enter Bill Payers First Name"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Bill Payers Last Name"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter The Delivery Address"), Display(Name = "Delivery Address")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Please Enter Your Card Number"), Display(Name = "Card Number"), DataType(DataType.CreditCard)]
        public int CardNumber { get; set; }

        [Required(ErrorMessage = "Please Enter The Expiry Date"), Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please Enter Your Security Number"), Display(Name = "Security Number"), DataType(DataType.CreditCard)]
        public int SecurityNumber { get; set; }
    }
}