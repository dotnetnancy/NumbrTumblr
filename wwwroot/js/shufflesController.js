(function () {
    "use strict";

    angular.module("app")
        .controller("shufflesController", shufflesController);
    //the body of the controller - where the code of the controller lives in this function
    function shufflesController($http, $location, $route,$routeParams) {

        var vm = this;
        vm.errorMessage = "";
        vm.numberSetID = $routeParams.numberSetID;
        vm.shuffleID = $routeParams.shuffleID;
        vm.shuffles = [];
        vm.newShuffle = {};
        vm.isBusy = true;
       

        $http.get("/api/shuffle/" + vm.numberSetID)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.shuffles);
            },
            function (error) {
                //failure
                vm.errorMessage = "Failed to load data: " + + error.statusText + " " + error.status;
            }).finally(function () {
                vm.isBusy = false;
            });


       

        vm.initCardTable = function () {
            $('#card-table-shuffles').cardtable({ id: 'small-card-table-shuffles', myClass: 'stacktable small-only' });
            //$('.small-only').hide();
        };

        vm.AddShuffle = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("/api/shuffle", vm.newShuffle)
                .then(function (response) {
                    vm.shuffles.push(response.data);
                    vm.newShuffle = {};
                },
                function (error) {
                    vm.errorMessage = "Failed to save new Shuffle " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
            window.location.href = '/';
        };

        vm.deleteShuffle = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.delete("/api/shuffle/delete/" + vm.shuffleID)
                .then(function (response) {

                },
                function (error) {
                    vm.errorMessage = "Failed to delete Shuffle " + error.statusText + " " + error.status;
                })
                .finally(function () {
                    vm.isBusy = false;
                    vm.newShuffle = {};
                });
            window.location.href = '/shuffles/' + vm.numberSetID;           
        };
    }
})();