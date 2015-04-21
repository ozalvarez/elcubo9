'use strict';
app.controller('printController', ['$scope', "$location", 'orderService',
    function ($scope, $location, orderService) {
        $scope.getOrder = function (orderID) {
            orderService.getOrder(orderID).then(function (data) {
                $scope.data = data;
                window.print();
                window.onfocus = function () {
                    $scope.parentWindow = window.opener.$windowScope;
                    $scope.parentWindow.printed(orderID);
                    window.close();
                }
            });
        }
        $scope.getOrder($location.search().OrderID);
    }
]);