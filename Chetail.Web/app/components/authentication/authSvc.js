(function() {
    'use strict';

    angular.module('app')
   .factory('authFactory', authFactory);

    //Injects $http, $q
    authFactory.$inject = ['httpChetail', '$q', 'localStorageService', 'apiUrl'];

    function authFactory(httpChetail, $q, localStorageService, apiUrl) {

        //API Resource
        var apiResource = apiUrl;

        //Public Interface
        var authFactory = {
            saveRegistration: _saveRegistration,
            login: _login,
            logOut: _logOut,
            fillAuthData: _fillAuthData,
            authentication: {
                isAuth: false,
                userName: ""
            }
        };
        return authFactory;

        //Public Interface

        ////////////
      
        function _saveRegistration(registration) {

            _logOut();

            return httpChetail.post(apiResource + 'account/register', registration).then(function (response) {
                return response;
            });

        };

        function _login(loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            httpChetail.post(apiResource + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

                authFactory.authentication.isAuth = true;
                authFactory.authentication.userName = loginData.userName;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        function _logOut() {

            localStorageService.remove('authorizationData');

            authFactory.authentication.isAuth = false;
            authFactory.authentication.userName = "";

        };

        function _fillAuthData () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                authFactory.authentication.isAuth = true;
                authFactory.authentication.userName = authData.userName;
            }

        }

    }

})();