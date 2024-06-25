using Salotto.App.Common.Settings;
using Salotto.App.Models.Activities;
using Salotto.App.Models.Standard;
using Salotto.App.Models.System;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System;
using System.Linq;

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
    }
}
