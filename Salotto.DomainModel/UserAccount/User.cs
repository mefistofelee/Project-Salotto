using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Salotto.DomainModel.UserAccount
{
    public partial class User : SalottoBaseEntity
    {
        public User()
        {

        }

        #region PrimaryKey
        public long UserId { get; set; }
        #endregion

        #region ForeignKeys
        [MaxLength(50)]
        public string Role { get; set; }
        public SalottoRole RoleInfo { get; set; }
        #endregion

        #region User properties
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? PasswordSetOn { get; set; }
        [MaxLength(500)]
        public string SetPasswordToken { get; set; }
        public DateTime? PasswordResetRequest { get; set; }
        [MaxLength(500)]
        public string PasswordResetToken { get; set; }
        public DateTime? LatestPasswordChange { get; set; }
        /// <summary>
        /// If the member is currently locked out of the system
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Reason she's been locked out
        /// </summary>
        [MaxLength(100)]
        public string LockedMessage { get; set; }
        #endregion

        #region Permission properties
        [MaxLength(500)]
        public string SerializedPermissions { get; set; }
        #endregion

        #region Profile properties
        /// <summary>
        /// Headshot of the applicant (URL)
        /// </summary>
        public string PhotoUrl { get; set; }
        #endregion
    }
}
