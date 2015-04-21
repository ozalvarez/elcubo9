'use strict';
app.factory('orderService', ["$myhttp", "$q", "utilService",
    function ($http,$q, utilService) {
        return {
            get: function (paging) {
                return $http.get(APIURL + 'api/order?Paging=' + paging);
            },
        };
    }
]);
