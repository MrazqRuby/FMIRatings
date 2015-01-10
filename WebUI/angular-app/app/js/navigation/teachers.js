/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

var TEACHER_RATING_TAB = "teacher-rating-tab";
var TEACHER_RATE_TABLE = "teacher-rate-table";
var TEACHER_COMMENT_TEMPLATE = "teacher-comment-template";

$(document).ready(function () {
    
 $(".teacher-open").click(function () {
        console.log("in funcion");
        var divParent = $(this).parent().parent().parent();
        console.log(divParent.attr("id"));
        if (divParent.attr("id")) {
            $("#body-wrapper").load("navbar-html/teacher-details/teacher-details.html", function () {
                $("#teacher-name").text(divParent.attr("id"));
                $("#teacher-choosen-tab-container").load("navbar-html/teacher-details/teacher-details-tab.html");
            });
        }

    });
    
    $('#teacher-tabs-nav a').click(function () {

        $(this).parent().parent().find("li").removeClass("active");
        $(this).parent().addClass("active");

        var hrefText = $(this).attr('href');
        var actualHrefText = hrefText.replace("#", "");

        console.log(actualHrefText);
        var page = "navbar-html/teacher-details/" + actualHrefText + ".html";

        $("#teacher-choosen-tab-container").load(page, function () {
            if (actualHrefText === TEACHER_RATING_TAB) {
                $("#teacher-ratings-table-container").load("navbar-html/teacher-details/" + TEACHER_RATE_TABLE + ".html");
                $("#teacher-comments-container").load("navbar-html/teacher-details/" + TEACHER_COMMENT_TEMPLATE + ".html");
            }
        });
    });

});
