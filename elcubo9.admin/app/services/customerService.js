'use strict';
app.factory('customerService', ["$myhttp",
    function ($http) {
        var _Url = APIURL + 'api/customer';
        return {
            get: function () {
                return $http.get(_Url + '/all');
            },
            save: function (object) {
                return $http.post(_Url, object);
            },
            remove: function (id) {
                return $http.delete(_Url + '?CustomerID=' + id);
            },
            enable: function (id, enable) {
                return $http.post(_Url + '/enable', {
                    CustomerID: id,
                    Enabled: enable
                });
            },
            addUser: function (object) {
                return $http.post(_Url + '/user', object);
            },
            getUsers: function (id) {
                return $http.get(_Url + '/users?CustomerID=' + id);
            },
            addRol: function (userID, customerUserType, enable, customerID) {
                var _object = {
                    UserID: userID,
                    CustomerUserType: customerUserType,
                    Enabled: enable,
                    CustomerID: customerID
                };
                return $http.post(_Url + '/rol', _object);
            },
            removeUser: function (userID,customerID) {
                return $http.delete(_Url + '/user?CustomerID=' + customerID + '&UserID=' + userID);
            },
        };
    }
]);
