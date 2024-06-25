using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.DomainModel.Activity
{
    public class UserEditionBinding : SalottoBaseEntity
    {
        #region PRIMARY KEYS

        public long Id { get; set; }

        #endregion

        #region FOREIGN KEYS

        public long UserId { get; set; }
        public User User { get; set; }

        public long EditionId { get; set; }
        public Edition Edition { get; set; }

        #endregion
    }
}
