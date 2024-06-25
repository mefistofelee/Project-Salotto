using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class EditionsViewModel : MainViewModelBase
    {
        public EditionsViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public List<Edition> Editions { get; set; }
        public Event RelatedEvent { get; set; }
        public List<UserEditionBinding> Bindings { get; set; }
    }
}
