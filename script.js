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


