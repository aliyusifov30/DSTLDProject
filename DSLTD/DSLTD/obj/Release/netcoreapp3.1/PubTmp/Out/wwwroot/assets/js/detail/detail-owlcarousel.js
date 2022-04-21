

$('.owl-carousel').owlCarousel({
    loop:false,
    margin:10,
    nav:false,
    dots:false,
    mouseDrag:false,
    touchDrag:false,
    pullDrag:false,
    freeDrag:false,
    width:83,
    responsive:{
        0: {
            items:1,
        },
        600: {
            items:1,
        },
        1000: {
            items:1,
        },
        1141:{
            items:1
        }
    }
})

$('.owl-carousel-also-like').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    dots:false,
    mouseDrag:false,
    touchDrag:false,
    pullDrag:false,
    freeDrag:false,
    responsive:{
        0: {
            touchDrag: true,
            items:1,
        },
        641: {
            touchDrag: true,
            items:2,
        },
        1006: {
            touchDrag: true,
            items:3
        },
        1141:{
            items:4
        }
    }
})

$('.owl-carousel-big-image').owlCarousel({
    loop:false,
    margin:10,
    nav:false,
    dots:false,
    mouseDrag:false,
    touchDrag:false,
    pullDrag:false,
    freeDrag:false,
    autoWidth:true,
    responsive:{
        0:{
            items:1,
            autoWidth:true,
            dots:true,
            touchDrag:true
        },
        600:{
            items:1,
            autoWidth:true,
            dots:true,
            touchDrag:true
        },
        1006:{
            items:1,
        }
    }
})

