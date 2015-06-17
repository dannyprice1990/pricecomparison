(function () {
    'use strict';

    angular.module('app')
   .factory('wholesalersFactory', wholesalersFactory);

    //Injects $http, $q
    wholesalersFactory.$inject = ['httpChetail', 'apiUrl', '$q'];

    function wholesalersFactory(httpChetail, apiUrl, $q) {

        //API Resource
        var apiWholesalersResource = apiUrl + 'api/wholesalers';

        //Default Page Sizes

        //Stores Objects
        var _wholesalers = [];
        var _wholesalersLoaded = false;

        //Public Interface
        var dataFactory = {
            wholesalers: _wholesalers,
            getWholesalers: _getWholesalers,
            addWholesaler: _addWholesaler,
            updateWholesaler: _updateWholesaler,
            deleteWholesaler: _deleteWholesaler,
            wholesalersLoaded: _wholesalersLoaded,
            getCheckedWholesalers: _getCheckedWholesalers,
            updateChecked: _updateChecked

        };
        return dataFactory;

        ////////////

        //Loads in Wholesalers
        function _getWholesalers() {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiWholesalersResource;

            httpChetail.get(_apiCall)
            .then(function (result) {
                //Successful
                angular.copy(result.data, _wholesalers);

                //Adds a checked flag to each wholesaler
                for (var i = 0; i < _wholesalers.length; i++) {
                    _wholesalers[i].checked = true;
                }

                deferred.resolve();
            },
            function () {
                //Error
                deferred.reject();
            });

            return deferred.promise;
        }

        //Gets checked Wholesalers
        function _getCheckedWholesalers() {

            var checkedWholesalers = [];
            for (var i = 0; i < _wholesalers.length; i++) {
                if (_wholesalers[i].checked == true) {
                    checkedWholesalers.push(_wholesalers[i]);
                }
            }

            return checkedWholesalers;
        }

        // Adds new Wholesaler
        function _addWholesaler(newWholesaler) {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiWholesalersResource;

            httpChetail.post(apiWholesalersResource, newWholesaler)
            .then(function (result) {
                //Successful
                _wholesalers.splice(0, 0, result.data);
                deferred.resolve(result.data);
            },
            function () {
                //Error
                deferred.reject();
            });
            return deferred.promise;
        };

        // Updates existing Wholesaler
        function _updateWholesaler(updatedWholesaler) {
            return httpChetail.put(apiWholesalersResource + '/' + updatedWholesaler.id, updatedWholesaler)
        };

        // Deletes Wholesaler
        function _deleteWholesaler(id) {
            return httpChetail.delete(apiWholesalersResource + '/' + id);
        };

        //Updates checked flag against wholesaler
        function _updateChecked(wholesaler) {
            for (var i = 0; i < _wholesalers.length; i++) {
                if (wholesaler.id == _wholesalers[i].id) {
                    _wholesalers[i].checked == wholesaler.checked;
                }
            }
        }

    }

})();

