'use strict';
app.controller('passwordController', ['$scope', '$rootScope', 'userService', 'utilService', 'toastrService', 
    function ($scope, $rootScope, userService, utilService, toastrService) {
        $scope.change = function () {
            userService.changePassword($scope.p).then(function (data) {
                toastrService.success("Clave Cambiada");
            });
        }
    }
]);