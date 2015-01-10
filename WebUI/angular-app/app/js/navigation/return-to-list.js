/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(function () {
 $('.show-list').click(function () {
        var hrefText = $(this).attr('href');
        var actualHrefText = hrefText.replace("#", "");

        var page = "navbar-html/"+actualHrefText + ".html";
        $("#body-wrapper").load(page);
    });
});


