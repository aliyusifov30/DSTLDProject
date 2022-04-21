

let height = document.body.offsetHeight;

console.log("Height")
console.log(height)

$(function(){
    $(document).on("click",".header-side-bar-card-icon",function(e){
        e.preventDefault();
        //Section is coming 
        $(".header-side-bar-card").removeClass("animate__animated animate__fadeOutRightBig")
        $(".header-side-bar-card").addClass("animate__animated animate__fadeInRightBig animate__fast")
        $(".header-side-bar-card").css("transform","translate(0px, 0px)");

        //Part is up
        setTimeout(function () {
            $(".header-side-bar-card .checkout-side").removeClass("animate__animated animate__fadeOutDown")
            $(".header-side-bar-card .checkout-side").addClass("animate__animated animate__fadeInUp")
            $(".header-side-bar-card .checkout-side").css("transform","translate(0px, 0px)");
        }, 300);

        $(".background-black").css("display","block");
        $(".background-black").css("height",document.body.clientHeight + "px");
        $(".navbar").css("background","white");
    })

    $(document).on("click",".header-side-bar-card-shopping-btn",function(e){
        e.preventDefault();
        //Section is coming 
        $(".header-side-bar-card").removeClass("animate__animated animate__fadeOutRightBig")
        $(".header-side-bar-card").addClass("animate__animated animate__fadeInRightBig animate__fast")
        $(".header-side-bar-card").css("transform","translate(0px, 0px)");

        setTimeout(function () {

            //Part is up
            $(".header-side-bar-card .checkout-side").removeClass("animate__animated animate__fadeOutDown")
            $(".header-side-bar-card .checkout-side").addClass("animate__animated animate__fadeInUp")
            $(".header-side-bar-card .checkout-side").css("transform","translate(0px, 0px)");
        }, 300);

        $(".background-black").css("display","block");
        $(".background-black").css("height",document.body.clientHeight + "px");
        $(".navbar").css("background","white");
    })

    $(document).on("click",".header-side-bar-card-x-btn",function(e){

        e.preventDefault();
        //Part is down
        $(".header-side-bar-card .checkout-side").removeClass("animate__animated animate__fadeInUp")
        $(".header-side-bar-card .checkout-side").addClass("animate__animated animate__fadeOutDown")
       
        setTimeout(function () {
            //Go to back
            $(".header-side-bar-card").removeClass("animate__animated animate__fadeInRightBig animate__fast")
            $(".header-side-bar-card").addClass("animate__animated animate__fadeOutRightBig")
        }, 300);

        $(".background-black").css("display","none");
        $(".background-black").css("height",height + "0px");

    })
})

