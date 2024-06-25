using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.System
{
    public class SystemIndexViewModel : MainViewModelBase
    {
        public SystemIndexViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {
            
        }

        public List<Post> Posts { get; set; }
    }
}
