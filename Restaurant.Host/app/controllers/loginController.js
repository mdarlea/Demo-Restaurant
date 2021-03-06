﻿(function () {
    'use strict';
    angular.module('appRestaurant').controller('loginController',
        ['$scope', '$location', '$authService', 'swAppSettings', function ($scope, $location, $authService, swAppSettings) {
            $scope.auth = $authService.authentication;

            if (!$authService.authentication.isAuth) {

            }

            $scope.loginData = {
                userName: null,
                password:null
            };

            $scope.login = function (isValid) {
                if (!isValid) {
                    $scope.message = "Invalid form data";
                    return false;
                }

                $scope.loginData.useRefreshTokens = true;
                $authService.login($scope.loginData)
                    .then(function (response) {
                        $location.path(swAppSettings.indexPage);
                    },
                 function (err) {
                     $scope.message = err.error_description;
                 });
            };
        }]);
})();