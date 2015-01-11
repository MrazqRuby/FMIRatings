/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

var TEACHER_RATING_TAB = "teacher-rating-tab";
var TEACHER_RATE_TABLE = "teacher-rate-table";
var TEACHER_COMMENT_TEMPLATE = "teacher-comment-template";

// $(document).ready(function () {
    // var headers = {
    //     'Access-Control-Allow-Origin': '*'};
//'Access-Control-Allow-Headers': 'X-PINGOTHER',
//'Access-Control-Max-Age': '1728000'};
/*
    $.ajax({
        method: "GET",
        url: "http://95.111.16.46:6420/api/Teachers",
//        dataType: 'text json',
//contentType: 'application/json; charset=utf-8',
        //headers: headers,
        success: function (msg) {
            console.log("success");
            alert($('Result', JSON.stringify(msg)));
        },
        error: function (msg) {
            
            console.log(JSON.stringify(msg));
            alert(JSON.stringify(msg));
        }
    });
    */

    $(document).on("click", ".teacher-open", function () {
        console.log("in funcion");
        var divParent = $(this).parent().parent().parent();

            $("#body-wrapper").load("navbar-html/teacher-details/teacher-details.html"); 
    });

//     var serviceUrl = 'http://95.111.16.46:6420/api/TeacherDepartments'; 


//         $.ajax({
//             type: "get",
//             url: serviceUrl,
//             xhrFields: {
//                 withCredentials: true
//             }        
//         }).done(function (data) {
// //            debugger;
// console.log(data);
//             $('#value1').text(data);
//         }).error(function (jqXHR, textStatus, errorThrown) {
// //                        debugger;
// //            $('#value1').text(jqXHR.responseText || textStatus);
// console.log(textStatus);
//         });
    


    // $(".teacher-open").click(function () {
    //     console.log("in funcion");
    //     var divParent = $(this).parent().parent().parent();
    //     console.log(divParent.attr("id"));
    //     if (divParent.attr("id")) {
    //         $("#body-wrapper").load("navbar-html/teacher-details/teacher-details.html", function () {
    //             $("#teacher-name").text(divParent.attr("id"));
    //             $("#teacher-choosen-tab-container").load("navbar-html/teacher-details/teacher-details-tab.html");
    //         });
    //     }

    // });

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
