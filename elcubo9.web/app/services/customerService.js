'use strict';
app.factory('customerService', ["$myhttp", '$q',
    function ($myhttp, $q) {
        return {
            get: function () {
                return $myhttp.get(APIURL + 'api/customer');
            }
        };
    }
]);
