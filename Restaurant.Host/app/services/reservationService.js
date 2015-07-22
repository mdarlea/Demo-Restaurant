(function () {
    'use strict';

    angular.module('appRestaurant').factory('$reservationService',
        ['$resource', 'swAppSettings',
        function ($resource, swAppSettings) {
            return $resource(
                swAppSettings.apiServiceBaseUri + 'api/reservation/:action',
                {
                     action: '@action'
                },
                {
                    'addNewReservation': {
                        method: 'POST',
                        params: { action: 'addNewReservation' },
                        isArray: false
                    }
                });
        }]);
})();