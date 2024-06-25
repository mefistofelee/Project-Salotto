using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.App.Common.Settings;
using Salotto.App.Models.System;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using System.Linq;
using Salotto.DomainModel.Activity;
using Salotto.App.Models.Activities;
using Microsoft.AspNetCore.Http;
using System;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.IO;
using Youbiquitous.Martlet.Core.Types;

namespace Salotto.App.Application
{
    public class PostService : ApplicationServiceBase
    {
        private readonly PostRepository _postRepo;
        public PostService(AppSettings settings) : base(settings)
        {
            _postRepo = new();
        }

        public PostViewModel GetPostViewModel(long id, string permissions)
        {
            var model = new PostViewModel(permissions, Settings)
            {
                Post = id > 0 ? _postRepo.FindById(id) : new Post(),
            };
            return model;
        }

        public CommandResponse Save(Post post, IFormFile file, User user, string author = null)
        {
            // user is admin or system
            var isAdminOrSystem = !(user.Role == SalottoRole.Standard);

            // properties
            post.Status = isAdminOrSystem ? PostStatus.Approved : PostStatus.Pending;
            post.CreatedDate = DateTime.UtcNow;
            post.ApprovedDate =  isAdminOrSystem ? DateTime.UtcNow : null;
            post.CreatedByUserId = user.UserId;
            post.ApprovedByUserId = isAdminOrSystem ? user.UserId : null;

            // photo
            using (var ms = new MemoryStream())
            {
                ms.Position = 0;
                file.CopyTo(ms);
                post.Photo = Convert.ToBase64String(ms.ToArray());
            }

            var response = _postRepo.Save(post, user.Email);
            return response;
        }

        public CommandResponse ChangePostStatus(long postId, PostStatus status, long approvedById = 0, string author = null)
        {
            var response = _postRepo.ChangePostStatus(postId, status, approvedById, author);
            return response;
        }

        public CommandResponse Delete(long id, string author = null)
        {
            var response = _postRepo.Delete(id, author);
            return response;
        }

        public PostsViewModel GetPostsViewModel(long userId, string permissions)
        {
            var model = new PostsViewModel(permissions, Settings)
            {
                Posts = userId > 0 ? _postRepo.AllByUser(userId) : _postRepo.All(),
            };
            return model;
        }
    }
}
