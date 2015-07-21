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
             
            $scope.register = function () {

                var startTimer = function () {
                    var timer = $timeout(function () {
                        $timeout.cancel(timer);
                        $location.path(swAppSettings.indexPage);
                    }, 2000);
                }

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
                    });
            };

        }]);
})();