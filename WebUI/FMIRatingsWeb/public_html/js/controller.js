
var fmiRatingsApp = angular.module('fmiRatingsApp', []);

fmiRatingsApp.controller('CoursesCtrl', ["$scope", "$http", function ($scope, $http) {            
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

fmiRatingsApp.controller('TeachersCtrl', ["$scope", "$http", function ($scope, $http) {            
            $http({
                    url: 'http://95.111.16.46:6420/api/teacherdepartments',
                    method: "GET",
                    xhrFields: {
                        withCredentials: true
                    }
                    
                }).success(function (data) {
                    $scope.departments = data;

                }).error(function (data) {
                    $scope.status = status;
                });

}]);

