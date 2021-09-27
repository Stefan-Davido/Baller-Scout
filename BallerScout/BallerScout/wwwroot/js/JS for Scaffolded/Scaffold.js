
// Upload Profile Photo
$("#uploadPhoto").click(function () {
    var data = new FormData();
    var files = $("#photoUpload").get(0).files;

    if (files.length > 0) {
        data.append("UploadedImage", files[0]);
        console.log(data);
    }

    $.ajax({
        type: "POST",
        url: "/Photo/UploadPhoto",
        data: data,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data.dbPath);
            $("#PhotoURL").val(data.dbPath);
            console.log("Work")
        },
        error: function () {
            alert("Error Uploading Photo!");
        }
    });
});