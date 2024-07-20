using System.Web;
using System.Web.Mvc;

namespace Assign3Cumulative___Narmin_Gurbanli
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
