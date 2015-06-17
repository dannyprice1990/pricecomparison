(function () {
    'use strict';

    angular.module('app')
   .directive('drNewCategory', drNewCategory);

    function drNewCategory() {

        var directive = {
            restrict: 'E',
            templateUrl: '/app/components/products/directives/drNewCategory.html',
            scope: {
                //Notify parent directive when saved
                notifyParentSaved: '&',
                //Notify parent directive when cancelled
                notifyParentCancelled: '&',
                //Notify parent directive to save
                notifySave: '&',
                //Category passed in (if any)
                category: '='
            },
            controller: function ($scope) {

                //New Category Object
                $scope.newCategory = {};

                //Submit form function
                $scope.submit = function (isValid) {

                    //Variable telling us form has been submitted
                    $scope.submitted = true;

                    //Check to make sure the form is completely valid
                    if (isValid) {

                        //Notify parent directive to save the book
                        $scope.notifySave({ newCategory: $scope.newCategory });

                        //Notify parent to say save went okay
                        $scope.notifyParentSaved();
                    }
                };
                

                //Initialise Form -------------------------------------------------

                //Edit or New Mode
                if (!!$scope.category) {
                    $scope.formTitle = 'Edit Category - ' + $scope.category.name;

                    //Fills in existing details for edit
                    $scope.newCategory = $scope.category
                }
                else {
                    $scope.formTitle = 'New Category';

                }
                //-------------------------------------------------------------------

            }
        };

        return directive;
    }

})();