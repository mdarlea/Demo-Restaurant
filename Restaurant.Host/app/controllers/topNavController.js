(function () {
    'use strict';
    angular.module('app').controller('topNavController',
        ['$scope', '$authService', function ($scope, $authService) {
            $scope.isAuth = $authService.authentication.isAuth;

            $scope.title = "Chinese Restaurant";
        }]);
})();