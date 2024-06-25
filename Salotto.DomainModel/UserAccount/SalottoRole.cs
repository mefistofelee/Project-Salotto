using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.DomainModel.UserAccount
{
    public class SalottoRole : SalottoBaseEntity
    {
        public SalottoRole()
        {

        }

        /// <summary>
        /// Id of role
        /// </summary>
        [MaxLength(50)]
        public string Role { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }


        /// <summary>
        /// Read permissions
        /// </summary>
        [MaxLength(500)]
        public string R_Permissions { get; set; }
        /// <summary>
        /// Update Permissions
        /// </summary>
        [MaxLength(500)]
        public string U_Permissions { get; set; }

        #region Supported Roles
        public const string System = "System";
        public const string Admin = "Admin";
        public const string Standard = "Standard";
        #endregion
    }
}
