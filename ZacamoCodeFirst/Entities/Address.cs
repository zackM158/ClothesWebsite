using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Please Enter A House Number"), Display(Name = "House Number"), Range(0, int.MaxValue, ErrorMessage = "Please Enter A Positive Number")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Please Enter A Street Name"), Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Please Enter A City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter A Post Code")]
        public string Postcode { get; set; }
    }
}
