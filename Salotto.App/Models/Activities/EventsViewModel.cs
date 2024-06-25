using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class EventsViewModel : MainViewModelBase
    {
        public EventsViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public List<Event> Events { get; set; }
    }
}
