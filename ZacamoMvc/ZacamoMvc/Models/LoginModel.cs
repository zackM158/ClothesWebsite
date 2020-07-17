using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZacamoMvc.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false), DataType(DataType.EmailAddress), Display(Name ="Email")]
        public string EmailAddress { get; set; }

        [Required(AllowEmptyStrings = false), DataType((DataType.Password))]
        public string Password { get; set; }
    }
}