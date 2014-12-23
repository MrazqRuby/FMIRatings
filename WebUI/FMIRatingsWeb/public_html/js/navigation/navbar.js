/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

//page names constant
var START_PAGE = "start-page";
var DISCIPLINES_LIST = "disciplines-list-page";

$(document).ready(function () {


    $("#body-wrapper").load("navbar-html/start-page.html #start-page", function () {
        loadStartPage();
    });

//        START PAGE LOADING
    function loadStartPage() {
        $("#start-page").html(function () {
            console.log("load start page");
            $("#start-page-carousel-div").load("navbar-html/carousel-component.html #carousel-generic");
            $("#start-page-info-div").html("<b>This is div</b>");
        });
    }

//        NAVBAR PAGE LOADING
    $('.nav a').click(function () {
        
        $(this).parent().parent().find("li").removeClass("active");
        $(this).parent().addClass("active");

        var hrefText = $(this).attr('href');
        var actualHrefText = hrefText.replace("#", "");

        console.log(actualHrefText);
        var page = "navbar-html/" + actualHrefText + ".html";

        $("#body-wrapper").load(page, function () {
            if (actualHrefText === START_PAGE) {
                loadStartPage();
            }
        });
    });



});
