/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */



//    localStorage.setItem("authentication", "dXNlcjp1c2Vy");
//    console.log(localStorage.getItem("authentication").length > 0);
    
    if(localStorage.getItem("authentication")){
        window.location.assign( "/FMIRatingsWeb/home-page.html");
//        $().load("home-page.html");
    } else {
//        $().load("nonregistered-user-homepage.html");
        window.location.assign("/FMIRatingsWeb/nonregistered-user-homepage.html");
//        window.location.assign("http://www.w3schools.com")
    }


