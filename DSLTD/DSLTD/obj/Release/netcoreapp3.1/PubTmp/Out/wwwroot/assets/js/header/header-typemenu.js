
// hover women navbar 
let headerTypeMenuWomen = document.querySelector(".header-type-menu-women");
let womenMegaMenu = document.querySelector(".women-megaMenu");


// Women Hover
headerTypeMenuWomen.addEventListener("mouseenter",function(){
    womenMegaMenu.classList.add("is-visible");
    setTimeout(function () {
        womenMegaMenu.classList.add("on-opacity");
    }, 20);

    $(".men-megaMenu").removeClass("is-visible");
    setTimeout(function () {
        $(".men-megaMenu").removeClass("on-opacity");
    }, 20);
})

// headerTypeMenuWomen.addEventListener("mouseleave",function(){
//     setTimeout(function () {
//         womenMegaMenu.classList.remove("is-visible");
//     }, 2000);

//     womenMegaMenu.classList.remove("on-opacity");
// })

womenMegaMenu.addEventListener("mouseenter",function(){
    womenMegaMenu.classList.add("is-visible");
    setTimeout(function () {
        womenMegaMenu.classList.add("on-opacity");
      }, 20);
})

womenMegaMenu.addEventListener("mouseleave",function(){
    womenMegaMenu.classList.remove("is-visible")
    setTimeout(function () {
        womenMegaMenu.classList.remove("on-opacity");
      }, 20);
      
})


// Men Hover 

$(function(){
    $(".header-type-menu-men").on("mouseover",function(){
        $(".men-megaMenu").addClass("is-visible");
        setTimeout(function () {
            $(".men-megaMenu").addClass("on-opacity");
        }, 20);

        $(".women-megaMenu").removeClass("is-visible");
        setTimeout(function () {
            $(".women-megaMenu").removeClass("on-opacity");
        }, 20);
    })

    $(".men-megaMenu").on("mouseover",function(){

        $(".men-megaMenu").addClass("is-visible");
        setTimeout(function () {
            $(".men-megaMenu").addClass("on-opacity");
        }, 20);

    })

    $(".men-megaMenu").on("mouseout",function(){
        $(".men-megaMenu").removeClass("is-visible");

        setTimeout(function () {
            $(".men-megaMenu").removeClass("on-opacity");
        }, 20);
    })
})