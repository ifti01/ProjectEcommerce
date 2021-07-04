using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Entities;
using FinalProject.Web.Models;

namespace FinalProject.Web.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserID { get; set; }
        public Pager Pager { get; set; }
        public string Status { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
        public List<string> AvailableStatuses { get;  set; }
    }
}
