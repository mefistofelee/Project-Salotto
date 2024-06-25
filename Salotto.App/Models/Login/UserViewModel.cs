///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;
using Salotto.DomainModel.UserAccount;
using System.Collections.Generic;

namespace Salotto.App.Models.Login
{
    /// <summary>
    /// The model for editing the user profile
    /// </summary>
    public class UserViewModel : MainViewModelBase
    {
        public UserViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {
            IsEdit = false;
        }
        public bool IsEdit { get; set; }

        /// <summary>
        /// User record
        /// </summary>
        public User RelatedUser { get; set; }

        public List<SalottoRole> Roles { get; set; }
    }
}