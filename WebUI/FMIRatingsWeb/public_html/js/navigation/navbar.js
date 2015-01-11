/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

//page names constant
var START_PAGE = "start-page";
var EXIT = "nonregistered-user-homepage";
var DISCIPLINES_LIST = "disciplines-list-page";

//$(document).ready(function () {

var app = angular.module("fmiRatingsApp", ['ngRoute']);
app.config(function ($routeProvider) {

    $routeProvider
            // route for the home page
            .when('/start-page', {
                templateUrl: 'navbar-html/start-page.html',
                controller: 'mainController'
            })

            // route for the discipline page
            .when('/disciplines-list-page', {
                templateUrl: 'navbar-html/disciplines-list-page.html',
                controller: 'disciplinesController'
            })

            // route for the teacher page
            .when('/teachers-list-page', {
                templateUrl: 'navbar-html/teachers.html',
                controller: 'teachersController'
            })

            .when('/discipline-details/:id', {
                templateUrl: 'navbar-html/discipline-details/discipline-details.html',
                controller: 'disciplineDetailsController'
            });
});

app.controller('mainController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
});

app.controller('disciplinesController', function ($scope, $http) {
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

    $scope.message = 'Look! I am an about page.';
});

app.controller('disciplineDetailsController', function ($rootScope, $scope, $routeParams, $route, $http) {
    //If you want to use URL attributes before the website is loaded
     $http({
                    url: 'http://95.111.16.46:6420/api/courses/' + $routeParams.id,
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
});

app.controller('teachersController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});
//
//app.controller("MainCtrl",["$scope", "$location", function($scope, $location) {
//  $scope.menuClass = function(page) {
//    
//    var current = $location.path(page);
//    var actualHrefText = current.path().substring(1);
////
////        console.log(actualHrefText);
////        var page = "navbar-html/" + actualHrefText + ".html";
//
//    return page === actualHrefText ? "active" : "";
//  };
//}]);

//    $("#body-wrapper").load("navbar-html/start-page.html #start-page", function () {
//        loadStartPage();
//    });
//
////        START PAGE LOADING
//    function loadStartPage() {
//        $("#start-page").html(function () {
//            console.log("load start page");
//            $("#start-page-carousel-div").load("navbar-html/carousel-component.html #carousel-generic");
//            $("#start-page-info-div").load("navbar-html/start-page-detailed-statistics.html");
//        });
//    }

//        NAVBAR PAGE LOADING
//    $('.nav a').click(function () {
//
//        $(this).parent().parent().find("li").removeClass("active");
//        $(this).parent().addClass("active");
//
//        var hrefText = $(this).attr('href');
//        var actualHrefText = hrefText.replace("#", "");
//
//        console.log(actualHrefText);
//        var page = "navbar-html/" + actualHrefText + ".html";
//
//        $("#body-wrapper").load(page, function () {
//            if (actualHrefText === START_PAGE) {
//                loadStartPage();
//            }
//            if (actualHrefText === EXIT){
//                localStorage.clear();
//            }
//        });
//    });
//});
