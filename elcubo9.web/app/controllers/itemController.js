'use strict';
app.controller('itemController', ['$scope', "$routeParams", '$location', 'menuService', 'utilService',
    function ($scope, $routeParams, $location, menuService, utilService) {
        $scope.$parent.showNav = true;
        $scope.orderMenu = {
            Quantity: 1
        }
        if (menuService.menuItemToAdd.MenuID != null) {
            $scope.data = menuService.menuItemToAdd;
        } else {
            $location.path('/intern');
        }

        $scope.openQuantity = function () {
            $('#modal-quantity').modal({
                backdrop: true
            });
        };
        $scope.openAdditionalInfo = function () {
            $('#modal-additional-info').modal({
                backdrop: true
            });
        };
        $scope.openAdditionalItems = function (item) {
            $scope.additional = item;
            $('#modal-additional-items').modal({
                backdrop: true
            });
        };
        $scope.addToOrder = function () {
            $scope.orderMenu = {
                Title: $scope.data.Title,
                Subtitle: $scope.data.Subtitle,
                Price: $scope.data.Price,
                Quantity: $scope.orderMenu.Quantity,
                MenuID: $scope.data.MenuID,
                AdditionalInfo: $scope.orderMenu.AdditionalInfo,
                Items: []
            }
            var _itemToOpen = null;

            angular.forEach($scope.data.Items, function (item) {
                var _Active = false;
                angular.forEach(item.Items, function (item2) {
                    if (item2.Active) {
                        $scope.orderMenu.Items.push(item2);
                        _Active = true;
                    }
                });
                if (item.Required && !_Active) {
                    _itemToOpen = item;
                }
            });
            if (_itemToOpen == null) {
                menuService.order.Items.push($scope.orderMenu);
                $location.path('/menu/' + $scope.data.CustomerID);
            } else {
                $scope.openAdditionalItems(_itemToOpen);
            }
        };

    }
]);