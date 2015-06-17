(function () {
    'use strict';

    angular.module('app')
   .factory('productsFactory', productsFactory);

    //Injects $http, $q
    productsFactory.$inject = ['httpChetail', 'apiUrl', '$q'];

    function productsFactory(httpChetail, apiUrl, $q) {

        // API Resource
        var apiProductsResource = apiUrl + 'api/products';
        var apiProductCategoriesResource = apiUrl + 'api/productcategories';
        var apiProductPricesResource = apiUrl + 'api/products';

        // Default Page Sizes
        var defaultProductsPageSize = 50;

        // Stores Objects
        // -- List of product categories
        var _productCategories = [];
        // -- bool for whether product categories have been loaded
        var _productCategoriesLoaded = false;
        // -- Selected Product Category
        var _selectedProductCategory = {};

        // Public Interface
        var dataFactory = {
            // Product Categories
            productCategories:_productCategories,
            getProductCategories: _getProductCategories,
            getProductCategory: _getProductCategory,
            addProductCategory: _addProductCategory,
            updateProductCategory: _updateProductCategory,
            deleteProductCategory: _deleteProductCategory,
            getProductCategoriesLoaded: _getProductCategoriesLoaded,
            selectedProductCategory:_selectedProductCategory,

            // Products
            getProducts: _getProducts,
            getProduct: _getProduct,
            addProduct: _addProduct,

            //Product Prices
            getProductPrices: _getProductPrices,
            addProductPrice: _addProductPrice,
            deleteProductPrice: _deleteProductPrice
        };
        return dataFactory;

        ////////////

        //Loads in Products
        function _getProducts(page, productCategoryId) {

            //Promise to return
            var deferred = $q.defer();

            //Defaults
            var _pageSize = defaultProductsPageSize;
            var _page = 0;

            //Has a specific page been passed in?
            if (!!page) {
                _page = page;
            }

            //Builds API Call
            var _apiCall = apiProductsResource + "?page=" + _page + "&pageSize=" +
                                                _pageSize 

            //If product category = -1 then return No Products
            //If product category = 0 then return All Products
            if (productCategoryId != -1) {

                //Filter by Product Category ID
                if (productCategoryId != 0) {
                    _apiCall = _apiCall + "&productCategoryId=" + productCategoryId;
                };

                //Get products
                httpChetail.get(_apiCall)
                .then(function (result) {
                    //Successful
                    deferred.resolve(result);
                },
                  function () {
                      //Error getting products
                      deferred.reject();
                  });

            }
            else {
                //Invalid Search
                deferred.reject();
            }

            return deferred.promise;
        }

        //Loads in a specific Product
        function _getProduct(id) {

            var _apiCall = apiProductsResource + '/' + id;
            return httpChetail.get(_apiCall);
        }

        // Adds new Product Category
        function _addProduct(newProduct) {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiProductsResource;

            httpChetail.post(_apiCall, newProduct)
            .then(function (result) {
                //Successful
                deferred.resolve(result.data);
            },
            function () {
                //Error
                deferred.reject();
            });
            return deferred.promise;
        };

        //Loads in a Product Prices
        function _getProductPrices(productId) {

            var _apiCall = apiProductPricesResource + '/' + productId + '/productPrices';
            return httpChetail.get(_apiCall);
        }

        // Adds new Product Price
        function _addProductPrice(productId, newProductPrice) {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiProductPricesResource + '/' + productId + '/productPrices';

            httpChetail.post(_apiCall, newProductPrice)
            .then(function (result) {
                //Successful
                deferred.resolve(result.data);
            },
            function () {
                //Error
                deferred.reject();
            });
            return deferred.promise;
        };
        
        // Deletes Product Price
        function _deleteProductPrice(productId, id) {
            return httpChetail.delete(apiProductPricesResource + '/' + productId + '/productPrices/' + id);
        };

        // Loads in Product Categories
        function _getProductCategories() {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiProductCategoriesResource;

            httpChetail.get(_apiCall)
            .then(function (result) {
                //Successful
                angular.copy(result.data, _productCategories);
                _productCategoriesLoaded = true;
                deferred.resolve();
            },
            function () {
                //Error
                deferred.reject();
            });

            return deferred.promise;

        }

        // Loads in a specific Product Category
        function _getProductCategory(id) {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiProductCategoriesResource + '/' + id;

            if (id == 0) {
               // Zero was passed in, return nothing
                deferred.resolve();
            }
            else {
                httpChetail.get(_apiCall)
               .then(function (result) {
                   //Successful
                   deferred.resolve(result);
               },
          function () {
              //Error, invalid category was passed in
              deferred.reject();
          });
            }     

            return deferred.promise;
        }

        // Adds new Product Category
        function _addProductCategory(newCategory) {

            //Promise to return
            var deferred = $q.defer();
            var _apiCall = apiProductCategoriesResource;

            httpChetail.post(apiProductCategoriesResource, newCategory)
            .then(function (result) {
                //Successful
                _productCategories.splice(0, 0, result.data);
                deferred.resolve(result.data);
            },
            function () {
                //Error
                deferred.reject();
            });
            return deferred.promise;
        };

        // Updates existing Product Category
        function _updateProductCategory(updatedCategory) {
            return httpChetail.put(apiProductCategoriesResource + '/' + updatedBook.id, updatedBook)
        };

        // Deletes Product Category
        function _deleteProductCategory(id) {
            return httpChetail.delete(apiProductCategoriesResource + '/' + id);
        };

        // Gets bool stating whether product categories are loaded or not
        function _getProductCategoriesLoaded() {
            return _productCategoriesLoaded;
        };

    }

})();