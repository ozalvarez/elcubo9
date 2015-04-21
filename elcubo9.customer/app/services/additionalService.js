'use strict';
app.factory('additionalService', ["$myhttp", 'customerService',
    function ($http, customerService) {
        var _Url = APIURL + 'api/additional';
        return {
            get: function () {
                return $http.get(_Url + '?CustomerID=' + customerService.customer.CustomerID);
            },
            save: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.post(_Url, model);
            },
            update: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.put(_Url, model);
            },
            remove: function (id) {
                return $http.delete(_Url + '?AdditionalID=' + id);
            },
            addItem: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.post(_Url+'/add-item', model);
            },
            updateItem: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.put(_Url+'/update-item', model);
            },
            removeItem: function (id) {
                return $http.delete(_Url + '/delete-item?AdditionalItemID=' + id);
            }
        };
    }
]);
