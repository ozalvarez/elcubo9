'use strict';
app.controller('appLoginController', ['$scope', '$location', 'authService',
    function ($scope, $location, authService) {
        $scope.go = function (path) {
            $location.path(path);
        };
        $scope.authentication = authService.authentication;
    }
]);