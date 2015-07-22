(function () {
    'use strict';
    angular.module('appRestaurant').controller('registerController',
        ['$rootScope', '$scope', '$location', '$timeout', '$authService', 'swAppSettings',
         function ($rootScope, $scope, $location, $timeout, $authService, swAppSettings) {
            $scope.savedSuccessfully = false;
            $scope.message = "";
            $scope.registerData = {
                email: null,
                hometown: null,
                password: null,
                confirmPassword: null
            };
             
            $scope.emailExpr = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\b/;

            $scope.register = function (isValid) {
                // check to make sure the form is completely valid
                if (!isValid) {
                    $scope.message = "Invalid form data";
                    return false;
                }

                var startTimer = function () {
                    var timer = $timeout(function () {
                        $timeout.cancel(timer);
                        $location.path(swAppSettings.indexPage);
                    }, 2000);
                }

                $scope.loading = true;
                $authService.register($scope.registerData)
                    .then(function (response) {
                        $scope.savedSuccessfully = true;
                        $scope.message = "User has been registered successfully, you will be redicted to the home page in 2 seconds.";

                        $rootScope.$broadcast('userAuthenticated', true);

                        startTimer();
                    }, function (response) {
                        var errors = [];
                        for (var key in response.ModelState) {
                            errors.push(response.ModelState[key]);
                        }
                        $scope.message = "Failed to register user due to:" + errors.join(' ');

                        $rootScope.$broadcast('userAuthenticated', false);
                    }).finally(function (response) {
                        $scope.loading = false;
                    });
            };
        }]);
})();