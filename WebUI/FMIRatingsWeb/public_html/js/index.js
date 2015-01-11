/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

    if(localStorage.getItem("authentication")){
        window.location.assign( "/FMIRatingsWeb/home-page.html");
    } else {
        window.location.assign("/FMIRatingsWeb/nonregistered-user-homepage.html");
    }


