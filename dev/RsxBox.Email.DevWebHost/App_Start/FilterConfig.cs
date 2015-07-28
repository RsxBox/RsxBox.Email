using System.Web;
using System.Web.Mvc;

namespace RsxBox.Email.DevWebHost
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
