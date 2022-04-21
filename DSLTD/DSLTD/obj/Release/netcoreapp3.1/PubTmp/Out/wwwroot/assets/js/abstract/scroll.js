let scrollY = window.scrollY;
window.onscroll = function(){

    scrollY = window.scrollY

    if(scrollY > 36){
        
        // $(".top").css("display","none")
        $("header").addClass("sticky");
        $("header .women-megaMenu").addClass("fixed");
        $("header .women-megaMenu").css("top","70px");
    }else{

        // $(".top").css("display","block")
        $("header").removeClass("sticky");
        $("header .women-megaMenu").removeClass("fixed");
    }
}
