'use strict';
app.controller('menuController', ['$scope', '$rootScope', '$timeout', 'customerService', 'menuService', 'additionalService', 'toastrService',
    function ($scope, $rootScope, $timeout, customerService, menuService, additionalService, toastrService) {
        $scope.new = function (item) {
            $scope.menu = {};
            if (item != null) {
                $scope.menu.Tag = item.MenuTagName;
            }
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            if ($scope.menu.MenuID == null) {
                menuService.save($scope.menu).then(function (data) {
                    $scope.get();
                    $('#modal-new').modal('hide');
                    toastrService.success("Item del menú creado");
                });
            } else {
                menuService.update($scope.menu).then(function (data) {
                    $scope.get();
                    $('#modal-new').modal('hide');
                    toastrService.success("Item del menú creado");
                });
            }
        };
        $scope.updateTag = function (item) {
            menuService.updateTag(item).then(function (data) {
                item.edit = false;
                toastrService.success("Categoría Guardada");
            });
        };
        $scope.get = function () {
            menuService.get().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.removeTag = function (item) {
            if (confirm("¿Está seguro que quiere eliminar la categoría " + item.MenuTagName + "?")) {
                menuService.removeTag(item).then(function (data) {
                    $scope.get();
                    toastrService.success("Categoría eliminada");
                });
            }
        }
        $scope.editItem = function (item) {
            $scope.menu = item;
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.removeItem = function (item) {
            if (confirm("¿Está seguro que quiere eliminar el item en el menú?")) {
                menuService.removeItem(item.MenuID).then(function (data) {
                    $scope.get();
                    toastrService.success("Item en el Menú eliminado");
                });
            }
        }
        $scope.addAdditional = function (item) {
            additionalService.get().then(function (data) {
                $scope.adds = data;
                $scope.ma = {
                    Title: item.Title,
                    MenuID: item.MenuID
                }
                $('#modal-additional').modal({
                    backdrop: true
                });
            });

        };
        $scope.saveAdditional = function () {
            $scope.ma.AdditionalID = $scope.ma.Additional.AdditionalID;
            menuService.addAdditional($scope.ma).then(function (data) {
                $scope.get();
                $('#modal-additional').modal('hide');
                toastrService.success("Adicional Agregado");
            });
        };
        $scope.removeAdditional = function (item) {
            if (confirm("¿Está seguro que quiere eliminar el adicional?")) {
                menuService.removeAdditional(item.MenuAdditionalID).then(function (data) {
                    $scope.get();
                    toastrService.success("Adicional Eliminado");
                });
            }
        };
        $scope.disable = function (item, Disabled) {
            menuService.disable(item.MenuID, Disabled).then(function () {
                item.Disabled = Disabled;
            });
        };
        /*INIT*/
        $scope.init = function () {
            $scope.get();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (customerService.sitesLoaded) {
            $scope.init();
        }
    }
]);