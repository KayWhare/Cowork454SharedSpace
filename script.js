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
