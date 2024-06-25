using Salotto.App.Common.Settings;
using Salotto.DomainModel.UserAccount;

namespace Salotto.App.Models.UserAccount
{
    public class SetPasswordViewModel : LandingViewModelBase
    {
        public SetPasswordViewModel(AppSettings settings)
        {
            Settings = settings;
        }

        public User User {  get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
