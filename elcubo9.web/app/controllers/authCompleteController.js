app.controller('authCompleteController', ['$scope', '$location', "$routeParams", 'authService', 'ngAuthSettings', 'toastrService',
    function ($scope, $location, $routeParams, authService, ngAuthSettings, toastrService) {
        var fragment = {
            provider: $routeParams.provider,
            haslocalaccount: $routeParams.haslocalaccount,
            external_user_name: $routeParams.external_user_name,
            email: $routeParams.email,
            external_access_token: $routeParams.external_access_token
        }
        $routeParams = null;
        $scope.authCompletedCB = function (fragment) {
            if (fragment.haslocalaccount == 'false') {
                authService.logOut(true);
                authService.externalAuthData = {
                    provider: fragment.provider,
                    Name: fragment.external_user_name,
                    Email: fragment.email,
                    externalAccessToken: fragment.external_access_token
                };
                $location.path('/associate');
            }
            else {
                //Obtain access token and redirect to orders
                var externalData = {
                    provider: fragment.provider,
                    externalAccessToken: fragment.external_access_token
                };
                authService.obtainAccessToken(externalData).then(function (response) {
                    window.location = InternUrl;
                }, function (err) {
                    toastrService.error(err);
                });
            }
        }
        $scope.authCompletedCB(fragment);
    }
]);