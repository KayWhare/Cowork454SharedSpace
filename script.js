// // -----------TOGGLE BUTTONS FOR NAV-------------------

const hamburger = document.getElementById('slideNavToggle');
let slideNav = document.querySelector('.slide-nav');
let loginSlide = document.getElementById('login');
let loginBtns = document.querySelectorAll('.members-login');
let contactForm = document.getElementById('contactForm');
let signUpForm = document.getElementById('signUpForm');
const hamburgerLabel = document.querySelector('.navToggleLabel'); 

hamburger.addEventListener('click', function(){
   if (hamburger.checked == true){
      zeroOutHeights();
      slideNav.style.height = '380px';
      hamburger.checked = true
   }else{
      zeroOutHeights()
   }
});
function loginToggle(){
      if (loginSlide.style.height > '0px'){
         zeroOutHeights();
      }else{
         zeroOutHeights();
         navHeight();
         addExitButton();
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

   window.addEventListener('resize', ()=>{
      if(window.innerWidth >= 768){
         hamburger.parentElement.style.display = 'none';
      }else{
         hamburger.parentElement.style.display = 'flex';
      }
   });

   function zeroOutHeights(){
      signUpForm.style.height = '0px';
      slideNav.style.height = '0px'
      hamburger.checked = false;
      contactForm.style.height = '0px';
      loginSlide.style.height = '0px';
      if(window.innerWidth >= 768){
         hamburger.parentElement.style.display = 'none';
      }
   }
   function addExitButton(){
      hamburger.parentElement.style.display = 'flex';
      hamburger.checked = true;
   }
   
function contactToggle(){
   var contactForm = document.getElementById('contactForm');
   if(contactForm.style.height > '0px'){
      zeroOutHeights()
   }else{
      zeroOutHeights();
      contactForm.style.height = '600px';
      addExitButton();
   }
}
function signUpToggle(){
   if(signUpForm.style.height > '0px'){
      zeroOutHeights();
   }else{
      zeroOutHeights();
      signUpForm.style.height = '600px';
      addExitButton();
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



