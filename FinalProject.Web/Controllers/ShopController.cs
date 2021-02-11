﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Services;
using FinalProject.Web.ViewModels;

namespace FinalProject.Web.Controllers
{
    public class ShopController : Controller
    {
        //ProductsService productsService = new ProductsService();

        // GET: Shop
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