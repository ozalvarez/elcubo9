'use strict';
app.controller('internController', ['$scope', '$rootScope','$location', 'customerService', 'menuService',
    function ($scope, $rootScope,$location, customerService, menuService) {
        menuService.order = { Items: [] };
        $scope.get = function () {
            customerService.get().then(function (data) {
                $scope.data = data;
            });
        }
        /*INIT*/
        $scope.$on('initDone', function (event) {
            $scope.get();
        });
        if ($rootScope.loaded) {
            $scope.get();
        }
    }
]);