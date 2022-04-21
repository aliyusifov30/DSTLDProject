

$(function () {

    $(document).on("click", ".policy li a", function () {

        let id = $(this).attr("data-id");

        fetch(`/checkout/getPolicyData/${id}`)
            .then(response => response.text())
            .then(data => {
                $("#PolicyModal .modal-content").html(data)
            })
        $("#PolicyModal").modal("show")
    })
})