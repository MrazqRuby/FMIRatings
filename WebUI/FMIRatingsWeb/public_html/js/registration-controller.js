/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

$(document).ready(function () {
    var fmiRatingsApp = angular.module('registration', []);

    fmiRatingsApp.controller("RegistrationCtrl", ["$scope", "$http", function ($scope, $http) {
            $scope.formInfo = {};
            
//            var parameters = {'name': 'aaa', 'password': 'aaa'};
           
            $scope.saveData = function () {
                var parameters = {"name" : $scope.formInfo.Name ,"password" : $scope.formInfo.Password};
                 console.log(JSON.stringify(parameters));
//                fmiRatingsApp.factory(function(){
                var headers = {'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
                    'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'};
                $http({
                    url: 'http://95.111.16.46:6420/api/users/postuser',
                    method: "POST",
                    data: JSON.stringify(parameters),
                    headers: headers
                }).success(function (data, status, headers, config) {
                    $scope.data = data;
                }).error(function (data, status, headers, config) {
                    $scope.status = status;
                });
//                });
//               
            };


        }]);
    fmiRatingsApp.controller("LoginCtrl", ["$scope", "$http", function ($scope, $http) {
            $scope.loginFormInfo = {};
            
$scope.login = function () {
                var parameters = {"name" : "siyana" ,"password" : $scope.loginFormInfo.Password};
                 console.log(JSON.stringify(parameters));
//                fmiRatingsApp.factory(function(){
                var headers = {'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
                    'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'};
                $http({
                    url: 'http://95.111.16.46:6420/api/users/getauthtoken',
                    method: "POST",
                    data: JSON.stringify(parameters),
                    headers: headers
                }).success(function (data, status, headers, config) {
                    $scope.data = data;
                }).error(function (data, status, headers, config) {
                    $scope.status = status;
                });
//                });
//               
            };



        }]);
    ;
});
