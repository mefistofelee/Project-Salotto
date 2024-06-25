using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.DomainModel.Activity
{
    public class Event : SalottoBaseEntity
    {
        #region PRIMARY KEYS

        public long Id { get; set; }

        #endregion

        #region FOREIGN KEYS

        public long CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        #endregion

        #region PROPERTIES

        public string Name { get; set; }
        public string Description { get; set; }

        #endregion
    }
}
