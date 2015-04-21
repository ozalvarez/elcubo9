'use strict';
app.factory('orderService', ["$myhttp", "$q", "utilService", 'customerService',
    function ($http, $q, utilService, customerService) {
        return {
            get: function () {
                return $http.get(APIURL + 'api/order?CustomerID=' + customerService.customer.CustomerID);
            }
            , getReport: function (paging) {
                return $http.get(APIURL + 'api/order?CustomerID=' + customerService.customer.CustomerID + "&Paging=" + paging);
            },
            getOrder: function (orderID) {
                return $http.get(APIURL + 'api/order/by-id?OrderID=' + orderID);
            },
            changeStatus: function (order, status) {
                return $http.post(APIURL + 'api/order/change-status', {
                    OrderID: order.OrderID,
                    OrderStatus: status
                });
            }
        };
    }
]);
