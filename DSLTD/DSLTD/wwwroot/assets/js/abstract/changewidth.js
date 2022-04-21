//Change Background Image Section Elements
let firstHomeBackgroundImage = document.querySelector("#first-home .background-image .big-image");
let freshStartBackgroundImage = document.querySelector("#fresh-start .background-image .big-image");
let denimDeliveryBackgroundImage = document.querySelector("#denim-delivery .background-image .big-image")

let buttonChangeRow = document.querySelectorAll(".buttons-change-section .background-image .container .row")
let intViewportWidth = window.innerWidth;


// Change Background Image Section Page Loading
if (intViewportWidth < 992) {
    // firstHomeBackgroundImage.src = "./assets/img/0211-Mobile-v2_1200x1200.gif";
    // freshStartBackgroundImage.src = "./assets/img/0105-Mobilenew_1200x1200.gif";
    // denimDeliveryBackgroundImage.src = "./assets/img/MOBILE_DENIM_HERO_3x_1d8d1316-64a1-4fed-873d-38866f532819_1200x1200.jpg";

    firstHomeBackgroundImage.style.display = "none";
    firstHomeBackgroundImage.nextElementSibling.style.display = "block";

    freshStartBackgroundImage.style.display = "none";
    freshStartBackgroundImage.nextElementSibling.style.display = "block";

    denimDeliveryBackgroundImage.style.display = "none";
    denimDeliveryBackgroundImage.nextElementSibling.style.display = "block";

} else {
    // firstHomeBackgroundImage.src = "./assets/img/0211-Desktop-v2_2000x2000.gif";
    // freshStartBackgroundImage.src = "./assets/img/0105-Desktop_2000x2000.gif";
    // denimDeliveryBackgroundImage.src = "./assets/img/DENIM_HERO_3x_30199e4e-c42d-4704-a5df-bd679265f99e_2000x2000.jpg";

    firstHomeBackgroundImage.style.display = "block";
    firstHomeBackgroundImage.nextElementSibling.style.display = "none";

    freshStartBackgroundImage.style.display = "block";
    freshStartBackgroundImage.nextElementSibling.style.display = "none";

    denimDeliveryBackgroundImage.style.display = "block";
    denimDeliveryBackgroundImage.nextElementSibling.style.display = "none";

}

// Change Buttons Position
if (intViewportWidth < 768) {
    buttonChangeRow.forEach(element => {
        element.classList.add("buttons-down-position")
    });
} else {
    buttonChangeRow.forEach(element => {
        element.classList.remove("buttons-down-position")
    });
}

if (intViewportWidth < 642) {
    denimDeliveryBackgroundImage.nextElementSibling.nextElementSibling.firstElementChild.classList.add("buttons-down-position")
} else {
    denimDeliveryBackgroundImage.nextElementSibling.nextElementSibling.firstElementChild.classList.remove("buttons-down-position")
}

window.onresize = function () {
    intViewportWidth = window.innerWidth;
    // Change Background Image Section
    if (intViewportWidth < 992) {
        // firstHomeBackgroundImage.src = "./assets/img/0211-Mobile-v2_1200x1200.gif";
        // freshStartBackgroundImage.src = "./assets/img/0105-Mobilenew_1200x1200.gif";
        // denimDeliveryBackgroundImage.src = "./assets/img/MOBILE_DENIM_HERO_3x_1d8d1316-64a1-4fed-873d-38866f532819_1200x1200.jpg";

        firstHomeBackgroundImage.style.display = "none";
        firstHomeBackgroundImage.nextElementSibling.style.display = "block";

        freshStartBackgroundImage.style.display = "none";
        freshStartBackgroundImage.nextElementSibling.style.display = "block";

        denimDeliveryBackgroundImage.style.display = "none";
        denimDeliveryBackgroundImage.nextElementSibling.style.display = "block";

    } else {
        // firstHomeBackgroundImage.src = "./assets/img/0211-Desktop-v2_2000x2000.gif";
        // freshStartBackgroundImage.src = "./assets/img/0105-Desktop_2000x2000.gif";
        // denimDeliveryBackgroundImage.src = "./assets/img/DENIM_HERO_3x_30199e4e-c42d-4704-a5df-bd679265f99e_2000x2000.jpg";

        firstHomeBackgroundImage.style.display = "block";
        firstHomeBackgroundImage.nextElementSibling.style.display = "none";

        freshStartBackgroundImage.style.display = "block";
        freshStartBackgroundImage.nextElementSibling.style.display = "none";

        denimDeliveryBackgroundImage.style.display = "block";
        denimDeliveryBackgroundImage.nextElementSibling.style.display = "none";

    }
    // Change Buttons Position
    if (intViewportWidth < 768) {
        // var freshStartRow 
        buttonChangeRow.forEach(element => {
            element.classList.add("buttons-down-position")
        });
    } else {
        buttonChangeRow.forEach(element => {
            element.classList.remove("buttons-down-position")
        });
    }
    //Denim-Deleviry Buttons Change Position
    if (intViewportWidth < 642) {
        denimDeliveryBackgroundImage.nextElementSibling.nextElementSibling.firstElementChild.classList.add("buttons-down-position")
    } else {
        denimDeliveryBackgroundImage.nextElementSibling.nextElementSibling.firstElementChild.classList.remove("buttons-down-position")
    }
}