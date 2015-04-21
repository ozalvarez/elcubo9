'use strict';
app.controller('appController', ['$scope', '$rootScope', 'authService', 'customerService', 'signalRCustomerService', 'orderService',
    function ($scope, $rootScope, authService, customerService, signalRCustomerService, orderService) {

        /*SIGNAL R*/
        $rootScope.signalRCustomerService = signalRCustomerService;

        $scope.connected = signalRCustomerService.connected;
        $rootScope.$on("getPendingOrders", function (e, orderID) {
            $scope.$apply(function () {
                if (orderID != null) {
                    $scope.showNot();
                    console.log("NEW ORDER", orderID);
                }
            });
        });
        $rootScope.$on("connected", function (e, connectionID) {
            $scope.$apply(function () {
                $scope.ConnectionID = connectionID
                console.log("CONNECTED",connectionID);
                $scope.connected = true;
            });
        });
        $rootScope.$on("reconnected", function (e, connectionID) {
            $scope.$apply(function () {
                $scope.ConnectionID = connectionID
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
        $scope.showNot = function () {
            if (typeof Notification !== 'undefined') {
                if (Notification.permission !== "granted")
                    Notification.requestPermission();
                var notification = new Notification('Hay Ordenes Nuevas', {
                    icon: 'http://elcubo9.com/favicon.ico',
                    body: "Haga Click para Procesarlas",
                });
                notification.onclick = function () {
                    window.focus();
                }
            } else {
                return;
            }
        }

        $scope.logOut = function () {
            authService.logOut();
            window.location = "/"
        }
        $scope.authentication = authService.authentication;
        customerService.get().then(function (data) {
            if (data.length > 0) {
                $scope.customer = customerService.customer;
                $scope.customers = customerService.customers;
                $rootScope.Symbol = customerService.customer.Symbol == null ? '$' : customerService.customer.Symbol;
                signalRCustomerService.initialize();
                $rootScope.$broadcast('initDone');
            } else {
                $scope.logOut();
            }
        });
        $scope.changeCustomer = function (item) {
            customerService.customer = item;
            $scope.customer = customerService.customer;
            $rootScope.Symbol = customerService.customer.Symbol == null ? '$' : customerService.customer.Symbol;           
            signalRCustomerService.initialize(true);
            $rootScope.$broadcast('initDone');
        };
        $scope.openTerms = function () {
            console.log('dd')
            $('#modal-terms').modal({
                backdrop: true
            });
        };
    }
]);