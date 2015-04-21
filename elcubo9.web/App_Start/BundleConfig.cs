using System.Web;
using System.Web.Optimization;

namespace elcubo9.web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            /*------------APP LOGIN-------------------*/
            bundles.Add(new StyleBundle("~/content/app-login").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-block-ui.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/toastr.css",
                      "~/Content/site-login.css"));

            bundles.Add(new ScriptBundle("~/bundles/app-login").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/angular-login").Include(
                    "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/loading-bar.min.js",
                      "~/Scripts/angular-block-ui.min.js",
                      "~/app/app-login.js",
                      "~/app/services/authService.js",
                      "~/app/services/authInterceptorService.js",
                      "~/app/services/toastrService.js",
                      "~/app/services/utilService.js",
                      "~/app/controllers/appLoginController.js",
                      "~/app/controllers/homeController.js",
                      "~/app/controllers/loginController.js",
                      "~/app/controllers/associateController.js",
                      "~/app/controllers/authCompleteController.js"));

            /*------------APP INTERN--------------------*/
            bundles.Add(new StyleBundle("~/content/app").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/loading-bar.css",
                      "~/Content/angular-block-ui.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/toastr.js",
                      "~/Scripts/moment-with-locales.min.js"
                  //    ,"~/Scripts/jquery.signalR-2.2.0.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/app/angular").Include(
                    "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/loading-bar.min.js",
                      "~/Scripts/angular-block-ui.min.js",
                      "~/app/app.js",
                      "~/app/filters/fromnow.js",
                      "~/app/services/myHttpService.js",
                      "~/app/services/authService.js",
                      "~/app/services/authInterceptorService.js",
                      "~/app/services/toastrService.js",
                      "~/app/services/utilService.js",
                      "~/app/services/customerService.js",
                      "~/app/services/menuService.js",
                      "~/app/services/signalRService.js",
                      "~/app/services/orderService.js",
                      "~/app/services/userService.js",
                      "~/app/controllers/appController.js",
                      "~/app/controllers/homeController.js",
                      "~/app/controllers/internController.js",
                      "~/app/controllers/menuController.js",
                      "~/app/controllers/itemController.js",
                      "~/app/controllers/orderController.js",
                      "~/app/controllers/orderSentController.js",
                      "~/app/controllers/myOrdersController.js",
                      "~/app/controllers/passwordController.js",
                      "~/app/controllers/contactController.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
