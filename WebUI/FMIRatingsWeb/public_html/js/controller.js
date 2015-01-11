
var bootstrap = angular.module("Discipline-list", ['ui.bootstrap']);
debugger;
bootstrap.controller('AccordionCtrl', function ($scope) {
  $scope.oneAtATime = true;

  $scope.groups = [
    {
      title: 'Dynamic Group Header - 1',
      content: 'Dynamic Group Body - 1'
    },
    {
      title: 'Dynamic Group Header - 2',
      content: 'Dynamic Group Body - 2'
    }
  ];

  $scope.items = ['Item 1', 'Item 2', 'Item 3'];


  $scope.addItem = function() {
    var newItemNo = $scope.items.length + 1;
    $scope.items.push('Item ' + newItemNo);
  };

  $scope.status = {
    isFirstOpen: true,
    isFirstDisabled: false
  };
});

// fmiRatingsApp.controller('TeachersCtrl', ["$scope", "$http", function ($scope, $http) {            
//             $http({
//                     url: 'http://95.111.16.46:6420/api/teacherdepartments',
//                     method: "GET",
//                     xhrFields: {
//                         withCredentials: true
//                     }
                    
//                 }).success(function (data) {
//                     $scope.departments = data;

//                 }).error(function (data) {
//                     $scope.status = status;
//                 });

//             $scope.showTeacher = function (teacherId){
//                 $http({
//                     url: 'http://95.111.16.46:6420/api/teachers/' + teacherId,
//                     method: "GET",
//                     xhrFields: {
//                         withCredentials: true
//                     }
                    
//                 }).success(function (data) {
//                     $scope.dataTeacher = data;

//                 }).error(function (data) {
//                     $scope.statusTeacher = data;
//                 });
//             }

// }]);

//var fmiRatingsApp = angular.module('fmiRatingsApp', []);
//
//fmiRatingsApp.controller('CoursesCtrl', ["$scope", "$http", function ($scope, $http) {
//
//    $scope.text = "stamo";
//  
////    $http.get('http://95.111.16.46:6420/api/coursecategories').
////            success(function (data, status, headers, config) {
////                // this callback will be called asynchronously
////                // when the response is available
////            }).
////            error(function (data, status, headers, config) {
////                // called asynchronously if an error occurs
////                // or server returns response with an error status.
////            });
//            
//            $http({
//                    url: 'http://95.111.16.46:6420/api/coursecategories',
//                    method: "GET",
//                    xhrFields: {
//                        withCredentials: true
//                    }
//                    
//                }).success(function (data) {
//                    $scope.data = data;
//
//                }).error(function (data) {
//                    $scope.status = status;
//                });
//                
//                $scope.showCourse = function (courseId){
//                    debugger;
//                    $http({
//                    url: 'http://95.111.16.46:6420/api/courses/' + courseId,
//                    method: "GET",
//                    xhrFields: {
//                        withCredentials: true
//                    }
//                    
//                }).success(function (data) {
//                    debugger;
//                    $scope.dataCourse = data;
//
//                }).error(function (data) {
//                    debugger;
//                    $scope.statusCourse = data;
//                });
//                }
//
//}]);
//
