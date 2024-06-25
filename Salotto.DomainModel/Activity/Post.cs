using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.DomainModel.Activity
{
    public class Post : SalottoBaseEntity
    {
        #region PRIMARY KEYS

        public long Id { get; set; }

        #endregion

        #region FOREIGN KEYS

        public long CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public long? ApprovedByUserId { get; set; }
        public User ApprovedByUser { get; set; }

        public long? EditionId { get; set; }
        public Edition Edition { get; set; }

        #endregion

        #region PROPERTIES

        public string Photo { get; set; }
        public string Description { get; set; }
        public PostStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }


        #endregion

        #region METHODS

        public string GetDataUrl()
        {
            return $"data:image/jpeg;base64,{Photo}";
        }

        #endregion
    }

    public enum PostStatus
    {
        Pending,
        Approved,
        Denied,
    }
}
