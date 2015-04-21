'use strict';
app.controller('connectionController', ['$rootScope', '$scope', 'signalRConnectionsService', '$myhttp',
function ($rootScope, $scope, signalRConnectionsService, $myhttp) { 
    $scope.connected = signalRConnectionsService.connected;
    $scope.ConnectionID = signalRConnectionsService.connectionID;
    var init = function () {
        signalRConnectionsService.refreshConnections();
    }
    if (signalRConnectionsService.connected) {
        init();
    } else {
        $myhttp.ping().then(function () {
            signalRConnectionsService.initialize();
        })
    }
    $rootScope.$on("connected", function (e, data) {
        $scope.$apply(function () {
            $scope.ConnectionID = signalRConnectionsService.connectionID;
            console.log("CONNECTED");
            $scope.connected = true;
            init();
        });
    });
    $rootScope.$on("reconnected", function (e, data) {
        $scope.$apply(function () {
            $scope.ConnectionID = signalRConnectionsService.connectionID;
            console.log("RECONNECTED");
            $scope.connected = true;
        });
    });
    $rootScope.$on("disconnected", function (e, data) {
        $scope.$apply(function () {
            console.log("DISCONNECTED");
            $scope.connected = false;
        });
    });

    $rootScope.$on("getConnections", function (e, data) {
        $scope.$apply(function () {
            console.log("Getting Connections");
            $scope.data = data;
        });
    });
    $scope.refresh = function () {
        signalRConnectionsService.refreshConnections();
    }
}
]);