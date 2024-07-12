using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.DomainModel.Activity
{
    public class Card : SalottoBaseEntity
    {
        #region PRIMARY KEYS

        public long Id { get; set; }

        #endregion

        #region FOREIGN KEYS

        public long UserId { get; set; }
        public User User { get; set; }

        public long? ApprovedByUserId { get; set; }
        public User ApprovedByUser { get; set; }

        #endregion

        #region PROPERTIES

        public bool IsApproved { get; set; }
        public string Code { get; set; }
        public DateTime ExpireDate { get; set; }

        #endregion
    }
}
