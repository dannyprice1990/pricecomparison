(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductCategoriesCtrl', ProductCategoriesCtrl);

    //Injects 
    ProductCategoriesCtrl.$inject = ['productsFactory', 'authFactory'];

    function ProductCategoriesCtrl(productsFactory, authFactory) {
        var vm = this;
        vm.addProductCategory = addProductCategory;
        vm.productCategories = productsFactory.productCategories;
        vm.pagination = {};
        vm.states = {
            loading: false
        }

        //Current Authentication Information
        vm.authentication = authFactory.authentication;

        ////////////////////

        //If product categories are yet to be loaded
        var loaded = productsFactory.getProductCategoriesLoaded();
        if (loaded == false) {
            getProductCategories();
        }

        //Gets a list of product categories
        function getProductCategories() {
            //Sets loading to true
            setLoading(true);

            productsFactory.getProductCategories()
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

        //Calls the deleteProductCategory function within our Products Service
        function deleteProductCategory(category) {

            //Sets loading to true
            setLoading(true);

            //Calls delete on service
            productsFactory.deleteProductCategory(category.id)
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

        //Calls the addProductCategory function within our Products Service
        function addProductCategory(category) {

            //Sets loading to true
            setLoading(true);

            productsFactory.addProductCategory(category)
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

        //Calls the updateProducCategory function within our Products Service
        function updateProductCategory(category) {

            //Sets loading to true
            setLoading(true);

            productsFactory.updateProductCategory(category)
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

    }

})();