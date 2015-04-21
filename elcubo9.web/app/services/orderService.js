'use strict';
app.factory('orderService', ["$myhttp",'localStorageService',
    function ($http, localStorageService) {
        return {
            getMy: function () {
                return $http.get(APIURL + 'api/order/my');
            },
            getOrder:function (orderID){
                return $http.get(APIURL + 'api/order/my?OrderID=' + orderID);
            },
            send: function (object) {
                object.OrderStatus = 1;
                return $http.post(APIURL + 'api/order', object);
            },
        };
    }
]);
