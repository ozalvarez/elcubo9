'use strict';
app.factory('menuService', ["$myhttp", 'customerService',
    function ($http, customerService) {
        var _Url = APIURL + 'api/menu';
        return {
            get: function () {
                return $http.get(_Url + '/get-all?CustomerID=' + customerService.customer.CustomerID);
            },
            save: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.post(_Url, model);
            },
            update: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.put(_Url, model);
            },
            removeItem: function (menuID) {
                return $http.delete(_Url + '?MenuID=' + menuID);
            },
            updateTag: function (model) {
                model.CustomerID = customerService.customer.CustomerID;
                return $http.put(_Url + '/update-tag', model);
            },
            removeTag: function (model) {
                return $http.delete(_Url + '/tag?MenuTagID=' + model.MenuTagID);
            },
            addAdditional: function (model) {
                return $http.post(_Url + '/add-additional', model);
            },
            removeAdditional: function (id) {
                return $http.delete(_Url + '/delete-additional?MenuAdditionalID=' + id);
            }
            , disable: function (MenuID, Disabled) {
                return $http.post(_Url + '/disable', {
                    MenuID: MenuID,
                    Disabled: Disabled
                });
            }
        };
    }
]);
