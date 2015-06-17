(function () {
    'use strict';

    angular.module('app')
   .directive('drNewProductPrice', drNewProductPrice);

    function drNewProductPrice() {

        var directive = {
            restrict: 'E',
            templateUrl: '/app/components/products/directives/drNewProductPrice.html',
            scope: {
                //Notify parent directive when saved
                notifyParentSaved: '&',
                //Notify parent directive when cancelled
                notifyParentCancelled: '&',
                //Notify parent directive to save
                notifySave: '&',
                //ProductPrice passed in (if any)
                productPrice: '=',
                //Wholesalers passed in
                wholesalers: '='
            },
            controller: function ($scope) {

                //New Product Price Object
                $scope.newProductPrice = {};

                //Sets selected index of Product Price
                $scope.selectedWholesaler = $scope.wholesalers[0];

                //Submit form function
                $scope.submit = function (isValid) {

                    //Variable telling us form has been submitted
                    $scope.submitted = true;

                    //Check to make sure the form is completely valid
                    if (isValid) {

                        //Stores wholesaler id
                        $scope.newProductPrice.wholesalerId = $scope.selectedWholesaler.id;

                        //Notify parent directive to save the book
                        $scope.notifySave({ newProductPrice: $scope.newProductPrice });

                        //Notify parent to say save went okay
                        $scope.notifyParentSaved();
                    }
                };

                //Initialise Form -------------------------------------------------

                //Edit or New Mode
                if (!!$scope.category) {
                    $scope.formTitle = 'Edit Product Price - ' + $scope.productPrice.name;

                    //Fills in existing details for edit
                    $scope.newCategory = $scope.productPrice
                }
                else {
                    $scope.formTitle = 'New Product Price';

                }
                //-------------------------------------------------------------------
            }
        };

        return directive;
    }

})();