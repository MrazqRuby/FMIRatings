var fmiApp = angular.module('fmiApp', ['ngRoute']).
config(function ($routeProvider, $locationProvider, $httpProvider) {
  // function($routeProvider, $locationProvider) {
    $routeProvider.when('/home-page.html#/home',
    {
      templateUrl:    'home.html',
      controller:     'HomeCtrl'
    });
    $routeProvider.when('/about',
    {
      templateUrl:'disciplines-list-page.html',
      controller: 'AboutCtrl'
    });
    $routeProvider.when('/contact',
    {
      templateUrl:'contact.html',
      controller:'ContactCtrl'
    });
    $routeProvider.otherwise(
    {
      redirectTo:'/home',
      controller:     'HomeCtrl', 
    }
  );
});

fmiApp.controller('NavCtrl', 
['$scope', '$location', function ($scope, $location) {
  $scope.navClass = function (page) {
    var currentRoute = $location.path().substring(1) || 'home';
    return page === currentRoute ? 'active' : '';
  };
  
  $scope.loadHome = function () {
        $location.url('/home');
    };
    
      $scope.loadAbout = function () {
      	console.log("Going to about");
        $location.url('/about');
        console.log($location.path());
    };
    
      $scope.loadContact = function () {
        $location.url('/contact');
    };
    
}]);

fmiApp.controller('CoursesCtrl', function ($scope) {
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
});

fmiApp.service(
  "apiService",
  function( $http, $q ) {
	  // Return public API.
	  return({
	    getCourses: getCourses,
	    getTeachers: getTeachers
	  });
	  // ---
	  // PUBLIC METHODS.
	  // ---
	  function getCourses() {
	    var request = $http({
	      method: "get",
	      url: "api/courses"
	    });

	    return( request.then( handleSuccess, handleError ) );
  }
  
  function getTeachers() {
    var request = $http({
      method: "get",
      url: "api/teachers"
    });
    return( request.then( handleSuccess, handleError ) );
  }

  function handleError( response ) {
    // The API response from the server should be returned in a
    // nomralized format. However, if the request was not handled by the
    // server (or what not handles properly - ex. server error), then we
    // may have to normalize it on our end, as best we can.
    if (!angular.isObject( response.data ) || !response.data.message) {
      return( $q.reject( "An unknown error occurred." ) );
    }
    // Otherwise, use expected error message.
    return( $q.reject( response.data.message ) );
  }

  function handleSuccess( response ) {
    return( response.data );
  }
});