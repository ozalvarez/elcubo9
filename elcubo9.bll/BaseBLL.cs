using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elcubo9.data.Models;
using elcubo9.data.binding;
using elcubo9.bll.Exceptions;

namespace elcubo9.bll
{
    public class BaseBLL
    {
        public static string StorageUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["StorageUrl"];
            }
        }
        public static string FacebookAPPID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["app.facebook.AppId"];
            }
        }
        public static string FacebookAPPToken
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["app.facebook.AppToken"];
            }
        }
        public static string GoogleAPPID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["app.google.ClientID"];
            }
        }

        protected ApplicationDbContext db
        {
            get
            {
                var db = new ApplicationDbContext();
                return db;
            }
        }
        public static bool Can(int CustomerID, string UserID, ApplicationDbContext _c, CustomerUserType Rol)
        {
            var _Can = _c.CustomerUserRoles.Where(m => m.CustomerUserType == Rol
                   && m.UserID.Equals(UserID) && m.CustomerID == CustomerID).Any();
            if (_Can)
                return _Can;
            else throw new RuleException("Ese usuario no tiene permiso para manejar el menu");
        }
    }
}
