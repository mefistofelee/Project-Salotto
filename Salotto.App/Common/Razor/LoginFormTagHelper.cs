///////////////////////////////////////////////////////////////////
//
// Youbiquitous Martlet: Bootstrap (5)-based Razor tag helpers
// Copyright (c) Youbiquitous 2022
//
// Author: Youbiquitous Team
//


using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Salotto.App.Common.Razor
{
    /// <summary>
    /// Razor tag helper for a Bootstrap5 LOGINFORM element
    /// </summary>
    [HtmlTargetElement("LoginForm1")]
    public class LoginFormTagHelper1 : TagHelper
    {
        public LoginFormTagHelper1()
        {
            Id = "login1";
            BackgroundClass = "card px-5 py-4 shadow rounded-3";
            WelcomeText = "Welcome";
            WelcomeTextClass = "h6 text-center text-gray bold";
            WelcomeSubtext = "Sign in to continue";
            WelcomeSubtextClass = "normal opacity7 small";
            UserFieldName = "username";
            PasswordFieldName = "password";
            ShowRememberMe = true;
            RememberMeText = "Stay connected";
            RememberMeTextClass = "text-gray";
            RememberMeFieldName = "rememberme";
            ShowForgotPassword = true;
            ForgotPasswordText = "Forgot password?";
            UserPlaceholderText = "User name";
            PasswordPlaceholderText = "Password";
            StatusTextClass = "text-warning small";
            SigninButtonClass = "btn px-4 bg-secondary text-white text-uppercase";
            SigninButtonText = "Sign In";
            SigninIcon = "fas fa-chevron-right";
            ForgotPasswordUrl = "#";
            ForgotPasswordTextClass = "text-gray";
            BottomLogoClass = "";
            TakingLongerTextClass = "text-danger small d-none";
            TakingLongerText = "Taking longer than expected";
            BottomText = "";
            BottomTextClass = "text-center tiny pt-1";
        }

        /// <summary>
        /// Topmost welcome text
        /// </summary>
        public string WelcomeText { get; set; }

        /// <summary>
        /// CSS class of the welcome text
        /// </summary>
        public string WelcomeTextClass { get; set; }

        /// <summary>
        /// Topmost welcome sub-text
        /// </summary>
        public string WelcomeSubtext { get; set; }

        /// <summary>
        /// CSS class of the welcome sub-text
        /// </summary>
        public string WelcomeSubtextClass { get; set; }

        /// <summary>
        /// Action URL of the login form
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Return URL
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Base ID of the form
        /// All internal element IDs are based on this Id-xxx
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the user field (property name of the INPUT)
        /// </summary>
        public string UserFieldName { get; set; }

        /// <summary>
        /// Placeholder of the USER element
        /// </summary>
        public string UserPlaceholderText { get; set; }

        /// <summary>
        /// Name of the password field (property name of the INPUT)
        /// </summary>
        public string PasswordFieldName { get; set; }

        /// <summary>
        /// Placeholder of the PASSWORD element
        /// </summary>
        public string PasswordPlaceholderText { get; set; }

        /// <summary>
        /// Whether teh element REMEMBER-ME should be displayed
        /// </summary>
        public bool ShowRememberMe { get; set; }

        /// <summary>
        /// Text of the REMEMBER-ME element
        /// </summary>
        public string RememberMeText { get; set; }

        /// <summary>
        /// CSS class of the REMEMBER-ME element
        /// </summary>
        public string RememberMeTextClass { get; set; }

        /// <summary>
        /// Name of the remember-me field (property name of the INPUT)
        /// </summary>
        public string RememberMeFieldName { get; set; }

        /// <summary>
        /// HTML color for the background of the switch
        /// </summary>
        public string RememberMeBackground { get; set; }

        /// <summary>
        /// Initial status of the remember-me switch
        /// </summary>
        public bool RememberMeChecked { get; set; }

        /// <summary>
        /// Whether to show the forgot-password section
        /// </summary>
        public bool ShowForgotPassword { get; set; }

        /// <summary>
        /// Remember-me text
        /// </summary>
        public string ForgotPasswordText { get; set; }

        /// <summary>
        /// CSS class of the forgot-password text
        /// </summary>
        public string ForgotPasswordTextClass { get; set; }

        /// <summary>
        /// URL to recover the password
        /// </summary>
        public string ForgotPasswordUrl { get; set; }

        /// <summary>
        /// Logo URL
        /// </summary>
        public string BottomLogoUrl { get; set; }

        /// <summary>
        /// CSS class for the logo image
        /// </summary>
        public string BottomLogoClass { get; set; }

        /// <summary>
        /// Caption of the sign-in button
        /// </summary>
        public string SigninButtonText { get; set; }

        /// <summary>
        /// CSS class of the sign-in button
        /// </summary>
        public string SigninButtonClass { get; set; }

        /// <summary>
        /// FA icon of the sign-in button
        /// </summary>
        public string SigninIcon { get; set; }

        /// <summary>
        /// CSS class of status messages
        /// </summary>
        public string StatusTextClass { get; set; }

        /// <summary>
        /// Message to show when the connection is taking longer
        /// </summary>
        public string TakingLongerText { get; set; }

        /// <summary>
        /// CSS class of the taking-longer message
        /// </summary>
        public string TakingLongerTextClass { get; set; }

        /// <summary>
        /// Text to show below the logo
        /// </summary>
        public string BottomText { get; set; }

        /// <summary>
        /// CSS class of the bottom text
        /// </summary>
        public string BottomTextClass { get; set; }

        /// <summary>
        /// Background CSS class for the login form container
        /// </summary>
        public string BackgroundClass { get; set; }

        /// <summary>
        /// HTML renderer
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Outermost element
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "row justify-content-center");
            output.Attributes.Add("id", Id);

            // 1st level DIV
            output.Content.AppendHtml($"<div class=\"col-12 col-md-10 col-xl-6 {BackgroundClass}\">");

            // Welcome box
            output.Content.AppendHtml($"<div class=\"mb-3 pb-2 border-bottom {WelcomeTextClass}\"><span>{WelcomeText}</span><span class=\"ms-3 {WelcomeSubtextClass}\">{WelcomeSubtext}</span></div>");

            // Form and internal layout
            output.Content.AppendHtml($"<form id={Id}-form autocomplete=\"off\" method=\"post\" action=\"{ActionUrl}\">");
            output.Content.AppendHtml($"<input type=\"hidden\" name=\"returnurl\" value=\"{ReturnUrl}\" />");

            // Container
            output.Content.AppendHtml("<div class=\"container-fluid\">");

            // INPUT area (UserName/Password/RememberMe)
            output.Content.AppendHtml("<div class=\"row\"><div class=\"col-12\"><div class=\"mt-2\">");

            // User input group
            output.Content.AppendHtml($"<div class=\"input-group\">");
            output.Content.AppendHtml("<span class=\"input-group-text\"><i class=\"fas fa-user\"></i></span>");
            output.Content.AppendHtml($"<input type=\"text\" name=\"{UserFieldName}\" id=\"{Id}-username\" class=\"form-control\" data-click-on-enter=\"#{Id}-trigger\" autocomplete=\"none\" placeholder=\"{UserPlaceholderText}\">");
            output.Content.AppendHtml("</div>");

            // Password input group
            output.Content.AppendHtml($"<div class=\"input-group mt-2\">");
            output.Content.AppendHtml("<span class=\"input-group-text\"><i class=\"fas fa-key\"></i></span>");
            output.Content.AppendHtml($"<input type=\"password\" name=\"{PasswordFieldName}\" id=\"{Id}-password\" class=\"form-control\" data-click-on-enter=\"#{Id}-trigger\" data-cleartext=\"off\" autocomplete=\"none\" placeholder=\"{PasswordPlaceholderText}\">");
            output.Content.AppendHtml("</div>");

            // RememberMe area
            if (ShowRememberMe)
            {
                var checkedStatus = RememberMeChecked ? "checked" : "";
                output.Content.AppendHtml($"<div class=\"row align-items-center mt-3\"><div class=\"col-12\">");
                output.Content.AppendHtml("<div class=\"form-check form-switch\">" +
                                          $"<input type=\"checkbox\" class=\"form-check-input\" id=\"{Id}-rememberme\" name=\"{RememberMeFieldName}\" {checkedStatus}>" +
                                          $"<label class=\"form-check-label {RememberMeTextClass}\" for=\"{Id}\">{RememberMeText}</label></div>");
                if (!string.IsNullOrWhiteSpace(RememberMeBackground))
                {
                    var style = "<style>.form-check-input:checked{" + $"background-color:{RememberMeBackground};" +
                                $"border-color:{RememberMeBackground};" + "}</style>";
                    output.Content.AppendHtml(style);
                }
                output.Content.AppendHtml("</div></div>");
            }
            output.Content.AppendHtml("</div></div></div>");

            // STATUS area (message and button)
            var icon = string.IsNullOrWhiteSpace(SigninIcon) ? "" : $"<i class=\"{SigninIcon} ms-2\"></i>";
            var caption = $"{SigninButtonText}{icon}".Trim();
            output.Content.AppendHtml($"<div class=\"row mt-2\">");
            output.Content.AppendHtml($"<div class=\"col-7 mt-2 text-end\">");
            output.Content.AppendHtml($"<div id=\"{Id}-status\" class=\"px-2 {StatusTextClass}\"></div>");
            output.Content.AppendHtml($"<div id=\"{Id}-takinglonger\" class=\"px-2 {TakingLongerTextClass}\">{TakingLongerText}</div></div>");
            output.Content.AppendHtml($"<div class=\"col-5 text-end\"><button type=\"button\" id=\"{Id}-trigger\" class=\"{SigninButtonClass}\">{caption}</button></div>");
            output.Content.AppendHtml("</div>");

            // Close 1st row container
            //output.Content.AppendHtml("</div></div></div>");
            output.Content.AppendHtml("</div>");
            output.Content.AppendHtml("</form>");

            // 2nd row (Forgot Password)
            if (ShowForgotPassword)
            {
                output.Content.AppendHtml("<div class=\"small mt-3 text-center pt-2 border-top\">");
                output.Content.AppendHtml($"<a class=\"text-decoration-none {ForgotPasswordTextClass}\" href=\"{ForgotPasswordUrl}\">{ForgotPasswordText}</a>");
                output.Content.AppendHtml("</div>");
            }

            // Logo
            if (!string.IsNullOrWhiteSpace(BottomLogoUrl))
            {
                output.Content.AppendHtml("<div class=\"mt-4 text-center\">");
                output.Content.AppendHtml($"<img src=\"{BottomLogoUrl}\" class=\"{BottomLogoClass}\" style=\"width: 100px\" />");
                output.Content.AppendHtml("</div>");
                output.Content.AppendHtml($"<div class=\"{BottomTextClass}\">{BottomText}</div>");
            }

            // Close container
            output.Content.AppendHtml("</div>");

            // Close outermost element
            output.Content.AppendHtml("</div>");
        }
    }
}
