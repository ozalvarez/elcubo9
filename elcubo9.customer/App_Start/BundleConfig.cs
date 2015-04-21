using System.Web;
using System.Web.Optimization;

namespace elcubo9.customer
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

            #region LOGIN
            bundles.Add(new StyleBundle("~/content/login").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/loading-bar.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/loading-bar.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/app/app-login.js",
                      "~/app/services/authService.js",
                      "~/app/directives/autoFillSync.js",
                      "~/app/controllers/loginController.js"));
            #endregion

            #region INTERN
            bundles.Add(new StyleBundle("~/content/app").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/loading-bar.css",
                      "~/Content/angular-ui-tree.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/plugins/metisMenu/metisMenu.min.css",
                      "~/Content/plugins/timeline.css",
                      "~/Content/plugins/morris.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/toastr.js",
                      "~/scripts/plugins/metisMenu/metisMenu.min.js",
                      "~/scripts/sb-admin-2.js",
                      "~/Scripts/moment-with-locales.min.js"
                    //  ,"~/Scripts/jquery.signalR-{version}.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/app/angular").Include(
                    "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/loading-bar.min.js",
                      "~/Scripts/angular-ui-tree.min.js",
                        "~/app/app.js",
                        "~/app/filters/fromnow.js",
                        "~/app/services/myHttpService.js",
                "~/app/services/authService.js",
                "~/app/services/authInterceptorService.js",
                "~/app/services/toastrService.js",
                "~/app/services/utilService.js",
                "~/app/services/signalRCustomerService.js",
                "~/app/services/customerService.js",
                "~/app/services/orderService.js",
                "~/app/services/menuService.js",
                "~/app/services/additionalService.js",
                "~/app/services/configurationService.js",
                "~/app/services/userService.js",
                "~/app/directives/fileread.js",
                "~/app/controllers/appController.js",
                "~/app/controllers/ordersController.js",
                "~/app/controllers/menuController.js",
                "~/app/controllers/additionalController.js",
                "~/app/controllers/passwordController.js",
                "~/app/controllers/configurationController.js"
                ,"~/app/controllers/reportOrderController.js"));
            #endregion

            #region PRINT
            bundles.Add(new StyleBundle("~/content/print").Include(
                 "~/Content/bootstrap.css",
                      "~/Content/loading-bar.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/toastr.css",
                      "~/Content/site-print.css"));

            bundles.Add(new ScriptBundle("~/bundles/print").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/print/angular").Include(
                    "~/Scripts/angular.min.js",
                    "~/Scripts/angular-local-storage.min.js",
                      "~/Scripts/loading-bar.min.js",
                      "~/app/app-print.js",
                      "~/app/services/myHttpService.js",
                      "~/app/services/toastrService.js",
                      "~/app/services/utilService.js",
                      "~/app/services/authService.js",
                      "~/app/services/authInterceptorService.js",
                      "~/app/services/customerService.js",
                "~/app/services/orderService.js",
                "~/app/controllers/printController.js"));
            #endregion

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
           // BundleTable.EnableOptimizations = true;
        }
    }
}
