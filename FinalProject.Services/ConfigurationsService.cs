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
        public Config GetConfig(string key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(key);
            }

        }
    }
}
