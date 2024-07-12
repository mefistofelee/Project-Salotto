using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Salotto.App.Common.Settings;
using Salotto.App.Models.Activities;
using Salotto.App.Models.Standard;
using Salotto.App.Models.System;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System;
using System.Linq;
using Youbiquitous.Martlet.Core.Types;

namespace Salotto.App.Application
{
    public class CardService : ApplicationServiceBase
    {
        private readonly CardRepository _cardRepository;

        public CardService(AppSettings settings) : base(settings)
        {
            _cardRepository = new();
        }

        public CardViewModel GetCardViewModel(long userId, string permissions)
        {
            var model = new CardViewModel(permissions, Settings)
            {
                Card = _cardRepository.FindByUserId(userId) ?? new Card(),
            };
            return model;
        }

        public CommandResponse Request(long userId)
        {
            var response = _cardRepository.CreatePending(userId);
            return response;
        }
    }
}
