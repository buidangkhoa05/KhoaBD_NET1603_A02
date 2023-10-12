using Application.Common;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration
{
    public static class ConfigAppSettings
    {
        public static void BindingAppSettings(this IConfiguration configuration)
        {
            do
            {
                AppConfig.ConnectionStrings = new ConnectionStrings();
            }
            while (AppConfig.ConnectionStrings == null);

            configuration.Bind("ConnectionStrings", AppConfig.ConnectionStrings);
        }
    }
}
