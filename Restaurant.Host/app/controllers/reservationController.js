(function () {
    'use strict';

    /**
    * 
    * @ngdoc object
    * @name appRestaurant.reservationController
    * @requires $scope
    * @requires $location
    * @requires $authService
    * @requires appRestaurant.$reservationService
    * @requires swCommon.$utilities
    * @requires $routeParams

    * @description Reservation controller responsible for creating, updating and removing reservations
    */

    angular.module('appRestaurant').controller('reservationController',
        ['$scope', '$location', '$authService', '$reservationService','$utilities','$routeParams',
        function ($scope, $location, $authService, $reservationService, $utilities, $routeParams) {

            //redirect to home page if not authorized
            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            };

            $scope.numberExpr = /^[1-9][0-9]*$/;

            //check if this is a request for an existinng reservation
            if ($routeParams.id) {
                var id = parseInt($routeParams.id);
                if (id) {
                    $scope.loading = true;
                    $scope.message = "Please wait while I am loading the reservation...";

                    //loads the reservation
                    $reservationService.getReservation({id:id}).$promise
                        .then(function (response) {
                            $scope.data = response;

                            //the reservation date and time
                            var reservationDateTime = $scope.data.reservationDateTime;
                            if (reservationDateTime) {
                                var date = new Date(reservationDateTime);
                                $scope.data.reservationDate = new Date(date);
                                $scope.data.reservationTime = new Date(date.getTime());
                            }
                    }, function (err) {
                            $scope.errMessage = err.data && err.data.message;
                        }).finally(function (response) {
                            $scope.loading = false;
                            $scope.message = null;
                    });
                }
            }

            if (!$scope.data) {
                $scope.data = {
                    reservationTime: $utilities.roundTime(new Date(new Date().getTime()), 30)
                };
            }

            //function that converts the reservation date and time into a string to fix the MVC date issue
            var updateReservationDateTime = function () {
                var data = $scope.data;

                if (!data.reservationTime) {
                    return false;
                }

                var time = new Date(data.reservationTime);
                data.hours = time.getHours();
                data.minutes = time.getMinutes();
                
                return true;
            }
            
            /**
            * @ngdoc function
            * @name appRestaurant.reservationController#addNewReservation
            * @methodOf appRestaurant.reservationController
            * @param {boolean} isValid True if the "Registration" form passed validation, False otherwise
            * @description Submits a new reservation after all validation has occurred  
            */
            $scope.addNewReservation = function (isValid) {

                // check to make sure the form is completely valid
                if (!isValid || !updateReservationDateTime()) {
                    $scope.errMessage = "Invalid form data";
                    return false;
                }

                $scope.message = "Please wait while I am adding the reservation into the system ...";
                $scope.errMessage = null;
                $scope.loading = true;
                    
                $reservationService.addNewReservation($scope.data).$promise
                    .then(function (response) {
                        $location.path('/reservations');
                    }, function (err) {
                        $scope.errMessage = err.data && err.data.message;
                    })
                    .finally(function (response) {
                        $scope.loading = false;
                        $scope.message = null;
                });
            };

            //function to change an existing reservation
            /**
            * @ngdoc function
            * @name appRestaurant.reservationController#updateReservation
            * @methodOf appRestaurant.reservationController
            * @param {boolean} isValid True if the "Registration" form passed validation, False otherwise
            * @description Changes an existing reservation if validation passed
            */
            $scope.updateReservation = function (isValid) {
                if (!id) {
                    //nothing to do if there is no id
                    return false;
                }

                // check to make sure the form is completely valid
                if (!isValid || !updateReservationDateTime()) {
                    $scope.errMessage = "Invalid form data";
                    return false;
                }

                $scope.message = "Please wait while I am updating the reservation ...";
                $scope.errMessage = null;
                $scope.loading = true;

                $reservationService.updateReservation({ id: id }, $scope.data).$promise
                    .then(function(response) {
                        $location.path('/reservations');
                    }, function(err) {
                        $scope.errMessage = err.data && err.data.message;
                    })
                    .finally(function(response) {
                        $scope.loading = false;
                        $scope.message = null;
                    });
            };

        }]);
})();