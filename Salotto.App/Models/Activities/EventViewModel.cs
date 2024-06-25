using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class EventViewModel : MainViewModelBase
    {
        public EventViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public Event Event { get; set; }
    }
}
