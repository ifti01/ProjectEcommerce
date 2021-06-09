using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Services;
using FinalProject.Web.Code;
using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Controllers
{
    public class ShopController : Controller
    {
        //ProductsService productsService = new ProductsService();

        // GET: Shop

        public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy)
        {
            
            ShopViewModel model = new ShopViewModel();


            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories() ;

            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            model.Products = ProductsService.Instance.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryID,sortBy);

            model.SortBy = sortBy;

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {

            FilterProductsViewModel model = new FilterProductsViewModel(); 

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            return PartialView(model);
        }
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;

                //var ids = productIDs.Split('-');

                //List<int> pIDs = ids.Select(x =>int.Parse(x)).ToList();

                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIDs); //products that user wiibuy
            }

            return View(model);
        }
    }
}