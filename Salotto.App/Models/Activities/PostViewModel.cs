using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class PostViewModel : MainViewModelBase
    {
        public PostViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public Post Post { get; set; }
    }
}
