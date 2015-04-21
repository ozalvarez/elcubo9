var app = angular.module('app', [ 'LocalStorageModule', 'angular-loading-bar']);

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


