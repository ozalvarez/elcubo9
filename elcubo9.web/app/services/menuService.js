'use strict';
app.factory('menuService', ["$myhttp", "$q", 'utilService',
    function ($myhttp, $q, utilService) {
        var menuServiceFactory = {
            get: function (customerID) {
                return $myhttp.get(APIURL + 'api/menu?CustomerID=' + customerID)
            },
            order: {
                Items: []
            },
            menuItemToAdd: {}
        }
        return menuServiceFactory;
    }
]);
