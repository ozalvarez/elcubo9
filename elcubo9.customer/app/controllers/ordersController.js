'use strict';
app.controller('ordersController', ['$scope', '$rootScope', 'customerService', 'toastrService', 'orderService',
    function ($scope, $rootScope, customerService, toastrService, orderService) {
        if (typeof Notification !== 'undefined') {
            if (Notification.permission !== "granted")
                Notification.requestPermission();
        }
        $scope.getOrders = function () {
            orderService.get().then(function (data) {
                $scope.data = data;
                if (customerService.isPrint()) {
                    angular.forEach($scope.data, function (obj, $index) {
                        if (obj.OrderStatus == 1) {
                            $scope.changeStatus(obj, 5, $index);
                        }
                    });
                }

            });
        }
        
        $rootScope.$on("getPendingOrders", function (e, data) {
            $scope.$apply(function () {
                $scope.getOrders();
            });
        });

        $scope.refresh = function () {
            $scope.getOrders();
        }
        $scope.print = function (orderID) {
            if (customerService.isPrint()) {
                window.$windowScope = $scope;
                var printPage = window.open('/print#?OrderID=' + orderID, '_blank');
            }

        }
        $scope.changeStatus = function (order, status, $index) {
            var _Continue = true;
            if (status == 4) {
                if (!confirm("¿Está seguro de que esta orden es inválida?")) {
                    _Continue = false;
                }
            }
            if (_Continue) {
                orderService.changeStatus(order, status).then(function (data) {
                    $rootScope.signalRCustomerService.changeStatus(order.OrderID, order.UserID, status).then(function () {
                        if (status == 4) {
                            $scope.data.splice($index, 1);
                        } else {
                            if (status == 5) {
                                $scope.print(order.OrderID);
                            }
                            order.OrderStatus = status;
                        }
                        $('#modal-order').modal('hide');
                        toastrService.success("Status cambiado y notificado al cliente");
                    });
                });
            }
        }
        $scope.printed = function (orderID) {
            $scope.$apply(function () {
                angular.forEach($scope.data, function (obj, $index) {
                    if (obj.OrderID == orderID) {
                        $scope.changeStatus(obj, 2, $index);
                    }
                });
            });
        };
        $scope.filterList = function (item) {
            return item.OrderStatus != 3;
        };
        $scope.openOrder = function (item, $index) {
            $scope.order = item;
            $scope.order.$index = $index;
            $('#modal-order').modal({
                backdrop: true
            });
        };

        /*INIT*/
        $scope.init = function () {
            $scope.getOrders();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (customerService.sitesLoaded) {
            $scope.init();
        }
    }
]);