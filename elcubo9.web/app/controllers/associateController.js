'use strict';
app.controller('associateController', ['$scope', '$location', '$timeout', 'authService', 'toastrService',
    function ($scope, $location, $timeout, authService, toastrService) {
        $scope.$parent.showNav = false;
        if (authService.externalAuthData.provider == "") {
            $location.path('/');
        }
        $scope.registerData = {
            Email: authService.externalAuthData.Email,
            Name: authService.externalAuthData.Name,
            provider: authService.externalAuthData.provider,
            externalAccessToken: authService.externalAuthData.externalAccessToken
        };

        $scope.registerExternal = function () {
            $scope.loggingIn = true;
            authService.registerExternal($scope.registerData).then(function (response) {
                toastrService.success("El usuario se ha creado ya puedes hacer tu 1ra orden.")
                window.location = InternUrl;
            },
            function (response) {
                $scope.loggingIn =false;
                      toastrService.error(response);
                  });
        };
    }
]);