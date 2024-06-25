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
    public class UserEditionBindingRepository
    {
        #region WRITE

        public CommandResponse Add(long editionId, long userId)
        {
            using var db = new SalottoDatabase();

            var edition = (from e in db.Editions where e.Id == editionId select e).FirstOrDefault();

            var bookingsCount = (from b in db.UserEditionBindings where !b.Deleted && b.Active && b.EditionId == editionId select b).Count();

            if (edition.Capacity <= bookingsCount)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_MaxCapacityReached);

            var binding = new UserEditionBinding()
            {
                UserId = userId,
                EditionId = editionId,
            };

            try
            {
                db.UserEditionBindings.Add(binding);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(binding.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        public CommandResponse Remove(long editionId, long userId)
        {
            using var db = new SalottoDatabase();
            var found = (from b in db.UserEditionBindings where !b.Deleted && b.Active && b.UserId == userId && b.EditionId == editionId select b).FirstOrDefault();

            try
            {
                found.Deleted = true;
                found.Active = false;
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(found.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        #endregion

        #region READ

        public List<UserEditionBinding> AllByUser(long userId)
        {
            using var db = new SalottoDatabase();
            var list = (from b in db.UserEditionBindings where !b.Deleted && b.Active && b.UserId == userId select b).ToList();
            return list;
        }
        
        #endregion
    }
}
