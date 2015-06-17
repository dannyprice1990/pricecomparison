(function() {
    'use strict';

    angular.module('app')
   .factory('authInterceptorFactory', authInterceptorFactory);

    //Injects $http, $q
    authInterceptorFactory.$inject = ['$q', '$location', 'localStorageService'];

    function authInterceptorFactory($q, $location, localStorageService) {

        //Public Interface
        authInterceptorFactory.responseError = _responseError;

        ////////////

        function _responseError(rejection) {
            if (rejection.status === 401) {
                $location.path('/login');
            }
            return $q.reject(rejection);
        }      

        return authInterceptorFactory;

    }

})();