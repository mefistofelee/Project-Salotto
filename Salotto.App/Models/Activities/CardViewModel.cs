using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class CardViewModel : MainViewModelBase
    {
        public CardViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public Card Card { get; set; }
    }
}
