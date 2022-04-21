$(function () {

    $(document).on("click", "#shop-on-instagram .item", function () {

        let id = $(this).attr("data-id");

        fetch(`/home/getInstagramData/${id}`)
            .then(response => response.text())
            .then(data => {
                $("#shop-on-instagram-modal .modal-content").html(data)
            })
        $("#shop-on-instagram-modal").modal("show")
    })
})