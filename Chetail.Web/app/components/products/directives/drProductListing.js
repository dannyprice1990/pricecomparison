(function () {
    'use strict';

    angular.module('app')
   .directive('drProductListing', drProductListing);

    function drProductListing() {

        var directive = {
            restrict: 'EA',
            templateUrl: '/app/components/products/directives/drProductListing.html',
            scope: true, // This directive inherits directly from it's parent controller
            controller: function ($scope) {
                // Specific to this Directives scope

                // Boolean - Show Form
                $scope.newForm = false;
                // State - Form, New or Edit
                $scope.newFormState = '';
                // Object - object passed into form for editing purposes
                $scope.newFormObject = {};

                // Show/Hide Form
                $scope.showNew = function (product) {
                    // If object is passed in - Edit
                    if (!!product) {
                        //Object to edit
                        $scope.newFormObject = category;
                        $scope.newFormState = "Edit"
                    }
                    else { // Else - New
                        $scope.newFormObject = undefined;
                        $scope.newFormState = "New"
                    }
                    $scope.newForm = !$scope.newForm;
                };

                // Save new product via parent controller
                $scope.saveNew = function (newProduct) {

                    switch ($scope.newFormState) {
                        case 'New':
                            $scope.vm.addProduct(newProduct);
                            break;
                        case 'Edit':
                            $scope.vm.updateProduct(newProduct);
                            break;
                        default:

                    }
                }
                // Delete via parent controller
                $scope.delete = function (product) {
                    $scope.vm.deleteProduct(product);
                }
                //  Save updated via parent controller
                $scope.saveUpdated = function (updatedProduct) {
                    $scope.vm.updateProduct(updatedProduct);
                }

            }
        };

        return directive;
    }

})();