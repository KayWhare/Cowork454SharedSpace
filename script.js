// // -----------TOGGLE FOR SIDEMENU & Cart-Quickview-------------------

// var checkbox = document.getElementById('openSidebarMenu');

// checkbox.addEventListener('click', function(){
//    let sidebarMenu = document.getElementById('sidebarMenu');

//    if (checkbox.checked == true){
//       return sidebarMenu.style.transform = 'translateX(0)';
//    }else{
//       sidebarMenu.style.transform = 'translateX(-250px)';
//    }
// });

// window.addEventListener("scroll", function (event) {
//     let headerDiv = document.getElementById('nav');
//     let spinner = document.querySelectorAll('.spinner');
 
//     if (window.pageYOffset > 0){
//        headerDiv.style.backgroundColor="rgba(0, 0, 0, 0.8)";
//        headerDiv.children[0].style.color = 'white';
//        headerDiv.children[3].style.color = 'white';
//        headerDiv.children[4].style.color = 'white';
//        for (i=0; i < spinner.length; i++){
//           spinner[i].style.backgroundColor='white'
//        };
//        // logo.src = 'images/newlogoWhite.png';
//     }else{
//        headerDiv.style.backgroundColor="rgba(255, 255, 255, 0.0)";
//        headerDiv.children[0].style.color = 'black';
//        headerDiv.children[3].style.color = 'black';
//        headerDiv.children[4].style.color = 'black';
//        for (i=0; i < spinner.length; i++){
//           spinner[i].style.backgroundColor='black'
//        };
//        // logo.src = 'images/newlogoDark.png';
//     }
//  });