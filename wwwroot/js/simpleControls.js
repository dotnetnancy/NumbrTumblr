(function () {
    "use strict";
    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor)      

    function waitCursor() {
        return {
            scope: {
                showhideflag: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }

})();