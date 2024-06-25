using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.Infrastructure.Persistence.Repositories
{
    public class CardRepository
    {
        #region READ

        public Card FindByUserId(long userId)
        {
            using var db = new SalottoDatabase();
            var found = (from c in db.Cards where !c.Deleted && c.Active && c.UserId == userId select c).FirstOrDefault();
            return found;
        }

        public Card FindById(long id)
        {
            using var db = new SalottoDatabase();
            var found = (from c in db.Cards where !c.Deleted && c.Active && c.Id == id select c).FirstOrDefault();
            return found;
        }

        #endregion

        #region WRITE
        #endregion
    }
}
