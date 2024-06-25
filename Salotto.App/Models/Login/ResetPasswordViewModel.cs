///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//




namespace Salotto.App.Models.Login
{
    /// <summary>
    /// The input model type used to reset password
    /// </summary>
    public class ResetPasswordViewModel : SimpleViewModelBase
    {
        /// <summary>
        /// Email of the user who wants to reset
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password token reset
        /// </summary>
        public string Token { get; set; }
    }
}