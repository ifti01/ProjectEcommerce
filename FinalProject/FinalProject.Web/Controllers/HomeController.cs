using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Services;
using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoriesService categoryService = new CategoriesService();
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.FeaturedCategories = categoryService.GetFeaturedCategories();

            return View(model); //model sent to view
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}