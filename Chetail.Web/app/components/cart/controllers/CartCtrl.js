(function () {
    'use strict';

    angular
        .module('app')
        .controller('CartCtrl', CartCtrl);

    //Injects 
    CartCtrl.$inject = ['cartFactory', 'toaster','wholesalersFactory','$location'];

    function CartCtrl(cartFactory, toaster, wholesalersFactory,$location) {
        var vm = this;
        vm.cart = cartFactory.cart;
        vm.addToCart = addToCart;
        vm.removeFromCart = removeFromCart;
        vm.showToastNotification = showToastNotification;
        vm.updateCart = updateCart;
        vm.postCartResult = postCartResult;
        vm.wholesalers = wholesalersFactory.wholesalers;
        vm.states = {
            loading: false
        }

        ////////////////////

        //Add to cart
        function addToCart(product) {
            cartFactory.addItem(product);

            //Shows a toast notification
            showToastNotification('success', 'Item added to cart', "'" + product.desc + "'" + ' added to cart', true);
        }

        //Remove from cart
        function removeFromCart(index) {

            //Shows a toast notification
            showToastNotification('success', 'Item removed from cart', "'" + cartFactory.cart.items[index].desc + "'" + ' removed from cart', true);

            cartFactory.removeItem(index);
        }

        //Update cart
        function updateCart() {
            cartFactory.updateCart();

            //Shows a toast notification
            showToastNotification('success', 'Cart Updated', 'Cart was updated successfully', true);

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

        //POSTS cart result
        function postCartResult() {

                //Sets loading to true
                setLoading(true);

                var checkedWholesalers = wholesalersFactory.getCheckedWholesalers();

            cartFactory.postCartResult(checkedWholesalers)
                   .then(function () {
                       //Success
                       //Navigate to results
                       $location.url('/results');
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

    }

})();