using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter A Name", AllowEmptyStrings = false), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please Enter A Price"), Range(0, double.MaxValue, ErrorMessage = "Please enter a positive number")]
        public double Price { get; set; }

        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        [Required(ErrorMessage = "Please Enter An Amount Of Stock", AllowEmptyStrings = false), Display(Name = "Stock"), Range(0, int.MaxValue, ErrorMessage = "Please Enter A Positive Number")]
        public int StockAmount { get; set; }

        [Required, Display(Name="Image"), RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg|.png)$", ErrorMessage = "Please Enter An Image In The Form Of .jpg Or .png")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Please Enter A Product Description"), MaxLength(500)]
        public string Description { get; set; }
    }
}
