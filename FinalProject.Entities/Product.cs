using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class Product:BaseEntity
    {
        //public int CategoryID { get; set; }
        public  virtual Category Category { get; set; }

        public string ImageURL { get; set; }
        public decimal Price { get; set; }
    }
}
