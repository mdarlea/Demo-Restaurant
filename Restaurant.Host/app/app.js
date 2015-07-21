/**
* @ngdoc overview
* @name appRestaurant
* @description
* Main application modue.
*/
var appRestaurant = angular.module('appRestaurant',
    ['ngRoute',
        'ui.bootstrap',
        'ngResource',
        'LocalStorageModule',
        'angular-loading-bar',
        'sw.common',
        'sw.ui.bootstrap']);

appRestaurant.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.when("/register", {
            controller: "registerController",
            templateUrl: "/app/views/register.html"
        });

        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "/app/views/login.html"
        });

        $routeProvider.when("/home", {
            controller: "homeController",
            templateUrl: "/app/views/home.html"
        });
    }
]);

appRestaurant.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

appRestaurant.config(function ($provide) {
    $provide.decorator('$utilities', function ($delegate) {
        angular.extend($delegate, {
            getProviderName: function (root, url) {
                var pos = url.indexOf("?");
                if (pos < 0) {
                    pos = url.length - 1;
                }
                var len = root.length;
                return url.substr(len, pos - len);
            }
        });

        return $delegate;
    });
});

appRestaurant.run(['$authService', function ($authService) {
    $authService.fillAuthData();
}]);