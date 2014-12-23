/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

$(document).ready(function () {

    $("#body-wrapper").html(function () {
//      $("#body-wrapper").html("<b>This is div</b>");
        $("#body-wrapper").load("navbar-html/start-page.html #start-page", function () {
            loadStartPage();
        });


        function loadStartPage() {
            $("#start-page").html(function () {
                $("#start-page-carousel-div").load("navbar-html/carousel-component.html #carousel-generic", function () {
//                    $("#start-page-info-div").html("<b>This is div</b>");
                });
            });
        }

    });













});
