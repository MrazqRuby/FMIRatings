var fmiRatingsApp = angular.module('fmiRatingsApp', []);

fmiRatingsApp.controller('CourcesCtrl', function ($scope) {
  $scope.cources = [[
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
});