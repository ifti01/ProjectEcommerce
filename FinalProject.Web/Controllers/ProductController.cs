using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Entities;
using FinalProject.Services;
using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string Search)
        {
            var products = productsService.GetProducts();

            if (string.IsNullOrEmpty(Search) == false)
            {
                products = products.Where(p => p.Name.ToLower().Contains(Search.ToLower())).ToList();

            }
            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoriesService categoryService = new CategoriesService();

            var categories = categoryService.GetCategories();

            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            CategoriesService categoryService = new CategoriesService();

            //create object of Entities

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            //newProduct.CategoryID = model.CategoryID;
            newProduct.Category = categoryService.GetCategory(model.CategoryID);
            
            productsService.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int? ID)
        {
            var product = productsService.GetProduct(ID);

            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsService.UpdateProduct(product);

            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsService.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }
    }
}