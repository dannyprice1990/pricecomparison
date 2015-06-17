(function () {
    'use strict';

    var app = angular
       .module('app', ['ngRoute', 'LocalStorageModule', 'toaster', 'ui.bootstrap'])
       .constant('apiUrl', 'http://chetailapi.azurewebsites.net/') //Stores a constant value, we inject this into the controllers / services that need it 
        //http://localhost:50432/
        //http://chetailapi.azurewebsites.net/
       .config(function ($routeProvider, $httpProvider) {

           //Intercept HTTP calls for authentication (response only)
           //$httpProvider.interceptors.push('authInterceptorFactory');

           //Routing ------------------------------

           //Home
           $routeProvider.when("/home", {
               templateUrl: "/app/components/home/views/home.html"
           });
           //Product Detail
           $routeProvider.when("/productdetail", {
               templateUrl: "/app/components/products/views/productdetail.html"
           });
           //Cart
           $routeProvider.when("/cart", {
               templateUrl: "/app/components/cart/views/cart.html"
           });
           //Results
           $routeProvider.when("/results", {
               templateUrl: "/app/components/results/views/results.html"
           });
           //Login
           $routeProvider.when("/login", {
               templateUrl: "/app/components/authentication/views/login.html"
           });

           $routeProvider.otherwise({ redirectTo: "/home" });
       });

    //app.run(['authFactory', function (authFactory) {
    //    authFactory.fillAuthData();
    //}]);

})();