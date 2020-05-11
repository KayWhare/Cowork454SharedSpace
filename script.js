// // -----------TOGGLE BUTTONS FOR NAV-------------------

const hamburger = document.getElementById('slideNavToggle');
let slideNav = document.querySelector('.slide-nav');
let loginSlide = document.getElementById('login');
let loginBtns = document.querySelectorAll('.members-login');
var contactForm = document.getElementById('contactForm');
var signUpForm = document.getElementById('signUpForm');

hamburger.addEventListener('click', function(){
   if (hamburger.checked == true){
      zeroOutHeights();
      slideNav.style.height = '380px';
      hamburger.checked = true
   }else{
      slideNav.style.height = '0px';
   }
});
function loginToggle(){
      if (loginSlide.style.height > '0px'){
         loginSlide.style.height = '0px'
      }else{
         zeroOutHeights();
         navHeight();
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

   function zeroOutHeights(){
      signUpForm.style.height = '0px';
      slideNav.style.height = '0px'
      hamburger.checked = false;
      contactForm.style.height = '0px';
      loginSlide.style.height = '0px';
   }
   
function contactToggle(){
   var contactForm = document.getElementById('contactForm');
   if(contactForm.style.height > '0px'){
      contactForm.style.height = '0px';
   }else{
      zeroOutHeights();
      contactForm.style.height = '600px';
   }
}
function signUpToggle(){
   if(signUpForm.style.height > '0px'){
      signUpForm.style.height = '0px';
   }else{
      zeroOutHeights();
      signUpForm.style.height = '600px';
   }
}


let loginBtn = document.getElementById('loginMobileView');
let fullNav = document.querySelector('.fullwidth-nav');
let topLogin = document.querySelectorAll('.members-login')[0];
let navLogo = document.querySelector('.nav-logo');


window.onscroll = function() { scollFunc()};

function scollFunc() {

   const yTop = document.documentElement.scrollTop;

   const topbox = document.getElementById("inner-top");

   const midbox = document.getElementById("inner-mid");

   const botbox = document.getElementById("inner-bot");

 if (yTop > 600) {
    topbox.classList.add("left-affect");
 }

 if (yTop>1200){
   midbox.classList.add("right-affect");
 }

 if(yTop>1600){
   botbox.classList.add("left-affect");
 }

}



