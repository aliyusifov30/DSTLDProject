
$('.owl-carousel').owlCarousel({
    margin:10,
    nav:true,
    dots: false,
  
    responsive:{
        0:{
            items: 1,
            mouseDrag: true,
            touchDrag: true,
            nav:false,
            stagePadding: 50,
        },
        641: {
            mouseDrag: true,
            touchDrag: true,
            nav:false,
            items:2,
            stagePadding: 50,
        },
        1010: {
            mouseDrag: false,
            touchDrag: false,
            pullDrag: false,
            freeDrag: false,
            items:3,
            slideBy:4
        },
        1200: {
            mouseDrag: false,
            touchDrag: false,
            pullDrag: false,
            freeDrag: false,
            items:4,
            slideBy:4
        }
    }
})

$(".owl-carousel .owl-nav .disabled").css("display","none");
$(".owl-carousel .owl-nav button").click(function(){
    let buttons = document.querySelectorAll(".owl-carousel .owl-nav button")
    let disabled = document.querySelector(".owl-carousel .owl-nav .disabled")

    console.log(buttons)
    buttons = Array.from(buttons);
    buttons.forEach(element => {
        console.log(element);
        element.style.display = "block";
    });

    disabled.style.display = "none";
})
