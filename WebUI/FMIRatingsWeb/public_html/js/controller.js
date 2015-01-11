
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
                
                $scope.showCourse = function (courseId){
                    debugger;
                    $http({
                    url: 'http://95.111.16.46:6420/api/courses/' + courseId,
                    method: "GET",
                    xhrFields: {
                        withCredentials: true
                    }
                    
                }).success(function (data) {
                    debugger;
                    $scope.dataCourse = data;

                }).error(function (data) {
                    debugger;
                    $scope.statusCourse = data;
                });
                }

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

            $scope.showTeacher = function (teacherId){
                $http({
                    url: 'http://95.111.16.46:6420/api/teachers/' + teacherId,
                    method: "GET",
                    xhrFields: {
                        withCredentials: true
                    }
                    
                }).success(function (data) {
                    $scope.dataTeacher = data;

                }).error(function (data) {
                    $scope.statusTeacher = data;
                });
            }

}]);

