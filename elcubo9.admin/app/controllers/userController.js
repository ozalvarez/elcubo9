'use strict';
app.controller('userController', ['$scope',"userService",
    function ($scope, userService) {
        $scope.users = [];
        $scope.paging = 1;
        $scope.text = null;
        $scope.disabledMore = false;

        var getCallback = function (data) {
            $scope.users = data;
        }

        function Get() {
            return userService.get($scope.paging, $scope.text);
        }

        $scope.getUsers = function () {
            Get().then(getCallback);
        };
        $scope.more = function () {
            $scope.paging++;
            Get().then(function (data) {
                if (data.length > 0) {
                    $scope.users.push.apply($scope.users, data);
                } else {
                    $scope.disabledMore = true;
                }
            });
        };
        $scope.search = function () {
            Get().then(getCallback);
        };
        $scope.getUsers();
        $scope.block = function (item,block) {
            userService.block(item.UserID, block).then(function () {
                item.Block = block;
            })
        };

    }
]);