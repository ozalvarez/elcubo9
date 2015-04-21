'use strict';
app.factory('$myhttp', ["$http", '$q', 'utilService', 'authService', 'localStorageService',
    function ($http, $q, utilService, authService, localStorageService) {
        var myHttpFactory = {};

        myHttpFactory.ping = function () {
            return myHttpFactory.get(APIURL + 'api/account/ping');
        }

        myHttpFactory.get = function (url) {
            var deferred = $q.defer();
            $http.get(url).success(function (data) {
                deferred.resolve(data);
            }).error(function (err, statusCode) {
                if (statusCode === 401) {
                    var authData = localStorageService.get('authorizationData');
                    if (authData) {
                        if (authData.useRefreshTokens) {
                            authService.refreshToken().then(function () {
                                myHttpFactory.get(url).then(function (data) {
                                    deferred.resolve(data);
                                });
                            });
                        }
                        else if (authData.useExternalTokenProvider) {
                            var externalData = {
                                provider: authData.externalProvider,
                                externalAccessToken: authData.externalTokenProvider
                            };
                            authService.obtainAccessToken(externalData).then(function (data) {
                                myHttpFactory.get(url).then(function (data) {
                                    deferred.resolve(data);
                                });
                            });
                        }
                    }
                } else {
                    utilService.errorCallback(err);
                    deferred.reject(err);
                }
            });
            return deferred.promise;
        }

        myHttpFactory.post = function (url, object) {
            var deferred = $q.defer();
            $http.post(url, object).success(function (data) {
                deferred.resolve(data);
            }).error(function (err, statusCode) {
                if (statusCode === 401) {
                    var authData = localStorageService.get('authorizationData');
                    if (authData) {
                        if (authData.useRefreshTokens) {
                            authService.refreshToken().then(function () {
                                myHttpFactory.post(url, object).then(function (data) {
                                    deferred.resolve(data);
                                });
                            });
                        }
                        else if (authData.useExternalTokenProvider) {
                            var externalData = {
                                provider: authData.externalProvider,
                                externalAccessToken: authData.externalTokenProvider
                            };
                            authService.obtainAccessToken(externalData).then(function (data) {
                                myHttpFactory.post(url, object).then(function (data) {
                                    deferred.resolve(data);
                                });
                            });
                        }
                    }
                } else {
                    utilService.errorCallback(err);
                    deferred.reject(err);
                }
            });
            return deferred.promise;
        }

        

        return myHttpFactory;
    }
]);