(function () {
    'use strict';

    angular.module('app')
   .directive('drNewProduct', drNewProduct);

    function drNewProduct() {

        var directive = {
            restrict: 'E',
            templateUrl: '/app/components/products/directives/drNewProduct.html',
            scope: {
                //Notify parent directive when saved
                notifyParentSaved: '&',
                //Notify parent directive when cancelled
                notifyParentCancelled: '&',
                //Notify parent directive to save
                notifySave: '&',
                //Product passed in (if any)
                product: '=',
                //Product categories passed in
                productCategories: '='
            },
            controller: function ($scope) {

                //New Product Object
                $scope.newProduct = {};

                //Sets selected index of Categories
                $scope.selectedCategory = $scope.productCategories[0];

                //Submit form function
                $scope.submit = function (isValid) {

                    //Variable telling us form has been submitted
                    $scope.submitted = true;

                    //Check to make sure the form is completely valid
                    if (isValid) {

                        //Stores product category id 'new product'
                        $scope.newProduct.productCategoryId = $scope.selectedCategory.id;

                        //Notify parent directive to save the object
                        $scope.notifySave({ newProduct: $scope.newProduct });

                        //Notify parent to say save went okay
                        $scope.notifyParentSaved();
                    }
                };

                //Initialise Form -------------------------------------------------

                //Edit or New Mode
                if (!!$scope.category) {
                    $scope.formTitle = 'Edit Product - ' + $scope.product.code;

                    //Fills in existing details for edit
                    $scope.newCategory = $scope.product
                }
                else {
                    $scope.formTitle = 'New Product';

                }
                //-------------------------------------------------------------------
            }
        };

        return directive;
    }

})();