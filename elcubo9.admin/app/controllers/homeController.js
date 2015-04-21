'use strict';
app.controller('homeController', ['$scope',"$filter", "userService",
    function ($scope,$filter, usf) {
        usf.getNumUsers().then(function (data) {
            $scope.numUsers = data;
        });

        $scope.dateDay = new Date();
        usf.getNumUsersByDay($scope.dateDay).then(function (data) {
            $scope.numUsersDay = data;
        });

        $scope.end = $filter('date')(new Date(), 'MM/dd/yyyy');
        var startDate = new Date();
        startDate.setDate(1);
        $scope.start = $filter('date')(startDate, 'MM/dd/yyyy');

        $scope.getNumUsersByPeriod = function () {
            usf.getNumUsersByPeriod($scope.start, $scope.end).then(function (data) {
                $scope.numUsersPeriod = data;
            });
        }
        $scope.getNumUsersByPeriod();
    }
]);