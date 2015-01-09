/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

$(document).ready(function () {
    var fmiRatingsApp = angular.module('registration', []);

    fmiRatingsApp.controller("RegistrationCtrl", ["$scope", function ($scope) {
            $scope.formInfo = {};
            var parameters = $scope.formInfo.Name + $scope.formInfo.Passwod;
            $scope.saveData = function () {
                fmiRatingsApp.factory(function(){
                     $scope.persons = $http({
                    url: 'http://95.111.16.46:6420/api/Users',
                    method: "POST",
                    data: parameters,
                    headers: {'Content-Type': 'application/x-www-form-urlencoded'}
                }).success(function (data, status, headers, config) {
                    $scope.data = data; // how do pass this to $scope.persons?
                }).error(function (data, status, headers, config) {
                    $scope.status = status;
                });
                });
               
            };


        }])
            .controller('MyCtrl2', [function () {

                }]);




});
