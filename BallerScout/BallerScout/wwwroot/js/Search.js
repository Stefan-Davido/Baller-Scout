function getSearchString() {
    var string = $("#InputSearchValue").val();
    var data = string;

    $.ajax({
        type: "GET",
        url: "/Search/SearchedUsers",
        data: { searchString: data },
        success: function (data) {
            console.log("Work");
            console.log(data);
        },
        error: function () {
            console.log("Work");
        }
    });
}