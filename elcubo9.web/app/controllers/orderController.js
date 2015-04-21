'use strict';
app.controller('orderController', ['$rootScope', '$scope', "$routeParams", '$location', 'menuService', 'orderService', 'toastrService',
    function ($rootScope, $scope, $routeParams, $location, menuService, orderService, toastrService) {
        $scope.$parent.showNav = true;
        if (menuService.order.Items.length == 0)
            $location.path('/');
        $scope.order = menuService.order;
        $scope.send = function () {
            orderService.send($scope.order).then(function (data) {
                $scope.order.OrderID = data;
                $rootScope.signalRService.send($scope.order.CustomerID, $scope.order.OrderID).then(function () {
                    $scope.$apply(function () {
                        $location.path('/orderSent/' + $scope.order.OrderID);
                    });
                });
            });
        };
        $scope.remove = function (item) {
            if ($scope.order.Items.length <= 1) {
                toastrService.error("No puede estar vacia la orden");
            } else {
                $scope.order.Items.splice(item, 1);
            }
        };
    }
]);