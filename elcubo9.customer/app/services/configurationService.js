'use strict';
app.factory('configurationService', ["$myhttp", "$q",
    function ($http, $q) {
        var configurationServiceFactory = {
            customer: {},
            customers: [],
            sitesLoaded: false,
            get: function () {
                return $http.get(APIURL + 'api/customer/by-user');
            }
        };
        return configurationServiceFactory;
    }
]);
