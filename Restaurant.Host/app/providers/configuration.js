(function () {
    'use strict';

    angular.module('app').config(['swAppSettingsProvider', function (swAppSettingsProvider) {
        swAppSettingsProvider.setSettings({
            apiServiceBaseUri: "http://www.swaksoft.com/",
            clientId: "RestaurantDemoApp"
        });
    }]);
})();