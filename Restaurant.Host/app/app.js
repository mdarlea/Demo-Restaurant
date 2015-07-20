var app = angular.module('app',
    ['ngRoute',
        'ui.bootstrap',
        'ngResource',
        'LocalStorageModule',
        'angular-loading-bar',
        'sw.common',
        'sw.ui.bootstrap']);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.when("/home", {
            controller: "homeController",
            templateUrl: "/app/views/home.html"
        });

        $routeProvider.when("/register", {
            controller: "registerController",
            templateUrl: "/app/views/register.html"
        });
    }
]);

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.config(function ($provide) {
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

app.run(['$authService', function ($authService) {
    $authService.fillAuthData();
}]);