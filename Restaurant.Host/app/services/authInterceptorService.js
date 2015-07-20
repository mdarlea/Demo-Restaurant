(function () {
    'use strict';
    angular.module('app').factory('authInterceptorService', ['$q', '$injector', '$location',
        function ($q, $injector, $location) {

            var authInterceptorServiceFactory = {
                request: function (config) {
                    $("#spinner").show();

                    config.headers = config.headers || {};

                    var factory = $injector.get("$authenticationTokenFactory");

                    var authData = factory.getToken();
                    if (authData && !authData.isExpired()) {
                        config.headers.Authorization = 'Bearer ' + authData.token.access_token;
                    }

                    return config;
                },

                response: function (response) {
                    if (response.status === 401) {
                        // handle the case where the user is not authenticated
                    }

                    $("#spinner").hide();
                    return response || $q.when(response);
                },

                responseError: function (rejection) {
                    $("#spinner").hide();
                    if (rejection.status === 401) {
                        var authService = $injector.get('$authService');
                        var factory = $injector.get("$authenticationTokenFactory");

                        var authData = factory.getToken();

                        if (authData) {
                            if (authData.token.useRefreshTokens) {
                                $location.path('/refresh');
                                return $q.reject(rejection);
                            }
                        }
                        authService.logOut();
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };

            return authInterceptorServiceFactory;
        }]);
})();