using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Types;

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

        public CommandResponse CreatePending(long userId)
        {
            using var db = new SalottoDatabase();
            try
            {
                var newCard = new Card()
                {
                    Code = Guid.NewGuid().ToString(),
                    IsApproved = false,
                    ApprovedByUserId = null,
                    UserId = userId,
                    ExpireDate= DateTime.UtcNow.AddYears(1),
                };
                db.Cards.Add(newCard);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted);
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        #endregion
    }
}
