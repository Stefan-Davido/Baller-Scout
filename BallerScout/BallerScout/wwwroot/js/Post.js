// Upload Photo For Post

$("#uploadPhoto").click(function () {
    var data = new FormData();
    var files = $("#photoUpload").get(0).files;

    if (files.length > 0) {
        data.append("UploadedImage", files[0]);
        console.log(data);
    }

    $.ajax({
        type: "POST",
        url: "/Post/UploadPhoto",
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
})

function clickLike(postIdParam) {
    var data = postIdParam;

    $.ajax({
        type: "POST",
        url: "/Post/Like",
        data: { postId: data },
        success: function (data) {
            console.log("Work");
            console.log(data);
        },
        error: function () {
            console.log("Work");
        }
    });
}

function savePost(postIdParam) {
    var data = postIdParam;

    $.ajax({
        type: "POST",
        url: "/Post/SavePost",
        data: { postId: data },
        success: function (data) {
            console.log("Work");
            console.log(data);
        },
        error: function () {
            console.log("Work");
        }
    });
}

// video
