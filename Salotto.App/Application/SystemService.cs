using Salotto.App.Common.Settings;
using Salotto.App.Models.Standard;
using Salotto.App.Models.System;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System.Linq;

namespace Salotto.App.Application
{
    public class SystemService : ApplicationServiceBase
    {
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepo;
        private readonly DynamicRepository _dynamicRepository;

        public SystemService(AppSettings settings) : base(settings)
        {
            _userRepository = new UserRepository();
            _postRepo = new();
            _dynamicRepository = new DynamicRepository(Settings.Secrets.GetConnectionStringFor(UserSecretsSettings.Salotto));
        }

        public SystemIndexViewModel GetSystemIndexViewModel(string permissions, PostStatus status)
        {
            var users = _userRepository.All();
            var model = new SystemIndexViewModel(permissions, Settings)
            {
                Posts = _postRepo.All(status, status == PostStatus.Approved ? 50 : 0),
            };
            return model;
        }
    }
}
