using Microsoft.EntityFrameworkCore;
using PoponGate.ExtensionMethods;
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
    public class EditionRepository
    {
        #region READ

        public List<Edition> All(long eventId = 0)
        {
            using var db = new SalottoDatabase();
            var list = (from e in db.Editions.Include(x => x.Event) where !e.Deleted && e.Active && (e.EventId == eventId || eventId == 0) select e).ToList();
            return list;
        }

        public Edition FindById(long id)
        {
            using var db = new SalottoDatabase();
            var found = (from e in db.Editions where !e.Deleted && e.Active && e.Id == id select e).FirstOrDefault();
            return found;
        }

        #endregion

        #region WRITE

        public CommandResponse Delete(long id, string author = null)
        {
            using var db = new SalottoDatabase();
            var found = (from e in db.Editions where e.Id == id select e).FirstOrDefault();
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

        public CommandResponse Save(Edition edition, string author = null)
        {
            using var db = new SalottoDatabase();
            var found = (from e in db.Editions where !e.Deleted && e.Active && e.Id == edition.Id select e).FirstOrDefault();

            var isEdit = edition.Id > 0;
            if (isEdit && found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);

            return isEdit
                ? UpdateInternal(edition, found, db, author)
                : InsertInternal(edition, db, author);
        }

        private CommandResponse InsertInternal(Edition edition, SalottoDatabase db, string author = null)
        {
            try
            {
                edition.Mark(isEdit: false, author);
                db.Editions.Add(edition);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(edition.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        private CommandResponse UpdateInternal(Edition edition, Edition found, SalottoDatabase db, string author = null)
        {
            edition.CopyPropertiesTo(found, "Id", "EventId", "ApprovedByUserId");
            try
            {
                found.Mark(isEdit: true, author);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(found.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic).AddKey(found.Id.ToString());
            }
        }

        #endregion
    }
}
