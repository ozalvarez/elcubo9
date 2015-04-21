'use strict';
app.factory('customerService', ["$myhttp", "$q",
    function ($http, $q) {
        var customerServiceFactory = {
            customer: {},
            customers: [],
            sitesLoaded: false,
            get: function () {
                var deferred = $q.defer();
                $http.get(APIURL + 'api/customer/by-user').then(function (data) {
                    customerServiceFactory.customers = data;
                    customerServiceFactory.customer = data[0];
                    customerServiceFactory.sitesLoaded = true;
                    deferred.resolve(data);
                });
                return deferred.promise;
            },
            isPrint: function () {
                return (customerServiceFactory.customer.Roles.indexOf(3) == -1) ? false : true
            },
            getEmails:function(){
                return $http.get(APIURL + 'api/customer/email?CustomerID=' + customerServiceFactory.customer.CustomerID);
            },
            addEmail: function (model) {
                model.CustomerID = customerServiceFactory.customer.CustomerID;
                return $http.post(APIURL + 'api/customer/email', model);
            },
            removeEmail: function (CustomerEmailID) {
                return $http.delete(APIURL + 'api/customer/email?CustomerEmailID=' + CustomerEmailID);
            }
        };
        return customerServiceFactory;
    }
]);
