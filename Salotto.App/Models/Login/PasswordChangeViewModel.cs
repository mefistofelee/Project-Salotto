///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Models.Login
{
    /// <summary>
    /// The model for a change-password action
    /// </summary>
    public class PasswordChangeViewModel
    {
        public long UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }

        /// <summary>
        /// Check input data is acceptable
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return UserId > 0 &&
                   !OldPassword.IsNullOrWhitespace() &&
                   NewPassword != null &&
                   NewPassword == RepeatPassword;
        }
    }
}