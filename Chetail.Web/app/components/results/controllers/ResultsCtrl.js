(function () {
    'use strict';

    angular
        .module('app')
        .controller('ResultsCtrl', ResultsCtrl);

    //Injects 
    ResultsCtrl.$inject = ['cartFactory', 'toaster', 'wholesalersFactory'];

    function ResultsCtrl(cartFactory, toaster, wholesalersFactory) {
        var vm = this;
        vm.cartResult = {}; 

        vm.states = {
            loading: false
        }

        ////////////////////

        showCartResult();

        function showCartResult() {
            //Retrieves cart result from service
            vm.cartResult = cartFactory.getCartResult();

            //Sets top item to expanded
            if (vm.cartResult.cartResultItems != null) {
                for (var i = 0; i < vm.cartResult.cartResultItems.length; i++) {
                    if (i ==0)
                        vm.cartResult.cartResultItems[i].isOpen = true;
                    else
                        vm.cartResult.cartResultItems[i].isOpen = false;
                }
            }        
        }

        //Show Toast Notification
        function showToastNotification(_type, _title, _body, _showCloseButton) {
            toaster.pop({
                type: _type,
                title: _title,
                body: _body,
                showCloseButton: _showCloseButton
            });
        }

        //Sets the controller to loading state
        function setLoading(loading) {
            vm.states.loading = loading;
        }
    }

})();