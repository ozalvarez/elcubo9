var app = angular.module('app', ['ngRoute', 'LocalStorageModule', 'blockUI']);

app.config(["$routeProvider", "blockUIConfig",
    function ($routeProvider, blockUIConfig) {
        $routeProvider.when("/", {
            controller: "internController",
            templateUrl: "/app/views/intern.html"
        });
        $routeProvider.when("/menu/:customerID/:tableNumber?", {
            controller: "menuController",
            templateUrl: "/app/views/menu.html"
        });
        $routeProvider.when("/item", {
            controller: "itemController",
            templateUrl: "/app/views/item.html"
        });
        $routeProvider.when("/order", {
            controller: "orderController",
            templateUrl: "/app/views/order.html"
        });
        $routeProvider.when("/orderSent/:orderID", {
            controller: "orderSentController",
            templateUrl: "/app/views/orderSent.html"
        });
        $routeProvider.when("/my-orders", {
            controller: "myOrdersController",
            templateUrl: "/app/views/my-orders.html"
        });
        $routeProvider.when("/password", {
            controller: "passwordController",
            templateUrl: "/app/views/password.html"
        });
        $routeProvider.when("/contact", {
            controller: "contactController",
            templateUrl: "/app/views/contact.html"
        });
        $routeProvider.otherwise({ redirectTo: "/" });

        // Change the default overlay message
        blockUIConfig.message = 'Cargando..';

        // Change the default delay to 100ms before the blocking is visible
        blockUIConfig.delay = 100;
    }
]);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: APIURL,
    clientId: 'app'
});

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
//app.run(['$templateCache', function ($templateCache) {
//    $templateCache.removeAll();
//}]);