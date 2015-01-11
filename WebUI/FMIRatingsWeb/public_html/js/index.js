/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */



//    localStorage.setItem("authentication", "dXNlcjp1c2Vy");
//    debugger
//    if(localStorage.getItem("authentication")){
//        $().load("home-page.html");
//    } else {
//        $().load("nonregistered-user-homepage.html");
//    }


var app = angular.module('indexPage', []);
app.controller('indexController', function ($scope, $location, $window) {
  debugger
  $window.location.href("home-page.html");
//        $location=$location.path("home-page.html");
    
});