(function () {
    "use strict";

    angular.module("app")
        .controller("numberSetNumbersController", numberSetNumbersController);
    //the body of the controller - where the code of the controller lives in this function
    function numberSetNumbersController($http, $routeParams, $location,$route) {
        var vm = this;

        vm.numberSetID = $routeParams.numberSetID;
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.currentNumberSet = {};
        vm.currentNumberSetNumbers = [];

        if (vm.numberSetID) {
            $http.get("/api/numberset/" + vm.numberSetID)
                .then(function (response) {
                    //success
                    angular.copy(response.data, vm.currentNumberSet);   
                    //angular.copy(vm.currentNumberSet.numberSetNumbers, vm.currentNumberSetNumbers);
                },
                function (error) {
                    //failure
                    vm.errorMessage = "Failed to load numberSet: " + + error.statusText + " " + error.status;
                }).finally(function () {
                    vm.isBusy = false;
                });
        }
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
           var result = vm.currentNumberSet.numberSetNumbers.filter(function (item) {
               return item.number === n;
           })[0];
           if (result) {
               //only add if there is not result at the moment
           }
           else
           {
               vm.currentNumberSet.numberSetNumbers.push({ 'number':n, 'numberSetID': vm.currentNumberSet.numberSetID, 'selectedNumber': false });
           }

       };

       vm.isNumberSelected = function (n) {
           if (vm.currentNumberSet.numberSetNumbers) {
               var result = vm.currentNumberSet.numberSetNumbers.filter(function (item) {
                   return item.number === n;
               })[0];
               if (result) {
                   return result.selectedNumber;
               }
           }
           return false;
       };

       vm.selectNumber = function (n) {
           if (vm.currentNumberSet.numberSetNumbers) {
               var result = vm.currentNumberSet.numberSetNumbers.filter(function (item) {
                   return item.number === n;
               })[0];
               if (result) {                   
                   result.selectedNumber = !result.selectedNumber;                  
               }
               else
               {
                   var selectedNumber = { 'number': n, 'numberSetID': vm.currentNumberSet.numberSetID, 'selectedNumber': true };
                   vm.currentNumberSet.numberSetNumbers.push(selectedNumber);
               }

           }
           vm.saveNumberSetNumbers(vm.currentNumberSetNumbers);
       };

       vm.saveNumberSetNumbers = function (numberSetNumbers) {
           vm.isBusy = true;
           vm.errorMessage = "";
           $http.post("/api/numbersetNumbers", vm.currentNumberSet.numberSetNumbers)
               .then(function (response) {
                   vm.currentNumberSet.numberSetNumbers = response.data;                   
               },
               function (error) {
                   vm.errorMessage = "Failed to save NumberSet numbers " + error.statusText + " " + error.status;
               })
               .finally(function () {
                   vm.isBusy = false;
               });
       };       
    }
})();
