var app = angular.module('app', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });
    $routeProvider.when("/menu", {
        controller: "menuController",
        templateUrl: "/app/views/menu.html"
    });
    $routeProvider.when("/additional", {
        controller: "additionalController",
        templateUrl: "/app/views/additional.html"
    });
    $routeProvider.when("/password", {
        controller: "passwordController",
        templateUrl: "/app/views/password.html"
    });
    $routeProvider.when("/configuration", {
        controller: "configurationController",
        templateUrl: "/app/views/configuration.html"
    });
    $routeProvider.when("/report-orders", {
        controller: "reportOrderController",
        templateUrl: "/app/views/report-orders.html"
    });
    $routeProvider.otherwise({ redirectTo: "/" });
}]);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: APIURL,
    clientId: 'ngAuthApp'
});

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
