'use strict';
app.controller('reportOrderController', ['$scope', '$rootScope', 'toastrService', 'orderService','customerService',
     function ($scope, $rootScope, toastrService, orderService, customerService) {
         
         $scope.getOrders = function () {
             orderService.getReport($scope.paging).then(function (data) {
                 if (data.length > 0) {
                     $scope.data.push.apply($scope.data, data);
                 } else {
                     $scope.disabledMore = true;
                 }
             });
         }
         $scope.more = function () {
             $scope.paging++;
             $scope.getOrders();
         };
         $scope.refresh = function () {
             $scope.paging = 1;
             $scope.data = [];
             $scope.getOrders();
         };
         
         $scope.openOrder = function (item, $index) {
             $scope.order = item;
             $scope.order.$index = $index;
             $('#modal-order').modal({
                 backdrop: true
             });
         };
         /*INIT*/
         $scope.init = function () {
             $scope.paging = 1;
             $scope.disabledMore = false;
             $scope.data = [];
             $scope.getOrders();
         };
         $scope.$on('initDone', function (event) {
             $scope.init();
         });
         if (customerService.sitesLoaded) {
             $scope.init();
         }
     }
]);