
var galleryThumbs = new Swiper('.gallery-thumbs-one', {
    spaceBetween: 5,
    slidesPerView: 5,

    freeMode: true,
    loopedSlides: 5, //looped slides should be the same
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
    setWrapperSize: true,
  });



  var galleryTop = new Swiper('.gallery-top-one', {
    spaceBetween: 5,
    loop:false,
    loopedSlides: 5, //looped slides should be the same
    setWrapperSize: true,
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    thumbs: {
      swiper: galleryThumbs,
    },
  });



  var galleryThumbs = new Swiper('.gallery-thumbs-two', {
    spaceBetween: 5,
    slidesPerView: 5,
    freeMode: true,
    loopedSlides: 5, //looped slides should be the same
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
    setWrapperSize: true,
  });



  var galleryTop = new Swiper('.gallery-top-two', {
    spaceBetween: 5,
    loop:false,
    loopedSlides: 5, //looped slides should be the same
    setWrapperSize: true,
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    thumbs: {
      swiper: galleryThumbs,
    },
  });

  var galleryThumbs = new Swiper('.gallery-thumbs-three', {
    spaceBetween: 5,
    slidesPerView: 5,

    freeMode: true,
    loopedSlides: 5, //looped slides should be the same
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
    setWrapperSize: true,
  });



  var galleryTop = new Swiper('.gallery-top-three', {
    spaceBetween: 5,
    loop:false,
    loopedSlides: 5, //looped slides should be the same
    setWrapperSize: true,
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    thumbs: {
      swiper: galleryThumbs,
    },
  });


var swiper = new Swiper('.swiper-container-hero', {
    autoplay: {
        delay: 5000,
    },
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },

    disableOnInteraction: false,
});