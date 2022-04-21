
let formInput = document.querySelectorAll(".form-input input");

console.log(formInput)
formInput.forEach(element => {
    element.addEventListener("keyup",function(){
        element.nextElementSibling.style.transition = "all 0.4s"
        element.nextElementSibling.style.transform = "translate(13px,-56px)"
        element.nextElementSibling.style.opacity = "1"
        
        if(element.value == ""){
            element.nextElementSibling.style.transition = "all 0.4s"
            element.nextElementSibling.style.transform = "translate(13px,-41px)"
            element.nextElementSibling.style.opacity = "0"
        }
    })
});




