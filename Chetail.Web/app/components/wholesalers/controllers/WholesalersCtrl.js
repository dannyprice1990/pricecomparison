(function () {
    'use strict';

    angular
        .module('app')
        .controller('WholesalersCtrl', WholesalersCtrl);

    //Injects 
    WholesalersCtrl.$inject = ['wholesalersFactory', 'authFactory'];

    function WholesalersCtrl(wholesalersFactory, authFactory) {
        var vm = this;
        vm.addWholesaler = addWholesaler;
        vm.wholesalers = wholesalersFactory.wholesalers;
        vm.updatechecked = updateChecked;
        vm.pagination = {};
        vm.states = {
            loading: false
        }

        //Current Authentication Information
        vm.authentication = authFactory.authentication;

        ////////////////////

        //If wholesalers are yet to be loaded
        if (wholesalersFactory.wholesalersLoaded == false) {
            getWholesalers();
        }

        //Gets a list of wholesalers
        function getWholesalers() {
            //Sets loading to true
            setLoading(true);

            wholesalersFactory.getWholesalers()
            .then(function () {
                //Success

            },
            function () {
                //Error

            })
            .then(function () {
                //Sets loading to false
                setLoading(false);
            });
        }

        //Calls the deleteWholesaler function within our Wholesalers Service
        function deleteWholesaler(wholesaler) {

            //Sets loading to true
            setLoading(true);

            //Calls delete on service
            wholesalersFactory.deleteWholesaler(wholesaler.id)
              .then(function () {
                  //Success

              },
            function () {
                //Error

            })
            .then(function () {
                //Sets loading to false
                setLoading(false);
            });
        }

        //Calls the addWholesaler function within our Wholesalers Service
        function addWholesaler(wholesaler) {

            //Sets loading to true
            setLoading(true);

            wholesalersFactory.addWholesaler(wholesaler)
                .then(function () {
                    //Success

                },
            function () {
                //Error

            })
            .then(function () {
                //Sets loading to false
                setLoading(false);
            });

        }

        //Calls the updateWholesaler function within our Wholesaler Service
        function updateWholesaler(wholesaler) {

            //Sets loading to true
            setLoading(true);

            wholesalersFactory.updateWholesaler(wholesaler)
                .then(function () {
                    //Success

                },
            function () {
                //Error

            })
            .then(function () {
                //Sets loading to false
                setLoading(false);
            });
        }

        //Sets the controller to loading state
        function setLoading(loading) {
            vm.states.loading = loading;
        }

        function updateChecked(wholesaler) {

        }

    }

})();