using Salotto.App.Common.Settings;
using PoponGate.Model.Queries;
using System.Collections.Generic;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Models.Admin
{
    public class AdminIndexViewModel : MainViewModelBase
    {
        public AdminIndexViewModel(string permissions, AppSettings settings) : base(permissions, settings)
        {

        }

        public List<Post> Posts { get; set; }
        public int PendingPostsCount { get; set; }
    }
}
