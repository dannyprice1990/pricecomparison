(function () {
    'use strict';

    angular.module('app')
   .directive('drProductPrices', drProductPrices);

    function drProductPrices() {

        var directive = {
            restrict: 'EA',
            templateUrl: '/app/components/products/directives/drProductPrices.html',
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
                $scope.showNew = function (productPrice) {
                    // If object is passed in - Edit
                    if (!!productPrice) {
                        //Object to edit
                        $scope.newFormObject = productPrice;
                        $scope.newFormState = "Edit"
                    }
                    else { // Else - New
                        $scope.newFormObject = undefined;
                        $scope.newFormState = "New"
                    }
                    $scope.newForm = !$scope.newForm;
                };

                // Save new category via parent controller
                $scope.saveNew = function (productPrice) {

                    switch ($scope.newFormState) {
                        case 'New':
                            $scope.vm.addProductPrice(productPrice);
                            break;
                        case 'Edit':
                            $scope.vm.updateProductPrice(productPrice);
                            break;
                        default:

                    }
                }
                // Delete via parent controller
                $scope.delete = function (productPrice) {
                    $scope.vm.deleteProductPrice(productPrice);
                }
                //  Save updated via parent controller
                $scope.saveUpdated = function (updatedProductPrice) {
                    $scope.vm.updateProductPrice(updatedProductPrice);
                }
            }
        };

        return directive;
    }

})();