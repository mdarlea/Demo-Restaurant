(function () {
    'use strict';
    angular.module('app').controller('homeController',
        ['$scope', '$location', '$authService', function ($scope, $location, $authService) {
            $scope.auth = $authService.authentication;

            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            }

            $scope.$on('userAuthenticated', function (event, data) {
               
            });
        }]);
})();