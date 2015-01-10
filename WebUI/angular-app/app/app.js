'use strict';

// Declare app level module which depends on views, and components
var myapp = angular.module('fmiRatingsApp', []);

myapp.controller('CoursesController', ['$scope', function($scope) {
	$scope.greet = "Greet";
//function CoursesController($scope) {
    $scope.courses = [
	  {
	    "id": 1,
	    "name": "sample string 2",
	    "description": "sample string 3",
	    "teachers": [
	      "sample string 1",
	      "sample string 2"
	    ]
	  },
	  {
	    "id": 1,
	    "name": "sample string 2",
	    "description": "sample string 3",
	    "teachers": [
	      "sample string 1",
	      "sample string 2"
	    ]
	  }
	];
}]);
