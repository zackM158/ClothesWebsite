using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }

        public int StockAmount { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

    }
}
