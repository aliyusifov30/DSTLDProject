let allFilterLi = document.querySelectorAll(".collection-filter-sort .collection-filter ul li");
let intViewportWidth = window.innerWidth;
let collectionListItemButton = document.querySelectorAll(".collection-list-item-button")

$(function(){

    // List Open Close and Icon Rotate
    $(".collection-filter-sort .collection-filter ul li .collection-type-name").click(function(e){
        e.preventDefault();
        let DropdownMenu = $(this).next();

        if(!$(this).next().hasClass("d-flex")){
            allFilterLi.forEach(element => {
            if(element.lastElementChild.classList.contains("d-flex"))
            {
                element.lastElementChild.classList.remove("d-flex");
            }            
            element.firstElementChild.firstElementChild.style.transform = "rotate(0deg)";
            element.firstElementChild.firstElementChild.style.transition = "0.5s";
            });
        }
        $(this).next().toggleClass("d-flex")

        if($(this).next().hasClass("d-flex")){
            $(this).children().css("transform" , "rotate(180deg)")
            $(this).children().css("transition" , "0.5s")
        }else{
            $(this).children().css("transform" , "rotate(0deg)")
            $(this).children().css("transition" , "0.5s")
        }
    })

    // Filter side bar Open

    $(".collection-filter .collection-responsive-title").click(function(){
        $(".filter-side-bar").removeClass("filter-side-bar-active animate__animated animate__fadeOutRight")
        $(".filter-side-bar").addClass("filter-side-bar-active animate__animated animate__fadeInRight")

        $(".background-black").css("display","block");
        $(".background-black").css("height",document.body.clientHeight + "px");
    })

    //Filter side bar Close

    $("#collection-title-section .filter-side-bar .x-btn i").click(function(){
        console.log("click x-btn")
        $(".filter-side-bar").removeClass("filter-side-bar-active animate__animated animate__fadeInRight")
        $(".filter-side-bar").addClass("filter-side-bar-active animate__animated animate__fadeOutRight")

        $(".background-black").css("display","none");
    })
    
    //Sort dropdown Open Close

    $("#collection-title-section .collection-sort .items .collection-sort-title a").click(function(e){
        e.preventDefault();
        if($("#collection-title-section .collection-sort-list .dropdown").hasClass("is-visibility-visible")){

            $("#collection-title-section .collection-sort-list .dropdown").css("transition","0.4s")
            $("#collection-title-section .collection-sort-list .dropdown").removeClass("is-visibility-visible")
            $("#collection-title-section .collection-sort-list .dropdown").css("opacity","0")

        }else{
            console.log("else")

            $("#collection-title-section .collection-sort-list .dropdown").css("transition","0.4s")
            $("#collection-title-section .collection-sort-list .dropdown").css("opacity","1")
            $("#collection-title-section .collection-sort-list .dropdown").addClass("is-visibility-visible")
        }
    })

    // Sort side bar Open

    $(".collection-filter-sort .collection-sort .collection-responsive-title").click(function(){
        $(".background-black").css("display","block");
        $(".background-black").css("height",document.body.clientHeight + "px");
        $(".sort-side-bar").css("transform","translate(0px, 0px)");
        $(".sort-side-bar").removeClass("animate__animated animate__fadeOutDown")
        $(".sort-side-bar").addClass("animate__animated animate__fadeInUp")
    })
    // Sort side bar Close

    $(".sort-side-bar .x-btn").click(function(){

        // $(".sort-side-bar").css("transform","translate(0px,290px)");
        $(".sort-side-bar").removeClass("animate__animated animate__fadeInUp")
        $(".sort-side-bar").addClass("animate__animated animate__fadeOutDown")
        
        $(".background-black").css("display","none");
    })
})

let aaaSize = window.innerWidth;

window.onresize = function(){
    aaaSize = window.innerWidth;
    console.log("123123123")
    if(aaaSize > 768){
        $(".filter-side-bar").removeClass("filter-side-bar-active ")
        $(".background-black").css("display","none");
    }
}

collectionListItemButton.forEach(elem => {
    
    elem.addEventListener("click",function(e){
        e.preventDefault();
    })
});


let listLabels = document.querySelectorAll(".collection-filter-sort ul li .collection-list label");
console.log(listLabels);

listLabels.forEach(element => {

    // element.classList.remove("label-active")
    element.addEventListener("click",function(){

        listLabels.forEach(element => {
            element.classList.remove("label-active")
        });
        element.classList.add("label-active")
    })
});

let listImageLabels = document.querySelectorAll(".collection-filter-sort ul li .collection-list .image-btns label");

listImageLabels.forEach(element => {

    element.addEventListener("click",function(){

        listLabels.forEach(element => {
            element.classList.remove("image-label-active")
        });
        element.classList.add("image-label-active")
    })
});


//////////////////////////
////Picture Image Load////
//////////////////////////


// let scrollNewY = window.scrollY;
// let colletionCards = document.querySelectorAll("#collection-card .col-sep .card");
// let manScroll = 0;
// let first = 0;
// let sec = 0;

// colletionCards.forEach(element => {
//     element.style.opacity = "0";
// });

// document.addEventListener('scroll', function(e) {
//     console.log(colletionCards);
    
//     console.log(scrollNewY)
//     scrollNewY = window.scrollY;
//     for(let k = 0 ; k < colletionCards.length ; k++){
//         if(scrollNewY > manScroll){
//             if(k%3==0){
//                 manScroll+=700;
//                 first = k + 1;
//                 sec = k + 2;
//             }
//             if(k===first){
//                 colletionCards[first].style.transform = "translate(0px, 230px)";
//             }
//             if(k===sec){
//                 colletionCards[sec].style.transform = "translate(0px, 260px)";
//             }
//             colletionCards[k].style.transform = "translate(0px, 200px)"
//             setTimeout(function () {
//                 colletionCards[k].style.transition = "2s";
//                 colletionCards[k].style.transform = "translate(0px, 0px)";
//                 colletionCards[k].style.opacity = "1";
//             }, 20);

//         }
//     }
// });
  



// for(let k = 0 ; k<3 ; k++){
//     // colletionCards[k].style.transform = "translate(0px, 200px)" `translate(0px, ${heightUp}px)`
//     colletionCards[k].style.transform = "translate(0px, 200px)"
//     if(k===1){
//         colletionCards[1].style.transform = "translate(0px, 230px)";
//     }
//     if(k===2){
//         colletionCards[2].style.transform = "translate(0px, 260px)";
//     }
//     setTimeout(function () {
//         heightUp += 20;
//         colletionCards[k].style.transition = "1s";
//         colletionCards[k].style.transform = "translate(0px, 0px)";
//         colletionCards[k].style.opacity = "1";
//     }, 20);
// }

