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
            .when("/search", {
                templateUrl: "navbar-html/search-results.html",
                controller: "searchController"
            })
            .otherwise({
                templateUrl: 'navbar-html/start-page.html',
                controller: 'mainController'
            });

});

app.controller('mainController', function ($scope, $http) {
    // create a message to display in our view
    $http({
        url: 'http://95.111.16.46:6420/api/Statistic/Query?objectType=teacher',
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

        getMaterials;
    }).error(function (data) {
        $scope.votesStatus = data;
    });

    var getMaterials = $http({
        url: 'http://95.111.16.46:6420/api/files/forcourse/' + $routeParams.id,
        method: "GET",
        xhrFields: {
            withCredentials: true
        }
    }).success(function (data) {
        $scope.courseMaterials = data;
        debugger

    }).error(function (data) {
        debugger
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
        if (typeof $scope.teachersRting !== 'undefined') {
            $scope.teachersRting.TeacherId = $scope.dataTeacher.id;
            debugger
            var teachersRting = $scope.teachersRting;

            var auth = + localStorage.getItem("authentication");
            console.log(auth);
            $http.defaults.headers.common.Authorization = localStorage.getItem("authentication");
            $http({
                url: 'http://95.111.16.46:6420/api/VoteForTeacher/PostVoteForTeacher',
                method: "POST",
                data: teachersRting,
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

app.controller('FileUploadCtrl', function ($scope, $http) {
    $scope.setFiles = function (element) {
        $scope.$apply(function (scope) {
            console.log('files:', element.files);
            // Turn the FileList object into an Array
            $scope.files = []
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i])
            }
            $scope.showUploadButton = true;
            $scope.progressVisible = false;
            console.log("file is vissible");
        });
    };

    $scope.uploadFile = function (courseId) {

        var filesData = {};
        filesData["courseId"] = courseId;
        filesData["fileName"] = $scope.files[0];
        $scope.progressVisible = true
        var fd = new FormData();
        fd.append('fileName', $scope.files[0]);
        fd.append('courseId', courseId);
        $http.post('http://95.111.16.46:6420/api/users/upload', fd, {
            transformRequest: angular.identity,
            headers: {'Content-Type': undefined}
        })
                .success(function (data) {
                    debugger
                })
                .error(function (data) {
                    debugger
                });
    }

    function uploadProgress(evt) {
        $scope.$apply(function () {
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
        $scope.$apply(function () {
            $scope.progressVisible = false
        })
        alert("The upload has been canceled by the user or the browser dropped the connection.")
    }
});

app.controller("searchController", function($scope, $http) {
    debugger;

$scope.text = "stamo";
    //var searchText = $scope.searchText || "ов";

$scope.search = function(searchText) {
    $scope.text = "siyana";
    if (searchText) {
        $http({
            url: "http://95.111.16.46:6420/api/teachers/search/" + searchText,
            method: "GET",
            xhrFields: {
                withCredentials: true
            }

        }).success(function(data) {
                debugger;
                $scope.teachersSearchResults = data;
            }).error(function(data) {
                $scope.message = "В момента има проблем с търсенето в системата. Опитайте отново.";
            });

        $http({
            url: "http://95.111.16.46:6420/api/courses/search/" + searchText,
            method: "GET",
            xhrFields: {
                withCredentials: true
            }

        }).success(function(data) {
                debugger;
                $scope.coursesSearchResults = data;
            }).error(function(data) {
                $scope.message = "В момента има проблем с търсенето в системата. Опитайте отново.";
            });


    }
    else {
        $scope.message = "Не сте въвели информация за търсене. Опитайте отново.";
    }
}
});
