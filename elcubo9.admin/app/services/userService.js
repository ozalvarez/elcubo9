'use strict';
app.factory('userService', ["$myhttp", "$rootScope",
    function ($http, $rootScope) {
        var _Url = APIURL + 'api/user';
        return {
            get: function (paging, text) {
                var _q = _Url + '?Paging=' + paging;
                if (text != null)
                    _q += '&Text=' + text;
                return $http.get(_q);
            },
            getNumUsers: function () {
                return $http.get(_Url + '/getNumUsers');
            },
            getNumUsersByDay: function (date) {
                return $http.get(_Url + '/getNumUsersByDay?UTCDate=' + date.toUTCString());
            },
            getNumUsersByPeriod: function (start, end) {
                return $http.get(_Url + '/getNumUsersByPeriod?Start=' + start + "&End=" + end);
            },
            block: function (userID, block) {
                return $http.post(_Url + '/block', {
                    UserID: userID,
                    Block: block
                });
            }
        }
    }
]);