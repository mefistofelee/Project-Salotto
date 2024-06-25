
//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the EDIT USER ACCOUNT buttons
//
$("#trigger-account-save").click(function() {
    var button = $(this);
    var overlay = $("#overlay");
    var fn = $("#fn").val();
    var ln = $("#ln").val();
    var em = $("#em").val();

    // Validate form content
    if (em.length === 0 || (fn.length === 0 && ln - length === 0)) {
        $("#message").fail(Err_IdentityFieldsRequired).hideAfter(4);
        return;
    }

    // Post changes
    button.spin();
    overlay.overlayOn();
    Ybq.postForm("#useraccount-form",
        function(data) {
            var response = "";
            button.unspin();
            overlay.overlayOff();
            try {
                response = JSON.parse(data);
            } catch (e) {
                $("#message").fail(System_SomethingWentWrong);
                return;
            };

            if (response.success) {
                $("#message").setMsg(Info_SavedChanges, response.success).hideAfter(3);
                __refreshViewAfterChanges();

                //$.confirm({
                //    title: "CASPIO",
                //    content: response.message,
                //    type: "green",
                //    columnClass: 'col-6',
                //    buttons: {
                //        stayHere: {
                //            text: Menu_StayOnPage,
                //            action: function() {
                //                // window.location.reload();
                //            }
                //        },
                //        goto: {
                //            text: Menu_BackHome,
                //            btnClass: "btn-primary",
                //            action: function() {
                //                Ybq.goto("/admin/corps/");
                //            }
                //        }
                //    }
                //});

            }
            else {
                $("#message").fail(response.message).hideAfter(3);
            }
        },
        function(error) {
            overlay.overlayOff();
            button.unspin();
            $("#message").fail(System_ConnectionError);
        });
});



// Update visual elements after the ajax backend change
function __refreshViewAfterChanges() {
    var fn = $("#fn").val();
    var ln = $("#ln").val();
    $("#page-title-content").html(fn + " " + ln);
}