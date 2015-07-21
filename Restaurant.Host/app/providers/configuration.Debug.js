(function () {
    'use strict';

    angular.module('appRestaurant').config(['swAppSettingsProvider', function (swAppSettingsProvider) {
        swAppSettingsProvider.setSettings({
            apiServiceBaseUri: "http://localhost:20178/",
            clientId: 'RestaurantDemoTestApp'
        });
    }]);
})();