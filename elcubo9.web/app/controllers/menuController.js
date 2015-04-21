'use strict';
app.controller('menuController', ['$scope','$rootScope', "$routeParams","$location", 'menuService',
    function ($scope,$rootScope, $routeParams,$location, menuService) {
        $scope.$parent.showNav = true;
        if ($routeParams.tableNumber != null) {
            menuService.order = { Items: [] };
            menuService.order.TableNumber = $routeParams.tableNumber;
        }
        menuService.order.CustomerID = $routeParams.customerID;
        $scope.order = menuService.order;
        $scope.select = function (menuItem) {
            menuService.menuItemToAdd = menuItem;
            $location.path('/item');
        }
        $scope.get = function () {
            menuService.get($routeParams.customerID).then(function (data) {
                $rootScope.Symbol = data[0].Symbol == null ? '$' : data[0].Symbol;
                $scope.data = data;
            });
        }

        /*INIT*/
        $scope.$on('initDone', function (event) {
            $scope.get();
        });
        if ($rootScope.loaded) {
            $scope.get();
        }
    }
]);