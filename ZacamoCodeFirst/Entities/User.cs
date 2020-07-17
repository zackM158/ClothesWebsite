using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter A First Name"), MaxLength(20), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter A Surname"), MaxLength(20), Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false), Display(Name = "Email"), MaxLength(250), DataType(DataType.EmailAddress), Index(IsUnique = true)]
        public string EmailAddress { get; set; }

        [Required(AllowEmptyStrings = false), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Salt { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool IsPremiumUser { get; set; }

        [Required, Display(Name = "Address")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Display(Name = "Saved Items")]
        public string SavedItems { get; set; }
    }
}
