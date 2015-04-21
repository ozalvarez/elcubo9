'use strict';
app.controller('configurationController', ['$scope', 'toastrService', 'customerService',
    function ($scope, toastrService, customerService) {

        $scope.getEmails = function () {
            customerService.getEmails().then(function (data) {
                $scope.emails = data;
            })
        }
        $scope.addEmail = function () {
            customerService.addEmail($scope.email).then(function (data) {
                toastrService.success("Email " + $scope.email.Email + " Agregado");
                $scope.getEmails();
            });
        }
        $scope.removeEmail = function (CustomerEmailID) {
            customerService.removeEmail(CustomerEmailID).then(function (data) {
                $scope.getEmails();
            });
        }

        /*INIT*/
        $scope.init = function () {
            $scope.getEmails();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (customerService.sitesLoaded) {
            $scope.init();
        }
    }
]);