// // -----------TOGGLE BUTTONS FOR NAV-------------------

const hamburger = document.getElementById('slideNavToggle');
let slideNav = document.querySelector('.slide-nav');
let loginSlide = document.getElementById('login');
let loginBtns = document.querySelectorAll('.members-login');
let contactForm = document.getElementById('contact-tab');
let signUpForm = document.getElementById('signUpForm');
const hamburgerLabel = document.querySelector('.navToggleLabel');
let blackoutBG = document.querySelector(".blackout-bg");

hamburger.addEventListener('click', function () {
    if (hamburger.checked == true) {
        zeroOutHeights();
        slideNav.style.height = '380px';
        hamburger.checked = true
    } else {
        zeroOutHeights()
    }
});
function loginToggle() {
    if (loginSlide.style.height > '0px') {
        zeroOutHeights();
    } else {
        zeroOutHeights();
        navHeight();
        addExitButton();
    }
};
function navHeight() {
    if (window.innerWidth >= 576) {
        loginSlide.style.height = '60px';
    } else {
        loginSlide.style.height = '180px';
    }
};

window.addEventListener('resize', () => {
    if (loginSlide.style.height > '0px') {
        navHeight();
    } else if (contactForm.style.height > '0px') {
        contactHeight();
    }
});

window.addEventListener('resize', () => {
    if (window.innerWidth >= 768) {
        hamburger.parentElement.style.display = 'none';
    } else {
        hamburger.parentElement.style.display = 'flex';
    }
});

function zeroOutHeights() {
    signUpForm.style.height = '0px';
    slideNav.style.height = '0px'
    hamburger.checked = false;
    contactForm.style.height = '0px';
    loginSlide.style.height = '0px';
    blackoutBG.style.display = 'none';
    if (window.innerWidth >= 768) {
        hamburger.parentElement.style.display = 'none';
    }
}


function addExitButton() {
    var footertag = document.documentElement.scrollHeight;
    hamburger.parentElement.style.display = 'flex';
    hamburger.checked = true;
    blackoutBG.style.display = 'block';
    blackoutBG.style.height = `${footertag}px`;
}

function contactToggle() {
    if (contactForm.style.height > '0px') {
        zeroOutHeights()
    } else {
        zeroOutHeights();
        contactHeight();
        addExitButton();
    }
}
function contactHeight() {
    if (window.innerWidth <= 576) {
        contactForm.style.height = '1050px';
    } else if (window.innerWidth > 576 &&
        window.innerWidth < 992) {
        contactForm.style.height = '950px';
    } else {
        contactForm.style.height = '850px';
    }
};
function signUpToggle() {
    if (signUpForm.style.height > '0px') {
        zeroOutHeights();
    } else {
        zeroOutHeights();
        signUpForm.style.height = '600px';
        addExitButton();
    }
}
function myMap() {
    var mapProp = {
        center: new google.maps.LatLng(-31.906252, 115.810570),
        zoom: 16,
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(-31.906252, 115.810570),
        animation: google.maps.Animation.BOUNCE,
    });

    marker.setMap(map);
}


let loginBtn = document.getElementById('loginMobileView');
let fullNav = document.querySelector('.fullwidth-nav');
let topLogin = document.querySelectorAll('.members-login')[0];
let navLogo = document.querySelector('.nav-logo');

let bookingContainer = document.getElementById("make-booking-partial");


function findAvailable(e){
        e.preventDefault();
        var form = e.currentTarget.closest('form');
        var formData = new URLSearchParams(new FormData(form));
        fetch('/Booking/Index', {
            body: formData,
            method: "post",
            }).then(res => {
                return res.text();
            }).then(res => {
                bookingContainer.innerHTML = res;
                setTimeout(function () {
                    location.href = "#make-booking-partial";
                }, 50);
            }).catch(err => {
                console.warn('Something went wrong.', err);
            });
};

function findFromAccount(e) {
    e.preventDefault();
    var form = e.currentTarget.closest('form');
    var formData = new URLSearchParams(new FormData(form));
    fetch('/Booking/Index', {
        body: formData,
        method: "post",
    }).then(res => {
        return res.text();
    }).then(res => {
        bookingContainer.innerHTML = res;
        setTimeout(function () {
            location.href = "#make-booking-partial";
        }, 50);
    }).catch(err => {
        console.warn('Something went wrong.', err);
    });
};

window.addEventListener('load', () => {
    document.body.classList.remove('fade-out');
});

var mailingSubForm = document.querySelector('.newsletter-footer');

function mailSubscribe(e) {
    e.preventDefault();
    var form = e.currentTarget.closest('form');
    var formData = new URLSearchParams(new FormData(form));
    fetch('/Home/Index', {
        body: formData,
        method: "post",
    }).then(res => {
        return res.text();
    }).then(res => {
        mailingSubForm.innerHTML = res;
    }).catch(err => {
        console.warn('Something went wrong.', err);
    });
};

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



