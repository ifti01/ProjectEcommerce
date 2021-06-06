using FinalProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Database;
using System.Data.Entity;

namespace FinalProject.Services
{
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();

                return instance;
            }
        }
        private static ProductsService instance { get; set; }
        public ProductsService()
        {
        }



        #endregion

        public int GetMaximumPrice()
        {
            using (var context = new CBContext())
            {
                // Perform data access using the context
                return (int) (context.Products.Max(x => x.Price));
                
            }
        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID,int? sortBy)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x=>x.Name.ToLower().Contains(searchTerm.ToLower())).ToList() ;
                    
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= minimumPrice).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;

                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;

                        case 4:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    } 
                }

                return products;
            }
        }

        //create communication between a controller and repository layer.
        // The service layer contains business logic.
        // In particular, it contains validation logic.
        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(p=>p.ID==ID).Include(p=>p.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo)
        {

            int pageSize = 5;

            //for showing product category in productindex

            using (var context = new CBContext())
            {
                return context.Products.
                    OrderBy(x=>x.ID)
                    .Skip((pageNo - 1) * pageSize).Take(pageSize)
                    .Include(x=> x.Category).ToList();
            }
        }
        public List<Product> GetProducts(int pageNo,int pageSize)
        {
           //for showing product category in productindex

            using (var context = new CBContext())
            {
                return context.Products.
                    OrderByDescending(x=>x.ID)
                    .Skip((pageNo - 1) * pageSize).Take(pageSize)
                    .Include(x=> x.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryID,int pageSize)
        {
            //for showing product category in productindex

            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.ID == categoryID).
                    OrderByDescending(x => x.ID)
                    .Take(pageSize)
                    .Include(x => x.Category).ToList();
            }
        }
        public List<Product> GetLatestProducts(int numberofproducts)
        {

            //for showing product category in productindex

            using (var context = new CBContext())
            {
                return context.Products.
                    OrderByDescending(x=>x.ID)
                    .Take(numberofproducts)
                    .Include(x=> x.Category).ToList();
            }
        }

        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                //category entry dile jate same category repeat na kore 
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;


                context.Products.Add(product);
                context.SaveChanges();

            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                //context.Categories.Remove(category);
                context.SaveChanges();

            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                var product = context.Products.Find(ID);

                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                
                context.Products.Remove(product);
                context.SaveChanges();

            }
        }
    }
}
