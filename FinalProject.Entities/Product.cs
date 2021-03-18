using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Range(1,10000)]
        public decimal Price { get; set; }
    }
}
