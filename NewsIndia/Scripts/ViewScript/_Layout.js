//Used to Show Notification
function showNotification(message, type) {
    $.notify(message, type);
}

$(document).ready(function () {

        //showNotification("Working..", "success");
    //$("#divLoading").show();
});
function ReloadMenuDashboard() {
    $("#divMenuDashboard").load("/Layout/LoadLayoutMenu");
}

function LoadView(val) {
    debugger;
    var link = $(val).attr('href');
    $("#divLoading").show();
    $("#divPartial").load(link, function (response, status, xhr) {
        if (status == "error")
            showNotification("Error while connecting with NewsIndia Server.", "warning");

        $("#divLoading").hide();
    });
    return false;
}