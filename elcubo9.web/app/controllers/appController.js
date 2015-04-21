'use strict';
app.controller('appController', ['$scope', '$rootScope', "$q", '$location', "utilService", 'authService', 'signalRService', '$myhttp',
    function ($scope, $rootScope, $q, $location, utilService, authService, signalRService, $myhttp) {
        if (!authService.authentication.isAuth) {
            authService.logOut();
        } else {
            $scope.authentication = authService.authentication;
            $rootScope.signalRService = signalRService;
            $scope.connected = signalRService.connected;
            $myhttp.ping().then(function () {
                signalRService.initialize();
                $rootScope.loaded = true;
                $rootScope.$broadcast('initDone');
            })

        }
        $scope.go = function (path) {
            $location.path(path);
        };
        $scope.logOut = function () {
            authService.logOut();
        }
        $rootScope.$on("connected", function (e, data) {
            $scope.$apply(function () {
                console.log("CONNECTED");
                $scope.connected = true;
            });
        });
        $rootScope.$on("reconnected", function (e, data) {
            $scope.$apply(function () {
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

    }
]);