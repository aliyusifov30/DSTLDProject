
$(function () {

    //Add Basket
    $(document).on("click", ".add-basket", function (e) {
        e.preventDefault();

        let id = $(this).attr("data-id");

        let sizeIdStr = $("#detail .size ul li .detail-size-active input")
        let sizeId = sizeIdStr.attr("data-sizeId")

        let colorIdStr = $(".colors");
        let colorId = colorIdStr.attr("data-colorId");

        if (colorId == undefined) {
            let addCartSize = $(this).attr("data-sizeId");
            let addCartColor = $(this).attr("data-colorId");

            fetch('/product/addbasket?id=' + id + '&sizeId=' + addCartSize + '&colorId=' + addCartColor)
                .then(response => response.text())
                .then(data => {
                    var s = document.querySelector(".header-side-bar-card");
                    s.innerHTML = "";
                    s.innerHTML = data;
                })

        } else {
            fetch('/product/addbasket?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId)
                .then(response => response.text())
                .then(data => {

                    var s = document.querySelector(".header-side-bar-card");
                    s.innerHTML = "";
                    s.innerHTML = data;
                })
        }

        $(".header-side-bar-card").removeClass("animate__animated animate__fadeOutRightBig")
        $(".header-side-bar-card").addClass("animate__animated animate__fadeInRightBig animate__fast")
        $(".header-side-bar-card").css("transform", "translate(0px, 0px)");

        //Part is up
        setTimeout(function () {
            $(".header-side-bar-card .checkout-side").removeClass("animate__animated animate__fadeOutDown")
            $(".header-side-bar-card .checkout-side").addClass("animate__animated animate__fadeInUp")
            $(".header-side-bar-card .checkout-side").css("transform", "translate(0px, 0px)");
        }, 300);

        $(".background-black").css("display", "block");
        $(".background-black").css("height", document.body.clientHeight + "px");
        $(".navbar").css("background", "white");
    })

    //Remove Item Basket
        //Plus

    $(document).on("click", ".basket-plus", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");

        let sizeId =  $(this).attr("data-sizeId")

        let colorId = $(this).attr("data-colorId")

        fetch('/product/addbasket?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId)
            .then(response => response.text())
            .then(data => {
                var s = document.querySelector(".header-side-bar-card");
                //s.innerHTML = "";
                s.innerHTML = data;
            })

    })

    $(document).on("click", ".basket-minus", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");

        let sizeId = $(this).attr("data-sizeId")
        let colorId = $(this).attr("data-colorId")

        fetch('/product/deletebasketitem?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId)
            .then(response => response.text())
            .then(data => {
                var s = document.querySelector(".header-side-bar-card");
                //s.innerHTML = "";
                s.innerHTML = data;
            })
    })

    $(document).on("click", ".remove-basket-item", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");

        let sizeId = $(this).attr("data-sizeId")
        let colorId = $(this).attr("data-colorId")
        let removeCheck = $(this).attr("data-check")

        fetch('/product/deletebasketitem?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId + '&check=' + removeCheck)
            .then(response => response.text())
            .then(data => {
                var s = document.querySelector(".header-side-bar-card");
                //s.innerHTML = "";
                s.innerHTML = data;
            })
    })

    //BasketCount

    // Wish List

    //let addWishList = document.querySelectorAll(".add-wishlist")

    //addWishList.forEach(function (e)
    //{
    //    e.addEventListener("click", function (element)
    //    {
    //        element.preventDefault();
    //        console.log(this)
    //        this.parentElement.classList.add("wishlist-active");

    //        let productId = this.getAttribute("data-id");
    //        let colorId = this.getAttribute("data-colorId");

    //        let favSide = this.parentElement.parentElement.parentElement;

    //        fetch('/account/addwishlist?id=' + productId + "&colorId=" + colorId)
    //        .then(response => response.text())
    //            .then(data => {
    //                favSide.innerHTML = "";
    //                favSide.innerHTML = data;
    //        })

    //    })
    //})

    $(document).on("click", ".add-wishlist", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");
        let colorId = $(this).attr("data-colorId")
        $(this).parent().toggleClass("wishlist-active")
        console.log(colorId)

        console.log("bu this : ")
        console.log($(this))

        fetch('/account/addwishlist?id=' + id + "&colorId=" + colorId)
            .then(response => response.text())
            .then(data => {
                console.log(data);
                $(this).parent().parent().parent().html(data);
            })
    })

    //let removeWishList = document.querySelectorAll(".remove-wishlist");

    //removeWishList.forEach(function (e) {

    //    e.addEventListener("click", function (element) {
    //        element.preventDefault();

    //        this.parentElement.classList.add("wishlist-active");
    //        let productId = this.getAttribute("data-id");
    //        let colorId = this.getAttribute("data-colorId");
    //        let favSide = this.parentElement.parentElement.parentElement;

    //        if (favSide == undefined) {
    //            fetch('/account/RemoveWishList?id=' + id + "&colorId=" + colorId)
    //                .then(response => response.text())
    //                .then(data => {
    //                    window.location.reload();
    //                })
    //        } else {
    //            fetch('/account/RemoveWishList?id=' + productId + '&colorId=' + colorId)
    //                .then(response => response.text())
    //                .then(data => {
    //                    favSide.innerHTML = "";
    //                    favSide.innerHTML = data;
    //                })
    //        }

    //    })
    //})


    $(document).on("click", ".remove-wishlist", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");
        let colorId = $(this).attr("data-colorId")

        $(this).parent().removeClass("wishlist-active")
        

        let favSide = document.querySelector("#collection-card .card .fav-side")

        if (favSide == undefined) {
            fetch('/account/RemoveWishList?id=' + id + "&colorId=" + colorId)
                .then(response => response.text())
                .then(data => {
                    window.location.reload();
                })
        }else {
            fetch('/account/RemoveWishList?id=' + id + '&colorId=' + colorId)
                .then(response => response.text())
                .then(data => {
                    console.log("asdad")
                    console.log(data);
                    console.log(favSide);
                    console.log();
                    $(this).parent().parent().parent().html(data);
                })
        }
    })

    //// Add Cart 
    $(document).on("click", ".man-table-item .cart-plus", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");
        let sizeId = $(this).attr("data-sizeId")
        let colorId = $(this).attr("data-colorId")

        fetch('/product/addcart?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId)
            .then(response => response.text())
            .then(data => {
                let body = document.querySelector(".cart-main");
                body.innerHTML = ""
                body.innerHTML = data;
            })
    })

    $(document).on("click", ".man-table-item .cart-minus", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");

        let sizeId = $(this).attr("data-sizeId")

        let colorId = $(this).attr("data-colorId")


        fetch('/product/removecart?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId)
            .then(response => response.text())
            .then(data => {
                let body = document.querySelector(".cart-main");
                body.innerHTML = ""
                body.innerHTML = data;
            })
    })

    $(document).on("click", ".man-table-item .remove-basket-item", function (e) {

        e.preventDefault();

        let id = $(this).attr("data-id");

        let sizeId = $(this).attr("data-sizeId")
        let colorId = $(this).attr("data-colorId")
        let removeCheck = $(this).attr("data-check")

        fetch('/product/RemoveCart?id=' + id + '&sizeId=' + sizeId + '&colorId=' + colorId + '&check=' + removeCheck)
            .then(response => response.text())
            .then(data => {
                let body = document.querySelector(".cart-main");
                body.innerHTML = ""
                body.innerHTML = data;
            })
    })
})
