'use strict';
app.controller('appController', ['$scope','$rootScope',"$q", '$location', 'authService',
    function ($scope, $rootScope, $q, $location, authService) {

        $scope.logOut = function () {
            authService.logOut();
            window.location = "/"
        }
        $scope.authentication = authService.authentication;
    }
]);