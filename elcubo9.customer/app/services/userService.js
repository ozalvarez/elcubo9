'use strict';
app.factory('userService', ['$myhttp','ngAuthSettings',
    function ($http, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var userServiceFactory = {};
        userServiceFactory.changePassword = function (model) {
            return $http.post(serviceBase + 'api/account/changepassword', model);
        };

        return userServiceFactory;
    }]);