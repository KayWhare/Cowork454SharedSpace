// // -----------TOGGLE BUTTONS FOR NAV-------------------

const hamburger = document.getElementById('slideNavToggle');
let slideNav = document.querySelector('.slide-nav');
let loginSlide = document.getElementById('login');
let loginBtns = document.querySelectorAll('.members-login');
var contactForm = document.getElementById('contactForm');

hamburger.addEventListener('click', function(){
   if (hamburger.checked == true ||
      contactForm.style.height == '600px' ||
      loginSlide.style.height == '70px'
      ){
         contactForm.style.height = '0px';   
         loginSlide.style.height = '0px';
         slideNav.style.height = '380px';
   }else{
    slideNav.style.height = '0px';
   }
});
function loginToggle(){
      if (loginSlide.style.height == '0px' ||
         hamburger.checked == true ||
         contactForm.style.height == '600px'){
            hamburger.checked = false;
            slideNav.style.height = '0px';
            contactForm.style.height = '0px';
            navHeight()   
      }else{
         loginSlide.style.height = '0px';
      }
   };
   function navHeight(){
      if(window.innerWidth >= 576){
         loginSlide.style.height = '60px';
      }else{
         loginSlide.style.height = '180px';
      }
    };
   window.addEventListener('resize', ()=>{
      if(loginSlide.style.height > '0px'){
         navHeight();
      }
   });
function contactToggle(){
   var contactForm = document.getElementById('contactForm');
   if(contactForm.style.height == '0px' ||
      loginSlide.style.height == '70px' ||
      hamburger.checked == true){
         hamburger.checked = false;
         loginSlide.style.height = '0px';
         slideNav.style.height = '0px';
         contactForm.style.height = '600px';
   }else{
      contactForm.style.height = '0px';
   }
}
function navHeight(){
   if(window.innerWidth >= 576){
      loginSlide.style.height = '60px';
   }else{
      loginSlide.style.height = '180px';
   }
 };

<<<<<<< HEAD
=======
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
>>>>>>> 1865941cfd487814d9b285dd30e3eac99cec6d26

