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
    public class EditionService : ApplicationServiceBase
    {
        private readonly EditionRepository _editionRepository;
        private readonly EventRepository _eventRepository;
        private readonly UserEditionBindingRepository _userEditionBindingRepository;

        public EditionService(AppSettings settings) : base(settings)
        {
            _editionRepository = new();
            _eventRepository = new();
            _userEditionBindingRepository = new();
        }

        public CommandResponse Delete(long id, string author = null)
        {
            var response = _editionRepository.Delete(id, author);
            return response;
        }

        public CommandResponse Save(Edition edition, string author = null)
        {
            var response = _editionRepository.Save(edition, author);
            return response;
        }

        public EditionsViewModel GetEditionsViewModel(long eventId, long userId, string permissions)
        {
            var model = new EditionsViewModel(permissions, Settings)
            {
                Editions = _editionRepository.All(eventId),
                RelatedEvent = _eventRepository.FindById(eventId),
                Bindings = _userEditionBindingRepository.AllByUser(userId),
            };
            return model;
        }

        public EditionViewModel GetEditionViewModel(long id, long eventId, string permissions)
        {
            if (id > 0 && eventId > 0)
                return null;

            var model = new EditionViewModel(permissions, Settings)
            {
                Edition = eventId > 0 ? new Edition() { EventId = eventId }
                    : id > 0 ? _editionRepository.FindById(id)
                    : new Edition(),
                Events = _eventRepository.All(),
            };
            return model;
        }

        public CommandResponse BindUserAndEdition(long editionId, long userId)
        {
            var response = _userEditionBindingRepository.Add(editionId, userId);
            return response;
        }

        public CommandResponse UnbindUserAndEdition(long editionId, long userId)
        {
            var response = _userEditionBindingRepository.Remove(editionId, userId);
            return response;
        }
    }
}
