using elcubo9.api.ActionFilter;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace elcubo9.api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
        }
        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            filters.Add(new RuleExceptionHandlingAttribute());
            filters.Add(new ElmahHandledErrorLoggerFilter());
        }
    }
}
