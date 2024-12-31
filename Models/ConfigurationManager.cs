using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Class dùng để đọc các thông tin cấu hình từ file appsettings.json
    /// </summary>
    public class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _instance =
       new Lazy<ConfigurationManager>(() => new ConfigurationManager());

        public static ConfigurationManager Instance => _instance.Value;

        public IConfiguration Configuration { get; }

        private ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // Get ZaloPay configuration
        public string GetAppId() => Configuration["ZaloPay:AppId"];
        public string GetSecretKey1() => Configuration["ZaloPay:Key1"];
        public string GetSecretKey2() => Configuration["ZaloPay:Key2"];
        public string GetEndpoint() => Configuration["ZaloPay:Endpoint"];
        public string GetCreateOrderUrl() => Configuration["ZaloPay:CreateOrderUrl"];
        public string GetQueryOrderUrl() => Configuration["ZaloPay:QueryOrderUrl"];

        // Get connection string
        public string GetConnectionString() => Configuration["ConnectionStrings:DefaultConnection"];
    }
}
