using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class EditionViewModel : MainViewModelBase
    {
        public EditionViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public Edition Edition { get; set; }
        public List<Event> Events { get; set; }
    }
}
