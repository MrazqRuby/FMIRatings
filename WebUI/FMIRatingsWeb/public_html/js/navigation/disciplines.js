/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

$(document).ready(function () {

        console.log("show panel");
        $(".discipline-open").click(function () {
            console.log("in funcion");
            var divParent = $(this).parent().parent().parent();
            console.log(divParent.attr("id"));
            if (divParent.attr("id")) {
                $("#body-wrapper").load("navbar-html/discipline-details/discipline-details.html", function(){
                    $("#discipline-title").text(divParent.attr("id"));
                });
            }

        });


});