using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.App.Common.Settings;
using Salotto.App.Models.System;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System.Linq;
using Salotto.DomainModel.Activity;
using Salotto.App.Models.Activities;
using Microsoft.AspNetCore.Http;
using System;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.IO;
using Youbiquitous.Martlet.Core.Types;

namespace Salotto.App.Application
{
    public class EventService : ApplicationServiceBase
    {
        private readonly EventRepository _eventRepository;

        public EventService(AppSettings settings) : base(settings)
        {
            _eventRepository = new();
        }

        public EventViewModel GetEventViewModel(long id, string permissions)
        {
            var model = new EventViewModel(permissions, Settings)
            {
                Event = id > 0 ? _eventRepository.FindById(id) : new Event(),
            };
            return model;
        }

        public EventsViewModel GetEventsViewModel(string permissions)
        {
            var model = new EventsViewModel(permissions, Settings)
            {
                Events = _eventRepository.All(),
            };
            return model;
        }

        public CommandResponse Save(Event @event, string author = null)
        {
            var response = _eventRepository.Save(@event, author);
            return response;
        }

        public CommandResponse Delete(long id, string author = null)
        {
            var response = _eventRepository.Delete(id, author);
            return response;
        }
    }
}
