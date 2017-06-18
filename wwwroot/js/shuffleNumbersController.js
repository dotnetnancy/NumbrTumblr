(function () {
    "use strict";

    angular.module("app")
        .controller("shuffleNumbersController", shuffleNumbersController);
    //the body of the controller - where the code of the controller lives in this function
    function shuffleNumbersController($http, $routeParams, $location, $route) {
        var vm = this;

        vm.numberSetID = $routeParams.numberSetID;
        vm.shuffleIncludeType = $routeParams.shuffleIncludeType;
        vm.shuffleID = $routeParams.shuffleID;

        vm.errorMessage = "";
        vm.isBusy = true;
        vm.currentShuffle = {};
        vm.currentShuffleNumbers = [];
        vm.shuffleDescription = "";

        if (vm.numberSetID && vm.shuffleIncludeType) {
            if (vm.shuffleIncludeType !== "None") {
                //vm.currenShuffle.numberSetID = vm.numberSetID;
                $http.get("/api/shuffle/" + vm.numberSetID + "/" + vm.shuffleIncludeType)
                    .then(function (response) {
                        //success                        
                        angular.copy(response.data, vm.currentShuffleNumbers);

                        vm.currentShuffleNumbers.forEach(function (element) {
                            element.selectedNumber = false;
                        });
                    },
                    function (error) {
                        //failure
                        vm.errorMessage = "Failed to load shuffle: " + + error.statusText + " " + error.status;
                    }).finally(function () {
                        vm.isBusy = false;
                        vm.editMode = false;
                    });
            }
            else {
                $http.get("/api/shuffle/GetShuffle/" + vm.shuffleID)
                    .then(function (response) {
                        //success                        
                        angular.copy(response.data, vm.currentShuffle);
                        angular.copy(vm.currentShuffle.shuffleNumbers, vm.currentShuffleNumbers);
                    },
                    function (error) {
                        //failure
                        vm.errorMessage = "Failed to load shuffle: " + + error.statusText + " " + error.status;
                    }).finally(function () {
                        vm.isBusy = false;
                        vm.editMode = true;
                    });

            }
           
        }
        vm.dateOf = function (utcDateStr) {
            return new Date(utcDateStr);
        };

        vm.isShuffleNumberSelected = function (n) {
            if (vm.currentShuffleNumbers) {
                var result = vm.currentShuffleNumbers.filter(function (item) {
                    return item.number === n;
                })[0];
                if (result) {
                    return result.selectedNumber;
                }
            }
            return false;
        };

        vm.selectShuffleNumber = function (n) {
            if (vm.currentShuffleNumbers) {
                var result = vm.currentShuffleNumbers.filter(function (item) {
                    return item.number === n;
                })[0];
                if (result) {
                    result.selectedNumber = !result.selectedNumber;
                }
            }
        };

        //after they have made their picks then they can save those selections and the shuffle itself
        vm.saveShuffleNumbers = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/shuffleNumbers/" + vm.numberSetID + '/' + vm.shuffleDescription, vm.currentShuffleNumbers)
                .then(function (response) {
                    vm.currentShuffleNumbers = response.data;
                    window.location.href = '#!/shuffles/' + vm.numberSetID;
                },
                function (error) {
                    vm.errorMessage = "Failed to save Shuffle numbers " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.editShuffle = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            vm.currentShuffle.shuffleNumbers = vm.currentShuffleNumbers;
            $http.put("/api/shuffle/edit", vm.currentShuffle)
                .then(function (response) {
                    vm.currentShuffleNumbers = response.data;
                    window.location.href = '#!/shuffles/' + vm.numberSetID;
                },
                function (error) {
                    vm.errorMessage = "Failed to save Shuffle edits " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }
})();
