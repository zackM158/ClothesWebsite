using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public class ManufacturerDto
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public int AmountOfProducts { get; set; }
    }
}
