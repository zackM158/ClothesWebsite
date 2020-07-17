using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please Enter A Name", AllowEmptyStrings = false), MaxLength(20), Display(Name = "Manufacturer")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter A Website", AllowEmptyStrings = false)]
        public string Website { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
