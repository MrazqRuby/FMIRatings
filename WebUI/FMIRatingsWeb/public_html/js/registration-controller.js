
var fmiRatingsApp = angular.module('registration', []);

fmiRatingsApp.controller("RegistrationCtrl", ["$scope", "$http", function ($scope, $http) {
        $scope.formInfo = {};

//            var parameters = {'name': 'aaa', 'password': 'aaa'};
        debugger
        $scope.saveData = function () {
            var parameters = $scope.formInfo;
            console.log(parameters);
            $http({
                url: 'http://95.111.16.46:6420/api/users/postuser',
                method: "POST",
                data: parameters,
                xhrFields: {
                    withCredentials: true
                }
//                    headers: headers
            }).success(function (data) {
                debugger;
                $scope.data = data;
                $scope.registrationHide = true;
                alert("Успешна регистрация!");
                window.location.assign("/FMIRatingsWeb/nonregistered-user-homepage.html");
            }).error(function (data) {

               $scope.status = status;
                alert("Неуспешна регистрация!");
            });//               
        };

    }]);
fmiRatingsApp.controller("LoginCtrl", ["$scope", "$http", function ($scope, $http) {
        $scope.loginFormInfo = {};

        $scope.login = function () {


            var parameters = $scope.loginFormInfo;
            console.log(JSON.stringify(parameters));
//                fmiRatingsApp.factory(function(){
            var auth = 'Basic ' + btoa($scope.loginFormInfo.Name + ":" + $scope.loginFormInfo.Password);
            localStorage.setItem("authentication", auth);
            console.log(auth);
//            var headers = {'Authentication':auth};
            $http.defaults.headers.common.Authorization = auth;
            $http({
                url: 'http://95.111.16.46:6420/api/users/getauthtoken',
                method: "POST",
                data: parameters,
//                headers: headers,
                xhrFields: {
                    withCredentials: true
                }
            }).success(function (data) {
                $scope.data = data;
                localStorage.setItem("authentication", auth);
                window.location.assign("/FMIRatingsWeb/home-page.html");
            }).error(function (status) {
                $scope.status = status;
                alert("Невалидно потребителско име или парола.");
            });
//                });
//               
        };



    }]);
;
//});
