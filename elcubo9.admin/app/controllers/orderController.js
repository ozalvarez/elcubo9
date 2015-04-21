'use strict';
app.controller('orderController', ['$scope', '$rootScope', 'toastrService', 'orderService',
    function ($scope, $rootScope, toastrService, orderService) {
        $scope.paging = 1;
        $scope.disabledMore = false;
        $scope.data = [];
        $scope.getOrders = function () {
            orderService.get($scope.paging).then(function (data) {
                if (data.length > 0) {
                    $scope.data.push.apply($scope.data, data);
                } else {
                    $scope.disabledMore = true;
                }
            });
        }
        $scope.more = function () {
            $scope.paging++;
            $scope.getOrders();
        };
        $scope.refresh = function () {
            $scope.paging = 1;
            $scope.data = [];
            $scope.getOrders();
        };
        $scope.getOrders();
        $scope.openOrder = function (item, $index) {
            $scope.order = item;
            $scope.order.$index = $index;
            $('#modal-order').modal({
                backdrop: true
            });
        };
    }
]);