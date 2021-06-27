using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Database;
using FinalProject.Entities;

namespace FinalProject.Services
{
    public class ConfigurationsService
    {
        #region Singleton

        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationsService();
                {
                    return instance;
                }
            }
        }
        private static ConfigurationsService instance { get; set; }

        private ConfigurationsService()
        {

        }

        #endregion
        public Config GetConfig(string Key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(Key);
            }

        } 
        public int Pagesize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("Pagesize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }

        } 
        public int ShopPagesize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPagesize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }

        }
    }
}
