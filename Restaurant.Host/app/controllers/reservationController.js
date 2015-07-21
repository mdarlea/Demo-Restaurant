(function () {
    'use strict';

    /**
    * 
    * @ngdoc object
    * @name appRestaurant.reservationController
    * @requires $scope
    * @requires $location
    * @requires $authService

    * @description Reservation controller
    */

    angular.module('appRestaurant').controller('reservationController',
        ['$scope', '$location', '$authService', function ($scope, $location, $authService) {
            //redirect to home page if not authorized
            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            }
            
            $scope.timeOptions = {
                required: true,
                hstep: 1,
                mstep: 0
            }
        }]);
})();