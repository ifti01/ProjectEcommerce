using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Entities;

namespace FinalProject.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
    }
}