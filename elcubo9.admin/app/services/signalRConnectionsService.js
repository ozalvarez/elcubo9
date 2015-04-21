'use strict';
app.factory('signalRConnectionsService', ["$rootScope", "$q", 'localStorageService',
    function ($rootScope, $q, localStorageService) {
        var signalRConnectionsService = {
            proxy: null,
            connected: false,
            connectionID:null,
            initialize: function () {
                console.log(signalRConnectionsService.connected)
                if (!signalRConnectionsService.connected) {
                    var connection = $.hubConnection(APIURL + "signalr", { useDefaultPath: false });
                    connection.qs = { Bearer: localStorageService.get('authorizationData').token };
                    signalRConnectionsService.proxy = connection.createHubProxy('OrderHub');
                    signalRConnectionsService.proxy.on('getConnections', function (data) {
                        $rootScope.$emit("getConnections", data);
                    });
                    var _Start = function (reconnect) {
                        connection.start().done(function () {
                            signalRConnectionsService.connected = true;
                            signalRConnectionsService.connectionID = connection.id
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
                        signalRConnectionsService.connected = false;
                        $rootScope.$emit("disconnected");
                        setTimeout(function () {
                            _Start(true);
                        }, 10000);
                    });
                }
            },
            refreshConnections: function () {
                return signalRConnectionsService.proxy.invoke('RefreshConnections');
            }
        };
        return signalRConnectionsService;
    }
]);