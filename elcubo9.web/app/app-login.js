var app = angular.module('app', ['ngRoute', 'LocalStorageModule', 'blockUI']);

app.config(["$routeProvider", "blockUIConfig",
    function ($routeProvider, blockUIConfig) {
        $routeProvider.when("/", {
            controller: "homeController",
            templateUrl: "/app/views/home.html"
        });
        $routeProvider.when("/signup", {
            controller: "loginController",
            templateUrl: "/app/views/signup.html"
        });
        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "/app/views/login.html"
        });
        $routeProvider.when("/associate", {
            controller: "associateController",
            templateUrl: "/app/views/associate.html"
        });
        $routeProvider.when("/authComplete", {
            controller: "authCompleteController",
            templateUrl: "/app/views/authComplete.html"
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
