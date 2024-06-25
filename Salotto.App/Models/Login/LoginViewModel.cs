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
    /// The input model type used to collect login data
    /// </summary>
    public class LoginViewModel : LandingViewModelBase
    {
        public LoginViewModel()
        {
            FormData = new LoginInput();
        }

        /// <summary>
        /// Login form data
        /// </summary>
        public LoginInput FormData { get; set; }
    }
}