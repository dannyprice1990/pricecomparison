(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductDetailCtrl', ProductDetailCtrl);

    //Injects 
    ProductDetailCtrl.$inject = ['productsFactory', 'authFactory', '$routeParams', 'cartFactory', 'wholesalersFactory','toaster'];

    function ProductDetailCtrl(productsFactory, authFactory, $routeParams, cartFactory, wholesalersFactory, toaster) {
        var vm = this;
        vm.product = {};
        vm.addToCart = addToCart;
        vm.productPrices = [];
        vm.wholesalers = wholesalersFactory.wholesalers;
        vm.states = {
            loading: false
        }
        vm.addProductPrice = addProductPrice;
        vm.deleteProductPrice = deleteProductPrice;

        //Current Authentication Information
        vm.authentication = authFactory.authentication;

        ////////////////////

        //Refresh list of wholesalers
        wholesalersFactory.getWholesalers();

        getProduct();

        //Gets product
        function getProduct() {

            //Sets loading to true
            setLoading(true);

            var productId = $routeParams.productId;

            //Has a product been passed in?
            if (typeof productId === 'undefined' || productId === null) {
                // No
               

            }
            else {
                productsFactory.getProduct(productId)
          .then(function (data) {
              //Success
              vm.product = data.data;

              getProductPrices();

          },
          function () {
              //Error

          }).then(function () {
              //Sets loading to false
              setLoading(false);
          });
            }            
        }

        //Gets product prices
        function getProductPrices() {

            //Sets loading to true
            setLoading(true);

            var productId = $routeParams.productId;

            //Has a product been passed in?
            if (typeof productId === 'undefined' || productId === null) {
                // No


            }
            else {
                productsFactory.getProductPrices(productId)
          .then(function (data) {
              //Success
              vm.productPrices = data.data;
          },
          function () {
              //Error

          }).then(function () {
              //Sets loading to false
              setLoading(false);
          });
            }

        }

        //Calls the addProductPrice function within our Product Service
        function addProductPrice(productPrice) {

            //Sets loading to true
            setLoading(true);

            //Adds product id
            productPrice.productId = vm.product.id;

            productsFactory.addProductPrice(vm.product.id, productPrice)
               .then(function (data) {

                   //Shows a toast notification
                   showToastNotification('success', 'Price Added', 'Price added successfully', true);

                   //Success
                   getProductPrices();
               },
          function (error) {
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

        //Add to cart
        function addToCart(product) {
            cartFactory.addItem(product);

            //Shows a toast notification
            showToastNotification('success', 'Item added to cart', "'" + product.desc + "'" + ' added to cart', true);

        }
      
        //Calls the deleteProductPrice function within our Product Service
        function deleteProductPrice(productPrice) {

            productsFactory.deleteProductPrice(vm.product.id, productPrice.id)
               .then(function () {
                   //Success
               
                   //Shows a toast notification
                   showToastNotification('success', 'Price Removed', 'Price removed successfully', true);

                   getProductPrices();
               },
          function () {
              //Error

          }).then(function () {
              //Sets loading to false
              setLoading(false);
          });
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
    }

   

})();