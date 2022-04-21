

// Burger Menu Open Element
let headerBar = document.querySelector(".header-bars");
// Burger Menu Close Element
let xIcon = document.querySelector(".x-icon");

//Burger Side Bar Type Menu Element
let sideBarElement = document.querySelectorAll(".header-bg-black-items ul li")

//Burger Menu Element Open Click
headerBar.addEventListener("click",function(){

    $(".header-side-bar-burger-menu").removeClass("animate__animated animate__fadeOutLeft");
    $(".header-side-bar-burger-menu").addClass("animate__animated animate__fadeInLeft");
    $(".header-side-bar-burger-menu").css("transform","translate(0px,0px)")
})

//Burger Menu Element Close Click

xIcon.addEventListener("click",function(){

  $(".header-side-bar-burger-menu").removeClass("animate__animated animate__fadeInLeft");
  $(".header-side-bar-burger-menu").addClass("animate__animated animate__fadeOutLeft");
})

// Burger Side Bar Type Menu Open Click

$(".header-bg-black-items ul li").click(function() {
    $(".plus-icon",this).toggleClass("d-none");
    $(".cancel-line-icon",this).toggleClass("cancel-line-icon-animation");
    $(".extra-elements",this).toggleClass("d-block");
});



