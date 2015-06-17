(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductsCtrl', ProductsCtrl);

    //Injects 
    ProductsCtrl.$inject = ['productsFactory', 'authFactory', '$routeParams', 'cartFactory', 'toaster'];

    function ProductsCtrl(productsFactory, authFactory, $routeParams,cartFactory, toaster) {
        var vm = this;
        vm.addProduct = addProduct;
        vm.addToCart = addToCart;
        vm.productCategories = productsFactory.productCategories;
        vm.products = [];
        vm.pagination = {};
        vm.states = {
            loading: false
        }
        vm.selectedProductCategory = {};
        vm.showToastNotification = showToastNotification;

        //Current Authentication Information
        vm.authentication = authFactory.authentication;

        ////////////////////

        getProducts();

        //Gets a list of products
        function getProducts(page) {
            //Sets loading to true
            setLoading(true);

            var productCategoryId = $routeParams.productCategory;

            //Has a product category been passed in?
            if (typeof productCategoryId === 'undefined' || productCategoryId === null) { 
                productCategoryId=0;
            };

            productsFactory.getProductCategory(productCategoryId)
            .then(function (data) {
                //Success
                if (typeof data === 'undefined') {
                    //All
                    vm.selectedProductCategory.name = "All Products";
                }
                else {
                    //Store selected product category
                    vm.selectedProductCategory = data.data;
                }             
            },
            function () {
                //Error
                //Invalid product category id
                productCategoryId = -1;
            })

            .then(function () {
                productsFactory.getProducts(page, productCategoryId)
                .then(function (data) {
                    //Success
                    vm.products = data.data;

                    //Pagination
                    vm.pagination = angular.fromJson(data.headers("x-pagination"));
                },
            function () {
                //Error

            })
            .then(function () {
                //Sets loading to false
                setLoading(false);
            });

            });
            
        }

        ////Go to next page of results
        //function nextPage() {
        //    getBooks(vm.data.recentBooks.pagination.currentPage + 1);
        //}

        //Go to previous page of results
        function prevPage() {
            getBooks(vm.data.recentBooks.pagination.currentPage - 1);
        }

        //Calls the deleteProduct function within our Product Service
        function deleteProduct(product) {

            //Sets loading to true
            setLoading(true);

            productsFactory.deleteProduct(product.id)
                 .then(function (data) {
                     //Success
                     getProducts();
                 },
            function () {
                //Error

            })
            .then(function () {
                //Sets loading to false
                setLoading(false);
            });
        }

        //Calls the addProduct function within our Product Service
        function addProduct(product) {

            //Sets loading to true
            setLoading(true);

            productsFactory.addProduct(product)
               .then(function (data) {
                   //Success
                   getProducts();
               },
          function (error) {
              //Error

              //Shows a toast notification

              showToastNotification('error', 'Error adding product', 'Error adding product. Please check the product code does not already exist', true);

          })
          .then(function () {
              //Sets loading to false
              setLoading(false);
          });
        }

        //Calls the updateProduct function within our Product Service
        function updateProduct(book) {

            //Sets loading to true
            setLoading(true);

            productsFactory.updateProduct(product)
              .then(function (data) {
                  //Success
                  getProducts();
              },
         function () {
             //Error

         })
         .then(function () {
             //Sets loading to false
             setLoading(false);
         });
        }

        //Add to cart
        function addToCart(product) {
            cartFactory.addItem(product);

            //Shows a toast notification
            showToastNotification('success', 'Item added to cart', "'" + product.desc + "'" + ' added to cart', true);

        }
      
        //Sets the controller to loading state
        function setLoading(loading) {
            vm.states.loading = loading;
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