(function () {
    'use strict';

    angular.module('app')
   .factory('cartFactory', cartFactory);

    //Injects $http, $q
    cartFactory.$inject = ['httpChetail', 'apiUrl', '$q', 'localStorageService'];

    function cartFactory(httpChetail, apiUrl, $q, localStorageService) {

        // API Resource
        var apiResource = apiUrl + 'api/cart';  

        // Stores Objects
        // -- Shopping Cart
        var _cart = {
            items: [],
            totalItemCount:0
        }

        var _cartResult = {};

        _getCartFromLocalStorage();
        _updateCart();
       
        // Public Interface
        var factory = {
            cart: _cart,
            getCartFromLocalStorage:_getCartFromLocalStorage,
            addItem: _addItem,
            removeItem: _removeItem,
            updateCart: _updateCart,
            postCartResult: _postCartResult,
            getCartResult: _getCartResult
        };
        return factory;

        ////////////

        //Posts cart result
        function _postCartResult(wholesalers) {

            //Clears previous results
            _cartResult = {};

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiResource;

            //Collate results
            var cartToPost = _collateCartForPost(wholesalers);

            httpChetail.post(_apiCall, cartToPost)
            .then(function (result) {
                //Successful

                _cartResult = result.data;

                deferred.resolve();
            },
            function () {
                //Error
                deferred.reject();
            });
            return deferred.promise;

        }

        //Gets cart result
        function _getCartResult() {

            return _cartResult;

        }

        function _collateCartForPost(wholesalers) {

            var cartToPost = {
                products: _cart.items,
                wholesalers: wholesalers,
                retailerId:1
            }

            return cartToPost;     
        }

        // Adds item to cart
        function _addItem(item) {

            //Does this item already exist in the cart?
            var index = -1;
            for (var i = 0; i < _cart.items.length; i++) {
                if (item.code == _cart.items[i].code) {
                    index = i;
                }
            }

            if (index != -1) {
                //Item already exists in cart - update qty
                _cart.items[index].qty += 1;
            }
            else {
                //Give a default qty of 1
                item.qty = 1;

                _cart.items.push(item);
            }       

            //Updates cart
            _updateCart();

            //updates local storage
            _updateLocalStorage();
        };

        // Removes item from cart
        function _removeItem(index) {
            _cart.items.splice(index, 1);

            //Updates cart
            _updateCart();

            //updates local storage
            _updateLocalStorage();
        };

        // Removes item from cart
        function _updateCart() {
            var cumTot = 0;
            for (var i = 0; i < _cart.items.length; i++) {
                cumTot = +cumTot + +_cart.items[i].qty;
            }
            _cart.totalItemCount = cumTot;
            _updateLocalStorage();

        };

        // Updates local storage
        function _updateLocalStorage() {
            localStorageService.set('chetailCart', _cart);
        }
        // Gets Cart from local storage
        function _getCartFromLocalStorage() {
            var storedCart = localStorageService.get('chetailCart');
            if (storedCart) {
                _cart = storedCart;
            }

        }

    }

})();