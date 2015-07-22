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

    * @description Controller used to display all reservations
    */

    angular.module('appRestaurant').controller('reservationsController',
        ['$scope', '$location', '$authService', '$reservationService',
        function ($scope, $location, $authService, $reservationService) {

            //redirect to home page if not authorized
            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            };
            
            $scope.loading = true;
            $scope.message = "Please wait while I am loading the reservations...";

            var reservationNameTemplate = "<a href=\'/#/reservation/{{row.entity.id}}/edit\'>{{row.entity[col.field]}}</a>";
            var reservationDateTemplate = '<div class="ui-grid-cell-contents">{{row.entity[col.field] | date:\'shortDate\'}}</div>';
            var reservationTimeTemplate = '<div class="ui-grid-cell-contents">{{row.entity.reservationDateTime | date:\'shortTime\'}}</div>';

            $scope.gridOptions = {
                enableFiltering: true,
                paginationPageSizes: [25, 50, 75],
                paginationPageSize: 25,
                data: 'data',
                columnDefs: [
                  { displayName: 'Reserved By', field: 'name', cellTemplate: reservationNameTemplate },
                  { displayName: 'Reservation Date', field: 'reservationDateTime', cellTemplate: reservationDateTemplate },
                  { displayName: 'Reservation Time', field: 'id', cellTemplate: reservationTimeTemplate },
                  { displayName: '# Of Guests', field: 'guestsCount' }
                ]
            };

            $scope.errMessage = null;
            
            //get all the reservations
            $reservationService.getAllReservations().$promise
            .then(function (data) {
                $scope.data = data;
            }, function (err) {
                $scope.errMessage = err.data && err.data.message;
            }).finally(function (response) {
                $scope.loading = false;
                $scope.message = null;
            });
        }]);
})();