'use strict';
app.controller('additionalController', ['$scope', '$rootScope', '$timeout', 'customerService', 'additionalService', 'toastrService',
    function ($scope, $rootScope, $timeout, customerService, additionalService, toastrService) {
        $scope.get = function () {
            additionalService.get().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.new = function (item) {
            $scope.a = {};
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            additionalService.save($scope.a).then(function (data) {
                $scope.get();
                $('#modal-new').modal('hide');
                toastrService.success("Item del menú creado");
            });
        };
        $scope.update = function (item) {
            additionalService.update(item).then(function (data) {
                item.edit = false;
                toastrService.success("Adicional Guardado");
            });
        };
        $scope.remove = function (item) {
            if (confirm("Esta seguro que quiere eliminar el adicional " + item.AdditionalName)) {
                additionalService.remove(item.AdditionalID).then(function (data) {
                    $scope.get();
                    toastrService.success("Adicional eliminado");
                });
            }
        };
        $scope.newItem = function (item) {
            $scope.ai = {
                AdditionalID: item.AdditionalID,
                AdditionalName: item.AdditionalName
            };
            $('#modal-new-item').modal({
                backdrop: true
            });
        };
        $scope.editItem = function (item) {
            $scope.ai = item;
            $('#modal-new-item').modal({
                backdrop: true
            });
        };
        $scope.addItem = function () {
            if ($scope.ai.AdditionalItemID == null) {
                additionalService.addItem($scope.ai).then(function (data) {
                    $scope.get();
                    $('#modal-new-item').modal('hide');
                    toastrService.success("Item creado");
                });
            } else {
                additionalService.updateItem($scope.ai).then(function (data) {
                    $scope.get();
                    $('#modal-new-item').modal('hide');
                    toastrService.success("Item creado");
                });
            }
        };
        $scope.removeItem = function (item) {
            if (confirm("Esta seguro que quiere eliminar el item adicional " + item.AdditionalItemName)) {
                additionalService.removeItem(item.AdditionalItemID).then(function (data) {
                    $scope.get();
                    toastrService.success("Item Adicional eliminado");
                });
            }
        }

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