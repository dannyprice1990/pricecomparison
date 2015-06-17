(function () {
    'use strict';

    angular.module('app')
   .directive('drNewWholesaler', drNewWholesaler);

    function drNewWholesaler() {

        var directive = {
            restrict: 'E',
            templateUrl: '/app/components/wholesalers/directives/drNewWholesaler.html',
            scope: {
                //Notify parent directive when saved
                notifyParentSaved: '&',
                //Notify parent directive when cancelled
                notifyParentCancelled: '&',
                //Notify parent directive to save
                notifySave: '&',
                //Wholesaler passed in (if any)
                wholesaler: '='
            },
            controller: function ($scope) {

                //New Wholesaler Object
                $scope.newWholesaler = {};

                //Submit form function
                $scope.submit = function (isValid) {

                    //Variable telling us form has been submitted
                    $scope.submitted = true;

                    //Check to make sure the form is completely valid
                    if (isValid) {

                        //Notify parent directive to save
                        $scope.notifySave({ newWholesaler: $scope.newWholesaler });

                        //Notify parent to say save went okay
                        $scope.notifyParentSaved();
                    }
                };
                

                //Initialise Form -------------------------------------------------

                //Edit or New Mode
                if (!!$scope.category) {
                    $scope.formTitle = 'Edit Wholesaler - ' + $scope.wholesaler.name;

                    //Fills in existing details for edit
                    $scope.newWholesaler = $scope.wholesaler
                }
                else {
                    $scope.formTitle = 'New Wholesaler';

                }
                //-------------------------------------------------------------------

            }
        };

        return directive;
    }

})();