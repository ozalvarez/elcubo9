'use strict';
app.controller('customerController', ['$scope', 'customerService', 'toastrService',
    function ($scope, customerService,  toastrService) {
        $scope.get = function () {
            customerService.get().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.get();
        $scope.new = function (item) {
            $scope.c = {};
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            customerService.save($scope.c).then(function (data) {
                $scope.get();
                $('#modal-new').modal('hide');
                toastrService.success("Customer Saved");
            });
        };
        $scope.remove = function (item) {
            if (confirm("Are you sure?")) {
                customerService.remove(item.CustomerID).then(function (data) {
                    $scope.get();
                    toastrService.success("Deleted");
                });
            }
        };
        $scope.enable = function (item, enable) {
            customerService.enable(item.CustomerID, enable).then(function (data) {
                item.Enabled = enable;
                $scope.get();
                if (item.Enabled)
                    toastrService.success("Enabled");
                else
                    toastrService.success("Disabled");
            });
        };

        $scope.openAddUser = function (item) {
            $scope.u = {
                CustomerID: item.CustomerID
            };
            $('#modal-add-user').modal({
                backdrop: true
            });
        };
        $scope.addUser = function () {
            customerService.addUser($scope.u).then(function (data) {
                $('#modal-add-user').modal('hide');
                toastrService.success("Usuario Agregado");
            });
        };
        $scope.openViewUser = function (item) {
            $scope.CustomerIDSelected = item.CustomerID
            customerService.getUsers(item.CustomerID).then(function (data) {
                $scope.users = data;
                $('#modal-view-user').modal({
                    backdrop: true
                });
            });
        };
        $scope.addRol = function (item, customerUserType, enable) {
            customerService.addRol(item.UserID, customerUserType, enable, $scope.CustomerIDSelected).then(function (data) {
                customerService.getUsers($scope.CustomerIDSelected).then(function (data) {
                    $scope.users = data;
                    toastrService.success("Rol Cambiado");
                });
            });
        };
        $scope.removeUser = function (item) {
            if (confirm("Are you sure?")) {
                customerService.removeUser(item.UserID, $scope.CustomerIDSelected).then(function (data) {
                    customerService.getUsers($scope.CustomerIDSelected).then(function (data) {
                        $scope.users = data;
                        toastrService.success("User Deleted");
                    });
                });
            }
        };
        $scope.edit = function (item) {
            $scope.c = item;
            $('#modal-new').modal({
                backdrop: true
            });
        };
    }
]);