using Salotto.App.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salotto.DomainModel.UserAccount;

namespace Salotto.App.Models.UserAccount
{
    public class UsersViewModel : MainViewModelBase
    {
        public UsersViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public List<User> Users { get; set; }
    }
}
