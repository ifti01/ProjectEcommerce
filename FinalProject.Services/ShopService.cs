using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Database;
using FinalProject.Entities;

namespace FinalProject.Services
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }
        private static ShopService instance { get; set; }
        public ShopService()
        {

        }



        #endregion

        public int SaveOrder(Order order)
        {
            using (var context = new CBContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }
    }
}
