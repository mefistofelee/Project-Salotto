///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


// JS for the LOGIN page

//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the SIGN-IN button
//
$("#login-trigger").click(function() {
    var userRef = "#login-username";
    var pswdRef = "#login-password";
    var formRef = "#login-form";
    var msgRef = "#login-status";
    var tooLongRef = "#login-takinglonger";

    var user = $(userRef).val();
    var pswd = $(pswdRef).val();

    if (user.length === 0 || pswd.length === 0) {
        $(msgRef).html(Err_IncompleteCredentials);
        return;
    }

    $(msgRef).html(Info_Connecting + "...").spin();
    var loginTimer = setTimeout(function() {
            $(tooLongRef).removeClass("d-none");
        },
        3000);

    Ybq.postForm(formRef,
        function(data) {
            var response = "";
            try {
                response = JSON.parse(data);
            } catch (e) {
                clearTimeout(loginTimer);
                $(msgRef).html(System_SomethingWentWrong);
                $(tooLongRef).addClass("d-none");
                return;
            };

            if (response.success) {
                Ybq.goto(response.redirectUrl);
            }
            else {
                clearTimeout(loginTimer);
                $(tooLongRef).addClass("d-none");
                $(msgRef).html(response.message);
                $(userRef).val("");
                $(pswdRef).val("");
            }
        },
        function(error) {
            clearTimeout(loginTimer);
            $(tooLongRef).addClass("d-none");
            $(msgRef).html(System_ConnectionError);
        });
});


//////////////////////////////////////////////////////////////////////////////////////
// Displays the login page shared by all users of the system
// 
//$("#login-form input").on("focus",
//    function() {
//        $("#message").html("");
//    });



