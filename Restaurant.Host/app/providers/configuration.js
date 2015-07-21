(function () {
    'use strict';

    angular.module('appRestaurant').config(['swAppSettingsProvider', function (swAppSettingsProvider) {
        swAppSettingsProvider.setSettings({
            apiServiceBaseUri: "http://www.swaksoft.com/",
            clientId: "RestaurantDemoApp"
        });
    }]);
})();