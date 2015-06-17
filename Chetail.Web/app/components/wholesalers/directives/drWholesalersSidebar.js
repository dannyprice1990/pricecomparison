(function () {
    'use strict';

    angular.module('app')
   .directive('drWholesalersSidebar', drWholesalersSidebar);

    function drWholesalersSidebar() {

        var directive = {
            restrict: 'EA',
            templateUrl: '/app/components/wholesalers/directives/drWholesalersSidebar.html',
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
                $scope.showNew = function (wholesaler) {
                    // If object is passed in - Edit
                    if (!!wholesaler) {
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

                // Save new wholesaler via parent controller
                $scope.saveNew = function (newWholesaler) {

                    switch ($scope.newFormState) {
                        case 'New':
                            $scope.vm.addWholesaler(newWholesaler);
                            break;
                        case 'Edit':
                            $scope.vm.updateWholesaler(newWholesaler);
                            break;
                        default:

                    }
                }
                // Delete via parent controller
                $scope.delete = function (wholesaler) {
                    $scope.vm.deleteWholesaler(wholesaler);
                }
                //  Save updated via parent controller
                $scope.saveUpdated = function (updatedWholesaler) {
                    $scope.vm.updateProductCategory(updatedWholesaler);
                }

            }
        };

        return directive;
    }

})();