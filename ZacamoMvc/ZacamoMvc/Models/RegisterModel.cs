using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZacamoMvc.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter A First Name"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter A Last Name"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter An Email"), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter A House Number"), Display(Name = "House Number"), Range(1, int.MaxValue, ErrorMessage = "Please Enter A Positive Number")]
        public int HouseNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter A Street Name"), MaxLength(20), Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter A City"), MaxLength(20)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter A Postcode"), MaxLength(10)]
        public string Postcode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter A Password"), DataType(DataType.Password), MinLength(6, ErrorMessage = "Minimum Length 6"), RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,20})$", ErrorMessage = "Must Be At Least 6 Characters,\nOne Uppercase Letter,\n One Lowercase Letter,\n One Numeric Character")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Confirm Your Password"), Display(Name = "Confirm Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}