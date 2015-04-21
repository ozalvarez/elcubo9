var app = angular.module('app',['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(["$routeProvider", function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });
    $routeProvider.when("/customers", {
        controller: "customerController",
        templateUrl: "/app/views/customers.html"
    });
    $routeProvider.when("/connections", {
        controller: "connectionController",
        templateUrl: "/app/views/connections.html"
    });
    $routeProvider.when("/users", {
        controller: "userController",
        templateUrl: "/app/views/users.html"
    });
    $routeProvider.when("/orders", {
        controller: "orderController",
        templateUrl: "/app/views/orders.html"
    });
    $routeProvider.otherwise({ redirectTo: "/" });
}]);

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


