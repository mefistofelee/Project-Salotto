using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.WireProtocol.Messages;
using PoponGate.ExtensionMethods;
using Salotto.DomainModel.Activity;
using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Types;

namespace Salotto.Infrastructure.Persistence.Repositories
{
    public class PostRepository
    {
        #region WRITE

        public CommandResponse Save(Post post, string author = null)
        {
            using var db = new SalottoDatabase();
            var found = (from p in db.Posts 
                         where !p.Deleted && p.Active && p.Id == post.Id select p).FirstOrDefault();

            var isEdit = post.Id > 0;
            if (isEdit && found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);

            return !isEdit
                ? InsertInternal(post, db, author)
                : UpdateInternal(post, found, db, author);
        }

        internal CommandResponse InsertInternal(Post post, SalottoDatabase db, string author = null)
        {
            try
            {
                post.Mark(isEdit: false, author);
                db.Posts.Add(post);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(post.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        internal CommandResponse UpdateInternal(Post post, Post found, SalottoDatabase db, string author = null)
        {
            try
            {
                post.CopyPropertiesTo(found, "Id", "CreatedByUserId", "ApprovedByUserId", "EditionId");
                found.Mark(isEdit: true, author);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(post.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        public CommandResponse Delete(long id, string author = null)
        {
            using var db = new SalottoDatabase();
            var found = (from p in db.Posts where !p.Deleted && p.Active && p.Id == id select p).FirstOrDefault();

            try
            {
                found.Deleted = true;
                found.Active = false;
                found.Mark(isEdit: true, author);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(found.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        public CommandResponse ChangePostStatus(long postId, PostStatus status, long approvedById = 0, string author = null)
        {
            using var db = new SalottoDatabase();
            var found = (from p in db.Posts where !p.Deleted && p.Active && p.Id == postId select p).FirstOrDefault();
            if (found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);

            try
            {
                found.Status = status;

                if (status == PostStatus.Approved && approvedById != 0)
                    found.ApprovedByUserId = approvedById;

                found.Mark(isEdit: true, author);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(found.Id.ToString());
            }
            catch
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_Generic);
            }
        }

        #endregion

        #region READ

        public List<Post> All(int max = 0)
        {
            using var db = new SalottoDatabase();
            var list = (from p in db.Posts where !p.Deleted && p.Active select p);
            if (max > 0)
                list = list.OrderByDescending(p => p.Id).Take(max);
            return list.ToList();
        }

        public List<Post> All(PostStatus status, int max = 0)
        {
            using var db = new SalottoDatabase();
            var list = (from p in db.Posts
                        .Include(x => x.CreatedByUser)
                        .Include(x => x.ApprovedByUser) 
                        where !p.Deleted && p.Active && p.Status == status select p);
            if (max > 0)
                list = list.OrderByDescending(p => p.Id).Take(max);
            return list.ToList();
        }

        public Post FindById(long id)
        {
            using var db = new SalottoDatabase();
            var found = (from p in db.Posts where !p.Deleted && p.Active && p.Id == id select p).FirstOrDefault();
            return found;
        }

        public Post FindByCreatedByUserId(long userId)
        {
            using var db = new SalottoDatabase();
            var found = (from p in db.Posts 
                         where !p.Deleted && p.Active && p.CreatedByUserId == userId select p).FirstOrDefault();
            return found;
        }

        public Post FindByApprovedByUserId(long userId)
        {
            using var db = new SalottoDatabase();
            var found = (from p in db.Posts
                         where !p.Deleted && p.Active && p.ApprovedByUserId == userId
                         select p).FirstOrDefault();
            return found;
        }

        public int CountByStatus(PostStatus status)
        {
            using var db = new SalottoDatabase();
            var count = (from p in db.Posts where !p.Deleted && p.Active && p.Status == status select p).Count();
            return count;
        }

        public List<Post> AllByUser(long userId)
        {
            using var db = new SalottoDatabase();
            var list = (from p in db.Posts
                        .Include(x => x.CreatedByUser).Include(x => x.ApprovedByUser) 
                        where !p.Deleted && p.Active && p.CreatedByUserId == userId 
                        select p).ToList();
            return list;
        }

        #endregion
    }
}
