(function () {
    'use strict';

    angular.module('app').config(['swAppSettingsProvider', function (swAppSettingsProvider) {
        swAppSettingsProvider.setSettings({
            apiServiceBaseUri: "http://localhost:20178/",
            clientId: 'RestaurantDemoTestApp'
        });
    }]);
})();