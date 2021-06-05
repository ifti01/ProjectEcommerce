﻿using System;
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
        //ProductsService productsService = new ProductsService();
        //CategoriesService categoriesService=new CategoriesService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string Search,int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            ////similar to above
            //if(pageNo.HasValue)
            //{
            //    if(pageNo.Value > 0)
            //    {
            //        model.PageNo = pageNo.Value;
            //    }
            //    else
            //    {
            //        model.PageNo = 1;
            //    }
            //}
            //else
            //{
            //    model.PageNo = 1;
            //}

            model.Products = ProductsService.Instance.GetProducts(model.PageNo);

            if (string.IsNullOrEmpty(Search) == false)
            {
                model.SearchTerm = Search;
                model.Products = model.Products.Where(p => p.Name.ToLower().Contains(Search.ToLower())).ToList();

            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            //CategoriesService categoryService = new CategoriesService();

            //create object of Entity Product

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.ImageURL = model.ImageURL;
            //newProduct.CategoryID = model.CategoryID;
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            
            ProductsService.Instance.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsService.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();
            model.ImageURL = product.ImageURL;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);

            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            existingProduct.ImageURL = model.ImageURL;
            //existingProduct.CategoryID = model.CategoryID;

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsService.Instance.GetProduct(ID);

            return View(model);
        }




    }
}