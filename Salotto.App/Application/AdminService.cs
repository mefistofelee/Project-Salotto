using Salotto.App.Common.Settings;
using Salotto.App.Models.Admin;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System;
using System.Linq;

namespace Salotto.App.Application
{
    public class AdminService : ApplicationServiceBase
    {
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;
        private readonly DynamicRepository _dynamicRepository;

        public AdminService(AppSettings settings) : base(settings)
        {
            _userRepository = new UserRepository();
            _postRepository = new();
            _dynamicRepository = new DynamicRepository(Settings.Secrets.GetConnectionStringFor(UserSecretsSettings.Salotto));
        }

        public AdminIndexViewModel GetAdminIndexViewModel(string permissions, PostStatus status)
        {
            var users = _userRepository.All();
            var model = new AdminIndexViewModel(permissions, Settings)
            {
                Posts = _postRepository.All(status, status == PostStatus.Approved ? 50 : 0)
            };

            if (status == PostStatus.Approved)
                model.PendingPostsCount = _postRepository.CountByStatus(PostStatus.Pending);

            return model;   
        }
    }
}
