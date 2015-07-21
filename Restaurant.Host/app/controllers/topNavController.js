(function () {
    'use strict';
    angular.module('app').controller('topNavController',
        ['$scope', '$location', '$authService', function ($scope, $location, $authService) {
            $scope.auth = $authService.authentication;

            $scope.title = "Chinese Restaurant";

            $scope.getUserName = function () {
                return $authService.authentication.userName;
            };

            $scope.logOut = function () {
                $authService.logOut();
                $location.path("/login");
            };
        }]);
})();