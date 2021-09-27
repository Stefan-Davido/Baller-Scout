// *** Follow & Unfollow ***
function followAndUnfollow(userId) {
    var data = userId;

    $.ajax({
        type: "POST",
        url: "/User/FollowAndUnfollow",
        data: { Id: data },
        success: function (data) {
            console.log("Work");
            console.log(data);
        },
        error: function () {
            console.log("Doesn't Work");
        }
    });
}

// *** Correct style to follow/unfollow button ***

const FOLLOWBTN = document.getElementById("followAndUnfollowBTN");
const followCheck = document.getElementById("nonePTag");
document.addEventListener("DOMContentLoaded", addStyleToButton);
FOLLOWBTN.addEventListener("click", changeFollowStatus);

function addStyleToButton() {
    var followStatus = followCheck.innerText;
    followCheck.style.display = "none";

    if (followStatus.toLowerCase() == "true") {
        FOLLOWBTN.classList.remove("btn-danger");
        FOLLOWBTN.classList.add("btn-success");
        FOLLOWBTN.innerText = "Unfollow";
    }
    else{
        FOLLOWBTN.classList.remove("btn-success");
        FOLLOWBTN.classList.add("btn-danger");
        FOLLOWBTN.innerText = "Follow";
    }
}

var currentStatus;
var Follow = true;
var Unfollow = false;

function changeFollowStatus() {
    var onClick = currentStatus ? Follow : Unfollow;
    swap();
}

function swap() {
    currentStatus = !currentStatus;
    followCheck.innerText = currentStatus;
    addStyleToButton();
}

// *** Number Of Followers ***

function numberOfFollowers(userId) {
    var data = userId;

    $.ajax({
        type: "GET",
        url: "/BallerScout/BallerScout.Service/FollowingService.cs/NumberOfFollowings",
        data: { Id: data },
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("Work");
            console.log(ajax.dataType);
            console.log(data);
            var counter = data;
            console.log(counter);
        },
        error: function () {
            console.log(Error);
        }
    });
}


    