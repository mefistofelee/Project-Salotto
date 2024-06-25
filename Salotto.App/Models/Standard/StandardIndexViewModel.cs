using Salotto.App.Common.Settings;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Standard
{
    public class StandardIndexViewModel : MainViewModelBase
    {
        public StandardIndexViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {
            
        }

        public List<Post> Posts { get; set; }
    }
}
