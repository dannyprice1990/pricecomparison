(function () {
    'use strict';

    angular.module('app')
   .directive('drCategoriesSidebar', drCategoriesSidebar);

    function drCategoriesSidebar() {

        var directive = {
            restrict: 'EA',
            templateUrl: '/app/components/products/directives/drCategoriesSidebar.html',
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
                $scope.showNew = function (category) {
                    // If object is passed in - Edit
                    if (!!category) {
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

                // Save new category via parent controller
                $scope.saveNew = function (newCategory) {

                    switch ($scope.newFormState) {
                        case 'New':
                            $scope.vm.addProductCategory(newCategory);
                            break;
                        case 'Edit':
                            $scope.vm.updateProductCategory(newCategory);
                            break;
                        default:

                    }
                }
                // Delete via parent controller
                $scope.delete = function (category) {
                    $scope.vm.deleteproductCategory(category);
                }
                //  Save updated via parent controller
                $scope.saveUpdated = function (updatedCategory) {
                    $scope.vm.updateProductCategory(updatedCategory);
                }

                // Sets selected category via parent controller
                $scope.setSelectedCategory = function (selectedCategory) {
                    $scope.vm.setSelectedCategory(selectedCategory);
                }

            }
        };

        return directive;
    }

})();