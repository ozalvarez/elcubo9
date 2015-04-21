'use strict';
app.controller('passwordController', ['$scope', '$rootScope', 'userService',  'toastrService', 'customerService',
    function ($scope, $rootScope, userService, toastrService, customerService) {
        $scope.change = function () {
            userService.changePassword($scope.p).then(function (data) {
                toastrService.success("Clave Cambiada");
            });
        }
        /*INIT*/
        $scope.init = function () {

        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (customerService.sitesLoaded) {
            $scope.init();
        }
    }
]);