function openModal(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (id === undefined || url === undefined) {
        alert("Error");
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: {
            "id": id
        },
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.show()
        },
        failure: function () {
            modal.hide()
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};