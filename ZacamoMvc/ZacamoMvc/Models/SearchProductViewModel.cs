using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZacamoMvc.Models
{
    public class SearchProductViewModel
    {
        [Required, Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}