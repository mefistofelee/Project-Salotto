using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.App.Common.Settings;
using Salotto.App.Models.System;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System.Linq;
using Salotto.App.Models.Standard;
using Salotto.DomainModel.Activity;
using Salotto.Resources;

namespace Salotto.App.Application
{
    public class StandardService : ApplicationServiceBase
    {
        private readonly PostRepository _postRepo;
        public StandardService(AppSettings settings) : base(settings)
        {
            _postRepo = new();
        }

        public StandardIndexViewModel GetStandardIndexViewModel(string permissions)
        {
            var model = new StandardIndexViewModel(permissions, Settings)
            {
                Posts = _postRepo.All(PostStatus.Approved, 50),
            };
            return model;
        }
    }
}
