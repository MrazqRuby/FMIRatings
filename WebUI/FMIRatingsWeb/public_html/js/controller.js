
var fmiRatingsApp = angular.module('fmiRatingsApp', []);

fmiRatingsApp.controller('CoursesCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.text = "stamo";
    debugger;
//    $http.get('http://95.111.16.46:6420/api/coursecategories').
//            success(function (data, status, headers, config) {
//                // this callback will be called asynchronously
//                // when the response is available
//            }).
//            error(function (data, status, headers, config) {
//                // called asynchronously if an error occurs
//                // or server returns response with an error status.
//            });
            
            $http({
                    url: 'http://95.111.16.46:6420/api/coursecategories',
                    method: "GET",
                    xhrFields: {
                        withCredentials: true
                    }
                    
                }).success(function (data) {
                    $scope.data = data;

                }).error(function (data) {
                    $scope.status = status;
                });

}]);

