'use strict';
app.controller('orderSentController', ['$scope', '$rootScope', "$routeParams", '$location', 'menuService', 'orderService',
    function ($scope, $rootScope, $routeParams, $location, menuService, orderService) {
        $scope.$parent.showNav = true;
        $scope.getOrder = function () {
            orderService.getOrder($routeParams.orderID).then(function (data) {
                $scope.order = data;
            })
        };
        $scope.getOrder();
        $rootScope.$on("changeStatus", function (e, orderID, status) {
            $scope.$apply(function () {
                if (orderID == $routeParams.orderID) {
                    $scope.order.OrderStatus = status
                }
            });
        });
    }
]);