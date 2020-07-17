using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities;

namespace ZacamoMvc.Models
{
    public class ProductWithImageModel
    {
        [Required(ErrorMessage = "Please Add A Product Image"), Display(Name = "Image")]
        public HttpPostedFileBase PostedFile { get; set; }

        [Required]
        public Product NewProduct { get; set; }
    }
}