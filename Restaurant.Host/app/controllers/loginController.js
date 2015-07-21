(function () {
    'use strict';
    angular.module('app').controller('loginController',
        ['$scope', '$location', '$authService', function ($scope, $location, $authService) {
            $scope.isAuth = $authService.authentication.isAuth;

            if (!$authService.authentication.isAuth) {

            }
        }]);
})();