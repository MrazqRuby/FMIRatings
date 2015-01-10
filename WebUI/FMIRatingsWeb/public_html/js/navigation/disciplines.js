/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

//constants
var DISCIPLINE_RATING_TAB = "discipline-ratings-tab";

var DISCIPLINE_COMMENT_TEMPLATE = "discipline-comment-template";
var DISCIPLINE_RATE_TABLE = "discipline-rate-table";

$(document).ready(function () {

var serviceUrl = 'http://95.111.16.46:6420/api/Courses';

  $.ajax({
            type: "get",
            url: serviceUrl,
            xhrFields: {
                withCredentials: true
            }        
        }).done(function (data) {
            debugger;
            $('#value1').text(data);
        }).error(function (jqXHR, textStatus, errorThrown) {
                        debugger;
            $('#value1').text(jqXHR.responseText || textStatus);
        });
        
    console.log("show panel");
    $(".discipline-open").click(function () {
        console.log("in funcion");
        var divParent = $(this).parent().parent().parent();
        console.log(divParent.attr("id"));
        if (divParent.attr("id")) {
            $("#body-wrapper").load("navbar-html/discipline-details/discipline-details.html", function () {
                $("#discipline-title").text(divParent.attr("id"));
                $("#discipline-choosen-tab-container").load("navbar-html/discipline-details/discipline-details-tab.html");
            });
        }

    });

    $('#discipline-tabs-nav a').click(function () {

        $(this).parent().parent().find("li").removeClass("active");
        $(this).parent().addClass("active");

        var hrefText = $(this).attr('href');
        var actualHrefText = hrefText.replace("#", "");

        console.log(actualHrefText);
        var page = "navbar-html/discipline-details/" + actualHrefText + ".html";

        $("#discipline-choosen-tab-container").load(page, function () {
            if (actualHrefText === DISCIPLINE_RATING_TAB) {
                $("#discipline-ratings-table-container").load("navbar-html/discipline-details/" + DISCIPLINE_RATE_TABLE + ".html");
                $("#discipline-comments-container").load("navbar-html/discipline-details/" + DISCIPLINE_COMMENT_TEMPLATE + ".html");
            }
        });
    });
    
    


});