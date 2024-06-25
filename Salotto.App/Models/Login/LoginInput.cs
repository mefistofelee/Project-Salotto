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
    public class LoginInput
    {
        /// <summary>
        /// The name of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password (clear text) as the user typed it 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Whether the user intends to stay connected
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Originally requested URL
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}