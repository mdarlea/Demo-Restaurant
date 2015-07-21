(function () {
    'use strict';

    /**
    * 
    * @ngdoc object
    * @name appRestaurant.homeController
    * @requires $scope
    * @requires $location
    * @requires $authService

    * @description Home controller
    */

    angular.module('appRestaurant').controller('homeController',
        ['$scope', '$location', '$authService', function ($scope, $location, $authService) {
            /**
            * @ngdoc property
            * @name appRestaurant.homeController#auth
            * @propertyOf appRestaurant.homeController
            * @description 
            * @returns {object} {@link swAuth.$authService#authentication authentication} property of the {@link swAuth.$authService $authService} service
            */
            $scope.auth = $authService.authentication;

            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            }

            /**
            * @ngdoc event
            * @name appRestaurant.homeController#userAuthenticated
            * @eventOf appRestaurant.homeController   
            * @eventType emit on $scope
            */
            $scope.$on('userAuthenticated', function (event, data) {
               
            });
        }]);
})();