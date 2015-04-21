using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace elcubo9.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError(e);
        }

        protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError(e);
        }

        //Dimiss 404 errors for ELMAH
        private void FilterError(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                HttpException ex = (HttpException)e.Exception.GetBaseException();
                if (ex.Message.Contains("A potentially dangerous Request.Path value was detected from the client"))
                    e.Dismiss();
                else
                {
                    switch (ex.GetHttpCode())
                    {
                        case 404:
                            e.Dismiss();
                            break;
                        case 410:
                            e.Dismiss();
                            break;
                        case 406:
                            e.Dismiss();
                            break;
                    }
                }
            }
        }
    }
}
