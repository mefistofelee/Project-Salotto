using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Activities
{
    public class PostsViewModel : MainViewModelBase
    {
        public PostsViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public List<Post> Posts { get; set; }
    }
}
