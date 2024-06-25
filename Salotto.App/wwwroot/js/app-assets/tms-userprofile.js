
//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the EDIT PROFILE button
//
$("#trigger-profile-save").click(function() {
    var button = $(this);
    var overlay = $("#overlay");
    var fn = $("#fn").val();
    var ln = $("#ln").val();
    var em = $("#em").val();

    // Validate form content
    if (em.length === 0 || (fn.length === 0 && ln.length === 0)) {
        $("#message-profile").fail(Err_IdentityFieldsRequired).hideAfter(4);
        return;
    }

    // Post changes
    button.spin();
    overlay.overlayOn();
    Ybq.postForm("#profile-form",
        function(data) {
            var response = "";
            button.unspin();
            overlay.overlayOff();
            try {
                response = JSON.parse(data);
            } catch (e) {
                $("#message-profile").fail(System_SomethingWentWrong);
                return;
            };

            if (response.success) {
                $("#message-profile").setMsg(Info_SavedChanges, response.success).hideAfter(3);
                __refreshViewAfterChanges();
            }
            else {
                $("#message-profile").fail(response.message);
            }
        },
        function(error) {
            overlay.overlayOff();
            button.unspin();
            $("#message-pswd").fail(System_ConnectionError);
        });
});

function __refreshViewAfterChanges() {
    $("#lbl_notes").html($("#no").val());
    $("#lbl_email").html($("#em").val());
    $("#lbl_phone").html($("#ph").val());
    $("#lbl_locat").html($("#lo").val());

    var name = $("#fn").val().capitalize() + " " + $("#ln").val().capitalize();
    $("#lbl_dname").html(name);
    $("#logged-user-name").html(name);

    var url = "/headshot/" + $("#userid").val() + "?x=" + Math.random();   
    $("#lbl_photo").attr("src", url);
    $("#logged-user-pic").attr("src", url);
}


//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the CHANGE PASSWORD button
//
$("#trigger-change-password").click(function() {
    var button = $(this);
    var overlay = $("#overlay");
    var op = $("#old-p").val();
    var p1 = $("#new-p1").val();
    var p2 = $("#new-p2").val();

    // Validate form content
    if (op.length === 0 || p1.length === 0 || p2 - length === 0 || p1.length !== p2.length) {
        $("#message-pswd").html(Err_FillAllPasswordFields).hideAfter(4);
        return;
    }

    // Post changes
    overlay.overlayOn();
    button.spin();
    Ybq.postForm("#password-form",
        function(data) {
            var response = "";
            overlay.overlayOff();
            button.unspin();
            try {
                response = JSON.parse(data);
            } catch (e) {
                $("#message-pswd").fail(System_SomethingWentWrong);
                $("#old-p").val("");
                $("#new-p1").val("");
                $("#new-p2").val("");
                return;
            };

            if (response.success) {
                $("#message-pswd").setMsg(response.message, response.success).hideAfter(3); 
                $("#old-p").val("");
                $("#new-p1").val("");
                $("#new-p2").val("");
            }
            else {
                $("#message-pswd").fail(response.message);
                $("#old-p").val("");
                $("#new-p1").val("");
                $("#new-p2").val("");
            }
        },
        function(error) {
            button.unspin();
            overlay.overlayOff();
            $("#message-pswd").fail(System_ConnectionError);
        });
});



