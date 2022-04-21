

let forgot = document.querySelector(".forgot");
let loginSide = document.querySelector(".login");
let recover = document.querySelector(".recover");
let backToLogin = document.querySelector(".backlogin")


forgot.addEventListener("click",function(e){
    e.preventDefault();

    loginSide.classList.remove("animate__animated")
    loginSide.classList.remove("animate__fadeInUp")

    loginSide.classList.add("animate__animated");
    loginSide.classList.add("animate__fadeOutDown");
    loginSide.style.display = "none";

    recover.style.visibility = "visible";
    recover.style.display = "block";

    recover.classList.remove("animate__animated")
    recover.classList.remove("animate__fadeOutDown")

    recover.classList.add("animate__animated");
    recover.classList.add("animate__fadeInUp");
})

backToLogin.addEventListener("click",function(e){

    e.preventDefault();



    recover.style.visibility = "hidden";
    recover.style.display = "none";

    recover.classList.remove("animate__animated")
    recover.classList.remove("animate__fadeInUp")

    recover.classList.add("animate__animated");
    recover.classList.add("animate__fadeOutDown");

    loginSide.classList.remove("animate__animated")
    loginSide.classList.remove("animate__fadeOutDown")

    loginSide.classList.add("animate__animated");
    loginSide.classList.add("animate__fadeInUp");
    loginSide.style.display = "block";


})




