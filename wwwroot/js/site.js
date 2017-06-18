(function () {
    "use strict";
    angular.module("app", ["simpleControls", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider
                .when('/', {
                    controller: "numberSetController",
                    controllerAs: "vm",
                    templateUrl: "/views/numberSetsView.html"
                });
            $routeProvider
                .when('/shuffles/:numberSetID', {
                    controller: "shufflesController",
                    controllerAs: "vm",
                    templateUrl: "/views/shufflesView.html"
                });
            $routeProvider
                .when('/shuffleNumbers/:numberSetID/:shuffleID/:shuffleIncludeType', {
                    controller: "shuffleNumbersController",
                    controllerAs: "vm",
                    templateUrl: "/views/shuffleNumbersView.html"
                });
            $routeProvider
                .when('/shuffleNumbers/:numberSetID/:shuffleIncludeType', {
                    controller: "shuffleNumbersController",
                    controllerAs: "vm",
                    templateUrl: "/views/shuffleNumbersView.html"
                });
            $routeProvider
                .when('/deleteShuffle/:shuffleID/:numberSetID', {
                    controller: "shufflesController",
                    controllerAs: "vm",
                    templateUrl: "/views/deleteShuffleView.html"
                }); 
            $routeProvider
                .when('/addNumberSet', {
                    controller: "numberSetController",
                    controllerAs: "vm",
                    templateUrl: "/views/addNumberSetView.html"
                });
            $routeProvider
                .when('/editNumberSet/:numberSetID', {
                    controller: "editNumberSetController",
                    controllerAs: "vm",
                    templateUrl: "/views/editNumberSetView.html"
                }); 

            $routeProvider
                .when('/deleteNumberSet/:numberSetID', {
                    controller: "editNumberSetController",
                    controllerAs: "vm",
                    templateUrl: "/views/deleteNumberSet.html"
                }); 

            $routeProvider
                .when('/numberSetNumbers/:numberSetID', {
                    controller: "numberSetNumbersController",
                    controllerAs: "vm",
                    templateUrl: "/views/numberSetNumbersView.html"
                });
            
            
                $routeProvider.otherwise({
                    redirectTo: '/'
                });
        });   
})();