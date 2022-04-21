
let smallImages = document.querySelectorAll(".small-pic-item .item .image")

let dots = document.querySelectorAll("#detail .pictures .small-pic .images .manual-dots a");

$(function(){
    console.log(smallImages)
    
    smallImages.forEach(elem => {
        let activeImage = document.querySelector(".small-pic-item .item .small-pic-active");

        elem.addEventListener("click",function(e){

            smallImages.forEach(element => {
                element.classList.remove("small-pic-active")
            });
            elem.classList.add("small-pic-active")
        })
    });

    dots.forEach(elem => {
        let activeImage = document.querySelector("#detail .pictures .small-pic .images .manual-dots .active");

        elem.addEventListener("click",function(e){

            dots.forEach(element => {
                element.classList.remove("active")
            });
            elem.classList.add("active")
        })
    });
})

let changeImg = window.innerWidth;

window.onresize = function(){
    changeImg = window.innerWidth;
    if(changeImg > 992){
        $("#detail .owl-stage").css("transform","translate3d(0px,0px,0px)")
    }
}

//var scrollSpy = new bootstrap.ScrollSpy(document.body, {
//    target: '#detail'
//})

$("#detail .details form .size .size-list .size-item .detail-size-button").first().addClass("detail-size-active");

$("#detail .details form .size .size-list .size-item label").click(function (element) {
    // $(this).children().removeClass("detail-size-active");
    $(".detail-size-button").removeClass("detail-size-active")
    $(this).parent().addClass("detail-size-active")
    console.log(element)
})


$(function () {

    $(document).on("click", ".load-comment", function (e) {

        e.preventDefault();

        fetch('')

    })
})