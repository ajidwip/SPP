var app = angular.module('myApp', ['datatables'])
    .directive('myPostRepeatDirective', function () {
        return function (scope, element, attrs) {
            if (scope.$last) {
                $('#loading').hide();
            }
        };
    });