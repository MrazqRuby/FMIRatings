/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

//page names constant
var START_PAGE = "start-page";
var EXIT = "nonregistered-user-homepage";
var DISCIPLINES_LIST = "disciplines-list-page";
var TEACHERS_LIST = "teachers-list-page";


//$(document).ready(function () {

var app = angular.module("fmiRatingsApp", ['ngRoute']);

app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common['Authorization'] = localStorage.getItem("authentication");
    }]);
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
                templateUrl: 'navbar-html/teachers-list-page.html',
                controller: 'teachersController'
            })

            .when('/exit', {
                templateUrl: 'nonregistered-user-homepage.html',
                controller: 'exitController'
            })

            .when('/discipline-details/:id', {
                templateUrl: 'navbar-html/discipline-details/discipline-details.html',
                controller: 'disciplineDetailsController'
            })

            .when('/teacher-details/:id', {
                templateUrl: 'navbar-html/teacher-details/teacher-details.html',
                controller: 'teacherDetailsController'
            })
            .otherwise({
                templateUrl: 'navbar-html/start-page.html',
                controller: 'mainController'
            });

});

app.controller('mainController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
});

app.controller('exitController', function ($scope) {
    // create a message to display in our view
    localStorage.clear();
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
        $scope.dataCourse = data;
        getVotes;
    }).error(function (data) {
        $scope.statusCourse = data;
    });
    var getVotes = $http({
        url: 'http://95.111.16.46:6420/api/VoteForCourse/' + $routeParams.id,
        method: "GET",
        xhrFields: {
            withCredentials: true
        }

    }).success(function (data) {
        $scope.votes = data;
        debugger
        $scope.sum = 0;
        angular.forEach($scope.votes.votes, function (value, key) {
            $scope.sum += value.avarage;
        });
    }).error(function (data) {
        $scope.votesStatus = data;
    });
    $scope.showDetails = true;
    $scope.showVote = false;
    $scope.showMaterials = false;
    $scope.info = function (id) {
        $scope.showDetails = !$scope.hideDetails;
        $scope.showVote = false;
        $scope.showMaterials = false;
    };
    $scope.vote = function (id) {
        $scope.showVote = !$scope.showVote;
        $scope.showDetails = false;
        $scope.showMaterials = false;
    };
    $scope.materials = function (id) {
        $scope.showMaterials = !$scope.showMaterials;
        $scope.showDetails = false;
        $scope.showVote = false;
    };
    $scope.rate = function () {
        if (typeof $scope.rating !== 'undefined') {
            $scope.rating.CourseId = $scope.dataCourse.id;
            var rating = $scope.rating;

            $http.defaults.headers.common.Authorization = localStorage.getItem("authentication");
            $http({
                url: 'http://95.111.16.46:6420/api/VoteForCourse',
                method: "POST",
                data: rating,
                xhrFields: {
                    withCredentials: true
                },
//                headers: {"Authentication": localStorage.getItem("authentication")}
            }).success(function (data) {
                debugger
                $scope.departments = data;
                alert("Вашият глас е запазен.")
            }).error(function (data) {
                debugger
                $scope.status = status;
                alert("Вашият глас не е запазен. Вече сте гласували")
            });
        }
    }



});
app.controller('teachersController', function ($scope, $http) {
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
    $scope.message = 'Look! I am an teacher page.';
});
app.controller('teacherDetailsController', function ($rootScope, $scope, $routeParams, $route, $http) {
    //If you want to use URL attributes before the website is loaded
    $http({
        url: 'http://95.111.16.46:6420/api/teachers/' + $routeParams.id,
        method: "GET",
        xhrFields: {
            withCredentials: true
        }
    }).success(function (data) {
        debugger;
        $scope.dataTeacher = data;
    }).error(function (data) {
        debugger;
        $scope.statusTeacher = data;
    });
    $http({
        url: 'http://95.111.16.46:6420/api/voteforteacher/' + $routeParams.id,
        method: "GET",
        xhrFields: {
            withCredentials: true
        }
    }).success(function (data) {
        $scope.votes = data.votes;
        $scope.sum = 0;
        angular.forEach($scope.votes, function (value, key) {
            $scope.sum += value.avarage;
        });

    }).error(function (data) {
        $scope.statusTeacherVotes = data;
    });

    $scope.showDetails = true;
    $scope.showVote = false;
    $scope.info = function (id) {
        $scope.showDetails = !$scope.hideDetails;
        $scope.showVote = false;
    };
    $scope.vote = function (id) {
        $scope.showVote = !$scope.showVote;
        $scope.showDetails = false;
    };
    $scope.rate = function () {
        if (typeof $scope.rating !== 'undefined') {
            $scope.rating.TeacherId = $scope.dataTeacher.id;
            var rating = $scope.rating;

            var auth = "Basic " + localStorage.getItem("authentication");
            console.log(auth);
            $http.defaults.headers.common.Authorization = localStorage.getItem("authentication");
            $http({
                url: 'http://95.111.16.46:6420/api/VoteForCourse/PostVoteForCourse',
                method: "POST",
                data: rating,
                xhrFields: {
                    withCredentials: true
                },
//                headers: {"Authentication": localStorage.getItem("authentication")}
            }).success(function (data) {
                debugger
                $scope.departments = data;
                alert("Вашият глас е запазен.")
            }).error(function (data) {
                debugger
                $scope.status = status;
                alert("Вашият глас не е запазен. Вече сте гласували")
            });
        }
    }

//    if ($scope.votes.length > 0) {
//        $scope.avarage = voteSum / $scope.votes.length;
//    }


});

app.controller('FileUploadCtrl', function($scope) {
    $scope.setFiles = function(element) {
        $scope.$apply(function(scope) {
        console.log('files:', element.files);
        // Turn the FileList object into an Array
        $scope.files = []
        for (var i = 0; i < element.files.length; i++) {
          $scope.files.push(element.files[i])
        }
        $scope.progressVisible = false
        });
    };

    $scope.uploadFile = function(courseId) {

        var filesData = {};
        fiilesData["courseId"] = courseId;
        filesData["fileName"] = $scope.files[0];
        $scope.progressVisible = true
        $http({
            url: 'http://95.111.16.46:6420/api/users/upload',
            method: "POST",
            data: filesData,
            xhrFields: {
                withCredentials: true
            },
//                headers: {"Authentication": localStorage.getItem("authentication")}
        }).success(function (data) {
            debugger;
            alert("Файлът е качен.")
        }).error(function (data) {
            debugger
            $scope.status = status;
            alert("Неуспешно качване на файла.")
        });
    }

    function uploadProgress(evt) {
        $scope.$apply(function(){
            if (evt.lengthComputable) {
                $scope.progress = Math.round(evt.loaded * 100 / evt.total)
            } else {
                $scope.progress = 'unable to compute'
            }
        })
    }

    function uploadComplete(evt) {
        /* This event is raised when the server send back a response */
        alert(evt.target.responseText)
    }

    function uploadFailed(evt) {
        alert("There was an error attempting to upload the file.")
    }

    function uploadCanceled(evt) {
        $scope.$apply(function(){
            $scope.progressVisible = false
        })
        alert("The upload has been canceled by the user or the browser dropped the connection.")
    }
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
//    });

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
