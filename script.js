// // -----------NAV TOGGLE-------------------

const hamburger = document.getElementById('slideNavToggle');
let slideNav = document.querySelector('.slide-nav');

hamburger.addEventListener('click', function(){
   if (hamburger.checked == true){
      return slideNav.style.height = '380px';
   }else{
    slideNav.style.height = '0px';
   }
});

let loginBtn = document.getElementById('loginMobileView');
let fullNav = document.querySelector('.fullwidth-nav');
let topLogin = document.querySelectorAll('.members-login')[0];
let navLogo = document.querySelector('.nav-logo');


window.onscroll = function() { scollFunc()};

function scollFunc() {

   const yTop = document.documentElement.scrollTop;

   const topimg = document.getElementById("top-img");
   const topbox = document.getElementById("top-box");

   const midimg = document.getElementById("mid-img");
   const midbox = document.getElementById("mid-box");

   const botimg = document.getElementById("bot-img");
   const botbox = document.getElementById("bot-box");

 if (yTop > 600) {
    topimg.classList.add("right-affect");
    topbox.classList.add("left-affect");
 }

 if (yTop>1000){
   midimg.classList.add("left-affect");
   midbox.classList.add("right-affect");
 }

 if(yTop>1400){
   botimg.classList.add("right-affect");
   botbox.classList.add("left-affect");
 }

}


// window.addEventListener('scroll', ()=>{
//    var fullnavTop = fullNav.scrollTop = 150;
//    if(window.pageYOffset > fullnavTop ){
//       loginBtn.style.display = 'flex';
//       navLogo.style.display = 'flex';
//    }else if(window.pageYOffset < 120 &&
//       window.innerWidth > 992){
//       loginBtn.style.display = 'none';
//    // }else if(topLogin.style.display = 'flex' &&
//    //    window.innerWidth > 768) {
//    //    loginBtn.style.display = 'none';
//    // }else if(window.innerWidth < 768){
//    //    loginBtn.style.display = 'flex';
//    //    loginBtn.style.marginRight = '60px'
   //       }
   // });

