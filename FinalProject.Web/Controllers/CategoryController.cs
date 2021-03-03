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
    public class CategoryController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();

        // GET: Category
        //[HttpGet]
        //public ActionResult Create(int ID)
        //{
        //    return View();
        //}


        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();


            model.SearchTerm = search;
            model.Categories = CategoriesService.Instance.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                
                model.Categories = model.Categories.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();

            }
            return PartialView(model);
        }

        #region Created

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
            //return PartialView(Index());
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            //create object of Entities

            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.isFeatured = model.isFeatured;
            newCategory.ImageURL = model.ImageURL;

            CategoriesService.Instance.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");
        }

    #endregion
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }

        #region Updation

        
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();


            var category = CategoriesService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesService.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            CategoriesService.Instance.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoriesService.Instance.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var category = CategoriesService.Instance.GetCategory(ID); 
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoriesService.Instance.DeleteCategory(category.ID);
            return RedirectToAction("Index");
        }

    }
}