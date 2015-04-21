﻿'use strict';
app.controller('homeController', ['$scope', '$location', 'authService', 'ngAuthSettings',
    function ($scope, $location, authService, ngAuthSettings) {
        if (authService.authentication.isAuth) {
            window.location = InternUrl;
        }
        $scope.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };
        $scope.message = "";

        $scope.login = function () {
            /*THIS LINE IS FOR FIX BUG ON AUTOCOMPLETE FORM*/
            $scope.$broadcast("autofill:update");
            authService.login($scope.loginData).then(function (response) {
                $location.path(InternUrl);
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };

        $scope.authExternalProvider = function (provider) {
            
            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                        + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                        + "&redirect_uri=" + RedirectUri;
            window.$windowScope = $scope;
            var windowSize = "width=" + window.innerWidth + ",height=" + window.innerHeight + ",scrollbars=no";
            var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", windowSize);
        };

        $scope.authCompletedCB = function (fragment) {
            $scope.$apply(function () {

                if (fragment.haslocalaccount == 'False') {

                    authService.logOut();

                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };

                    $location.path('/associate');

                }
                else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function (response) {

                        $location.path('/orders');

                    },
                 function (err) {
                     $scope.message = err.error_description;
                 });
                }

            });
        }
    }
]);
