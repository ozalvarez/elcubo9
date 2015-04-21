'use strict';
app.controller('myOrdersController', ['$scope', "$routeParams", '$location', 'orderService', 'toastrService',
    function ($scope, $routeParams, $location, orderService, toastrService) {
        $scope.$parent.showNav = true;
        orderService.getMy().then(function (data) {
            $scope.data = data;
        });
    }
]);