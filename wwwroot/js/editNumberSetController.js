(function () {
    "use strict";

    angular.module("app")
        .controller("editNumberSetController", editNumberSetController);
    //the body of the controller - where the code of the controller lives in this function
    function editNumberSetController($http, $routeParams, $location) {
        var vm = this;
        vm.numberSetID = $routeParams.numberSetID;
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.currentNumberSet = {};

        if (vm.numberSetID) {
            $http.get("/api/numberset/" + vm.numberSetID)
                .then(function (response) {
                    //success
                    angular.copy(response.data, vm.currentNumberSet);
                },
                function (error) {
                    //failure
                    vm.errorMessage = "Failed to load data: " + + error.statusText + " " + error.status;
                }).finally(function () {
                    vm.isBusy = false;
                });
        }

        vm.EditNumberSet = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.put("/api/numberset/edit", vm.currentNumberSet)
                .then(function (response) {
                    angular.copy(response.data, vm.currentNumberSet);
                },
                function (error) {
                    vm.errorMessage = "Failed to edit NumberSet " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                    vm.currentNumberSet = {};
                }); 
            window.location.href = '/';
        };

        vm.deleteNumberSet = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.delete("/api/numberset/delete/" + vm.currentNumberSet.numberSetID)
                .then(function (response) { 
                    
                },
                function (error) {
                    vm.errorMessage = "Failed to delete NumberSet " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                    vm.currentNumberSet = {};
                });
            window.location.href = '/';
        };
    }
})();