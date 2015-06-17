(function () {
    'use strict';

    angular.module('app')
   .factory('httpChetail', httpChetail);

    //Injects $http,
    httpChetail.$inject = ['$http', 'localStorageService'];

    //httpChetail is a wrapper for HTTP calls we will use for authenticating against the Chetail Web API
    //The reason we have created this custom wrapper is because we want all calls to pass in the authenticated token (if any)
    //The reason we couldn't use an interceptor was because we make HTTP calls to other resources

    function httpChetail($http, localStorageService) {

        //Public Interface

        // The service object to be returned (`$http` wrapper)
        var serviceObj = {};

        ////////////

        // Augments the request configuration object 
        // with OAuth specific stuff (e.g. some header)
        function getAugmentedConfig(cfg) {
            var config = cfg || {};
            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }       

        // Create wrappers for methods WITHOUT data
        ['delete', 'get', 'head', 'jsonp'].forEach(function (method) {
            serviceObj[method] = function (url, config) {
                var config = getAugmentedConfig(config);
                return $http[method](url, config);
            };
        });

        // Create wrappers for methods WITH data
        ['post', 'put'].forEach(function (method) {
            serviceObj[method] = function (url, data, config) {
                var config = getAugmentedConfig(config);
                return $http[method](url, data, config);
            };
        });

        // Return the service object
        return serviceObj;

    }

})();