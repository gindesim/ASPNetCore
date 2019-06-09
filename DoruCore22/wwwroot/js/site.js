const uri = "api/coverapi";
let coverSet = null;
function getCount(data) {
    const el = $("#counter");
    let label = "Cover";
    if (data) {
        el.text(data + " " + label);
    } else {
        el.text("No " + label);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#cover");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, value) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(value.series))
                    .append($("<td></td>").text(value.cast))
                    .append($("<td></td>").text(value.releasedate))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editCover(value.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                deleteCover(value.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            coverSet = data;
        }
    });
}

function addCover() {
    const Cover = {
        series: $("#add-series").val(),
        cast: $("#add-cast").val(),
        releasedate: $("#add-releasedate").val()
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(Cover),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#add-releasedate").val("");
        }
    });
}

function deleteCover(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editCover(id) {
    $.each(coverSet, function (key, value) {
        if (value.id === id) {
            $("#edit-series").val(value.series);
            $("#edit-cast").val(value.cast);
            $("#edit-releasedate").val(value.releasedate);
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function () {
    const value = {
        series: $("#add-series").val(),
        cast: $("#add-cast").val(),
        releasedate: $("#add-releasedate").val(),
        id: $("#edit-id").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(value),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}

function saveCover(id) {
    $.ajax({
        url: uri + "/SaveCover",
        type: "GET",
        success: function (result) {
        }
    });
}

function loadCover(id) {
    $.ajax({
        url: uri + "/LoadCover",
        type: "GET",
        success: function (result) {
            getData();
        }
    });
}

function loadFromText(id) {
    $.ajax({
        url: uri + "/LoadFromText",
        type: "GET",
        success: function (result) {
            getData();
            $("#cover").css({ display: "block" });
        }
    });
}
