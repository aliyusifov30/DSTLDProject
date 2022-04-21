
let resultBoxTitle = document.querySelector(".result-box-title");
let searchInput = document.querySelector("#search form .search-input")


$(function () {

    searchInput.addEventListener("keyup", function () {

        let word = $(this).val()

        fetch('https://localhost:44364/search/searching?word=' + word)
            .then(response => response.text())
            .then(data => {
                var s = document.querySelector("#search .result-box .row");
                s.innerHTML = "";
                s.innerHTML = data;
                console.log(data);
            })

        $("#search .result-of-search").css("display", "block")

        if (searchInput.value == "") {
            $("#search .result-of-search").css("display", "none")
        }

    })

})





let grids = document.querySelectorAll("#grids .grid");

grids.forEach(element => {
    
    element.addEventListener("click",function(){

        let gridActived = document.querySelector(".grid-active");
        gridActived.classList.remove("grid-active");
        element.firstElementChild.classList.add("grid-active");
        let collectionCards = document.querySelectorAll("#collection-card .col-sep");

        if(element.classList.contains("grid-2x2")){
            collectionCards.forEach(element => {
                element.classList.remove("col-lg-3")
                element.classList.add("col-lg-6")
                element.style.transition = "0.5s";
            });
        }else{
            collectionCards.forEach(element => {
                element.classList.remove("col-lg-6")
                element.classList.add("col-lg-3");
                element.style.transition = "0.5s";
            });
        }
    })

});
