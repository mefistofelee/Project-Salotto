
//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the EDIT COMPANY buttons
//
$("#trigger-company-save").click(function() {
    var button = $(this);
    var overlay = $("#overlay");
    var name = $("#nm").val();
    var id = $("#id").val();

    // Validate form content
    if (name.length === 0) {
        $("#message").fail(Err_CompanyNameRequired).hideAfter(4);
        return;
    }

    // Post changes
    button.spin();
    overlay.overlayOn();
    Ybq.postForm("#company-form",
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
                __refreshViewAfterChanges();
                $.confirm({
                    title: "CASPIO",
                    content: response.message,
                    type: "green",
                    columnClass: 'col-6',
                    buttons: {
                        stayHere: {
                            text: Menu_StayOnPage,
                            action: function() {
                                // window.location.reload();
                            }
                        },
                        goto: {
                            text: Menu_BackHome,
                            btnClass: "btn-primary",
                            action: function() {
                                Ybq.goto("/admin/corps/");
                            }
                        }
                    }
                });

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
    $("#page-title-content").html($("#nm").val());
}