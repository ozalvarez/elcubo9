'use strict';
app.factory('signalRCustomerService', ["$rootScope", "$q", "$myhttp", 'customerService', 'localStorageService', 'utilService',
    function ($rootScope, $q, $http, customerService, localStorageService, utilService) {
        var signalRCustomerService = {
            proxy: null,
            connected: false,
            initialize: function (reset) {
                console.log(reset)
                if (!signalRCustomerService.connected || reset!=null) {
                    var connection = $.hubConnection(APIURL + "signalr", { useDefaultPath: false });
                    connection.qs = {
                        Bearer: localStorageService.get('authorizationData').token,
                        Type: "CUSTOMER",
                        CustomerID: customerService.customer.CustomerID
                    };
                    signalRCustomerService.proxy = connection.createHubProxy('OrderHub');
                    signalRCustomerService.proxy.on('getPendingOrders', function (orderID) {
                        $rootScope.$emit("getPendingOrders", orderID);
                    });
                    var _Start = function (reconnect) {
                       // connection.logging = true;
                        connection.start().done(function () {
                            signalRCustomerService.connected = true;
                            if (reconnect) {
                                $rootScope.$emit("reconnected", connection.id);
                            } else {
                                $rootScope.$emit("connected", connection.id);
                            }
                        }).fail(function (e) {
                            console.log('Failed to connect Connected', e);
                        });
                    }
                    _Start(false);
                    connection.disconnected(function () {
                        signalRCustomerService.connected = false;
                        $rootScope.$emit("disconnected");
                        setTimeout(function () {
                            _Start(true);
                        }, 10000); // Restart connection after 10 seconds.
                    });
                } else {
                    signalRCustomerService.get();
                }
            },
            changeStatus: function (orderID, userID, status) {
                return signalRCustomerService.proxy.invoke('ChangeStatus', orderID, userID, customerService.customer.CustomerID, status);
            },
            get: function () {
                return signalRCustomerService.proxy.invoke('GetPendingOrders', customerService.customer.CustomerID);
            }
        };
        return signalRCustomerService;
    }
]);