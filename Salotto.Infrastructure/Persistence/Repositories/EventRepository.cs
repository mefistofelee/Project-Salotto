using Microsoft.Extensions.Logging;
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
    public class EventRepository
    {
        #region READ

        public List<Event> All()
        {
            using var db = new SalottoDatabase();
            var list = (from e in db.Events where !e.Deleted && e.Active select e).ToList();
            return list;
        }

        public Event FindById(long id)
        {
            using var db = new SalottoDatabase();
            var found = (from e in db.Events where !e.Deleted && e.Active && e.Id == id select e).FirstOrDefault();
            return found;
        }

        #endregion

        #region WRITE

        public CommandResponse Save(Event @event, string author)
        {
            using var db = new SalottoDatabase();
            var found = (from e in db.Events where !e.Deleted && e.Active && e.Id == @event.Id select e).FirstOrDefault();

            var isEdit = @event.Id > 0;
            if (isEdit && found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);

            return !isEdit
                ? InsertInternal(@event, db, author)
                : UpdateInternal(@event, found, db, author);
        }

        private CommandResponse UpdateInternal(Event @event, Event found, SalottoDatabase db, string author = null)
        {
            try
            {
                @event.CopyPropertiesTo(found, "CreatedById", "Id");
                found.Mark(isEdit: true, author);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(found.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        private CommandResponse InsertInternal(Event @event, SalottoDatabase db, string author = null)
        {
            try
            {
                @event.Mark(isEdit: false, author);
                db.Events.Add(@event);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(@event.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        public CommandResponse Delete(long id, string author = null)
        {
            using var db = new SalottoDatabase();
            var found = (from e in db.Events where !e.Deleted && e.Active select e).FirstOrDefault();
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
    }
}
