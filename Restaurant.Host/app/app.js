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
        'sw.ui.bootstrap',
        'ui.grid']);

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

        $routeProvider.when("/reservation", {
            controller: "reservationController",
            templateUrl: "/app/views/reservation-new.html"
        });

        $routeProvider.when("/reservation/:id/edit", {
            controller: "reservationController",
            templateUrl: "/app/views/reservation-edit.html"
        });

        $routeProvider.when("/reservations", {
            controller: "reservationsController",
            templateUrl: "/app/views/reservations.html"
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
            },
            roundTime: function(time,round) {
                var timeToReturn = new Date(time);
                var roundTime = round || 15;

                timeToReturn.setMilliseconds(Math.round(time.getMilliseconds() / 1000) * 1000);
                timeToReturn.setSeconds(Math.round(timeToReturn.getSeconds() / 60) * 60);
                timeToReturn.setMinutes(Math.round(timeToReturn.getMinutes() / roundTime) * roundTime);
                return timeToReturn;
            }
        });

        return $delegate;
    });
});

appRestaurant.run(['$authService', function ($authService) {
    $authService.fillAuthData();
}]);