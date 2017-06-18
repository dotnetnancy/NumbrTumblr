(function () {
    "use strict";

    angular.module("app")
        .controller("numberSetController", numberSetController);
    //the body of the controller - where the code of the controller lives in this function
    function numberSetController($http, $location, $route) {
        var vm = this;
        vm.minNumberSetValue = 0;
        vm.maxNumberSetValue = 500;

        vm.numberSets = [];
        vm.newNumberSet = {};
        vm.numberSetNumbers = [];
        vm.isBusy = true;
        $http.get("/api/numberset")
            .then(function (response) {
                //success
                angular.copy(response.data, vm.numberSets);
            },
            function (error) {
                //failure
                vm.errorMessage = "Failed to load data: " + + error.statusText + " " + error.status;
            }).finally(function () {
                vm.isBusy = false;
            });


        vm.errorMessage = "";

        vm.initCardTable = function () {
            $('#card-table-numberSets').cardtable({ id: 'small-card-table-numberSets', myClass: 'stacktable small-only' });
            //$('.small-only').hide();
        };

        vm.AddNumberSet = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            vm.range(vm.newNumberSet.numberSetMin, vm.newNumberSet.numberSetMax);

            vm.newNumberSet.numberSetNumbers = vm.numberSetNumbers;
            $http.post("/api/numberset", vm.newNumberSet)
                .then(function (response) {
                    vm.numberSets.push(response.data);
                    vm.newNumberSet = {};
                },
                function (error) {
                    vm.errorMessage = "Failed to save new NumberSet " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
            window.location.href = '/';
        };

        vm.range = function (min, max, step) {
            step = step || 1;
            var input = [];

            for (var i = min; i <= max; i += step) {
                input.push(i);
                vm.pushNumberIfNotPresent(i);
            }
            return input;
        };

        vm.pushNumberIfNotPresent = function (n) {
            var result = vm.numberSetNumbers.filter(function (item) {
                return item.number === n;
            })[0];
            if (result) {
                //only add if there is not result at the moment
            }
            else {
                vm.numberSetNumbers.push({ 'number': n, 'selectedNumber': false });
            }

        };
    }
})();