
let input = document.querySelector("header .search-side form input");

$("header .navbar .navbar-search-btn").click(function(e){

    e.preventDefault();

    $(".search-side").css("display","block")
    
    $(".search-side").removeClass("animate__animated  animate__fadeOutUp")
    $(".search-side").addClass("animate__animated  animate__fadeInDown")
    $(".search-side").css("transform","translate(0px,0px)");

    $(".background-black").css("display","block");
    $(".background-black").css("height",document.body.clientHeight + "px");
})


$("header .navbar .bi-search").click(function(e){

    e.preventDefault();

    $(".search-side").css("display","block")

    $(".search-side").removeClass("animate__animated  animate__fadeOutUp")
    $(".search-side").addClass("animate__animated  animate__fadeInDown")
    $(".search-side").css("transform","translate(0px,0px)");

    $(".background-black").css("display","block");
    $(".background-black").css("height",document.body.clientHeight + "px");
})

$(".search-side .close-btn a").click(function(e){

    e.preventDefault();

    $(".search-side").removeClass("animate__animated  animate__fadeInDown")
    $(".search-side").addClass("animate__animated  animate__fadeOutUp")

    // $(".search-side").css("transition","0.5s");
    // $(".search-side").css("transform","translate(0px,-100%)");

    $(".background-black").css("display","none");

    $(".result-of-search").css("display","none");
})

input.addEventListener("keyup",function(){

    let word = input.value;

    fetch('/search/searching?word=' + word)
        .then(response => response.text())
        .then(data => {
            var s = document.querySelector(".result-of-search");

            s.innerHTML = data;
        })

    $(".result-of-search").css("display","block")

    if(input.value === ""){
        $(".result-of-search").css("display","none")
    }
})

//$(function () {

//    $("#search form input").keyup(function () {

//        alert("asd");

//        let word = $(this).val()

//        fetch('https://localhost:44364/search/searching?word=' + word)
//            .then(response => response.text())
//            .then(data => {
//                var s = document.querySelector(".result-box-title");
//                s.innerHTML = data;
//            })


//        $("#search .result-of-search").css("display", "block")

//        if (searchInput.value === "") {
//            console.log("if")
//            $("#search .result-of-search").css("display", "none")
//        }
//    })
//})





// Search HTML
