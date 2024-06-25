using Salotto.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Services.Security;

namespace Salotto.DomainModel.UserAccount
{
    /// <summary>
    /// Methods on the PERSONA entity
    /// </summary>
    public partial class User
    {
        private readonly IPasswordService _password = new DefaultPasswordService();

        /// <summary>
        /// Check whether persona has given permission in the system
        /// Permission is a single letter (eg, P or H, etc)
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public bool HasPermission(string encoded)
        {
            if (SerializedPermissions.IsNullOrWhitespace())
                return true;

            return !encoded.IsNullOrWhitespace() && SerializedPermissions.ContainsAny(encoded);
        }

        /// <summary>
        /// Returns the time since last password change
        /// </summary>
        /// <returns></returns>
        public (TimeSpan Period, string Description) SinceLastPasswordChange()
        {
            if (!LatestPasswordChange.HasValue)
                return (TimeSpan.MinValue, AppMessages.System_InfoNotAvailable);

            var days = Math.Abs((DateTime.UtcNow.Date - LatestPasswordChange.Value.Date).Days);
            var dayText = days switch
            {
                0 => AppStrings.Text_Today,
                1 => AppStrings.Text_Yesterday,
                _ => $"{days} {AppStrings.Text_DayPlural} {AppStrings.Text_Ago}"
            };
            return (DateTime.UtcNow.Date - LatestPasswordChange.Value.Date, dayText);
        }

        /// <summary>
        /// Whether the password token for this user is still valid
        /// </summary>
        /// <returns></returns>
        public bool PasswordResetTokenValid(int secs)
        {
            var maxTime = PasswordResetRequest.GetValueOrDefault().AddSeconds(secs);
            return maxTime >= DateTime.UtcNow;
        }

        /// <summary>
        /// Whether the instance has enough data
        /// </summary>
        /// <returns></returns>
        public bool IsValidState()
        {
            return Email.IsValidEmail() &&
                   (!FirstName.IsNullOrWhitespace() || !LastName.IsNullOrWhitespace());
        }

        /// <summary>
        /// Display name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public bool IsSystem()
        {
            return Role == SalottoRole.System;
        }

        public bool IsAdmin()
        {
            return Role == SalottoRole.Admin;
        }

        public string GetPhotoUrl()
        {
            return $"data:image/jpeg;base64,{PhotoUrl}";
        }
    }
}
