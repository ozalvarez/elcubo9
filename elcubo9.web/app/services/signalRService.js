'use strict';
app.factory('signalRService', ["$rootScope", 'localStorageService',
    function ($rootScope, localStorageService) {
        var signalRService = {
            proxy: null,
            connected: false,
            initialize: function () {
                if (!signalRService.connected) {
                    var connection = $.hubConnection(APIURL + "signalr");
                    connection.qs = {
                        Bearer: localStorageService.get('authorizationData').token,
                        Type: "WEB"
                    };
                    signalRService.proxy = connection.createHubProxy('OrderHub');
                    signalRService.proxy.on('changeStatus', function (orderID, status) {
                        $rootScope.$emit("changeStatus", orderID, status);
                    });
                    var _Start = function (reconnect) {
                        connection.start().done(function () {
                            signalRService.connected = true;
                            if (reconnect) {
                                $rootScope.$emit("reconnected");
                            } else {
                                $rootScope.$emit("connected");
                            }
                        }).fail(function (e) {
                            console.log('Failed to connect Connected', e);
                        });
                    }
                    _Start(false);
                    connection.disconnected(function () {
                        signalRService.connected = false;
                        $rootScope.$emit("disconnected");
                        setTimeout(function () {
                            _Start(true);
                        }, 10000); // Restart connection after 10 seconds.
                    });
                    
                } else {

                }
            },
            send: function (customerID, orderID) {
                return signalRService.proxy.invoke('Send', customerID, orderID);
            }
        };
        return signalRService;
    }
]);