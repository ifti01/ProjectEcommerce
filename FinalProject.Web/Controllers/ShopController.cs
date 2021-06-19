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
    [Authorize]
    public class ShopController : Controller
    {
        //ProductsService productsService = new ProductsService();

        // GET: Shop

        public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy,int? pageNo)
        {
            
            ShopViewModel model = new ShopViewModel();


            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories() ;

            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

           
            model.SortBy = sortBy;

            model.CategoryID = categoryID;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, 10);

            model.Pager = new Pager(totalCount,pageNo,10);

            

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int? pageNo)
        {

            FilterProductsViewModel model = new FilterProductsViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, 10);

            model.Pager = new Pager(totalCount, pageNo,10);


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