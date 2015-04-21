using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elcubo9.api
{
    public class APP
    {
        public static string FacebookAPPID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["app.facebook.AppId"];
            }
        }
        public static string FacebookAPPSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["app.facebook.AppSecret"];
            }
        }
    }
}