using PRN221.Domain.Models;

namespace Application.Common
{
    public class AppConfig
    {
        public static ConnectionStrings ConnectionStrings { get; set; }
        //public static Admin Admin { get; set; } = new Admin();
        //public static Customer Customer { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
