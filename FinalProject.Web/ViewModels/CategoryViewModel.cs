using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FinalProject.Entities;



namespace FinalProject.Web.ViewModels
{
    public class NewCategoryViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int CategoryID { get; set; }
        //new added
        public string ImageURL { get; set; } 
        public bool isFeatured { get; set; }
    }

    public class CategorySearchViewModel
    {
        public string SearchTerm { get; set; } 
        public List<Category> Categories { get; set; }
        public Pager Pager { get; set; }
    }

    public class EditCategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public Decimal Price { get; set; }
        public int ID { get; set; }
        //new added
        public string ImageURL { get; set; }
        public bool isFeatured { get; set; }

    }
}
