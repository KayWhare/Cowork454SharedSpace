
var galleryThumbs = new Swiper('.gallery-thumbs-one', {
    spaceBetween: 10,
    slidesPerView: 5,

    freeMode: true,
    loopedSlides: 5, //looped slides should be the same
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
  });



  var galleryTop = new Swiper('.gallery-top-one', {
    spaceBetween: 10,
    loop:false,
    loopedSlides: 5, //looped slides should be the same
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    thumbs: {
      swiper: galleryThumbs,
    },
  });



  var galleryThumbs = new Swiper('.gallery-thumbs-two', {
    spaceBetween: 10,
    slidesPerView: 5,

    freeMode: true,
    loopedSlides: 5, //looped slides should be the same
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
  });



  var galleryTop = new Swiper('.gallery-top-two', {
    spaceBetween: 10,
    loop:false,
    loopedSlides: 5, //looped slides should be the same
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    thumbs: {
      swiper: galleryThumbs,
    },
  });

  var galleryThumbs = new Swiper('.gallery-thumbs-three', {
    spaceBetween: 10,
    slidesPerView: 5,

    freeMode: true,
    loopedSlides: 5, //looped slides should be the same
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
  });



  var galleryTop = new Swiper('.gallery-top-three', {
    spaceBetween: 10,
    loop:false,
    loopedSlides: 5, //looped slides should be the same
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    thumbs: {
      swiper: galleryThumbs,
    },
  });