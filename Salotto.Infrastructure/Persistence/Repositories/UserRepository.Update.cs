using Azure.Storage.Blobs;
using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MongoDB.Driver;
using PoponGate.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Martlet.Services.Security;

namespace Salotto.Infrastructure.Persistence.Repositories
{
    public partial class UserRepository
    {
        private readonly IPasswordService _password = new DefaultPasswordService();

        public CommandResponse SaveProfile(User user, Stream photo, bool photoIsDefined, string author = null)
        {
            // Check
            if (user == null || user.UserId < 0)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            // Find persona ref
            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.UserId == user.UserId && !u.Deleted
                         select u).SingleOrDefault();

            if(found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            if (UserAlreadyExists(db, user))
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserAlreadyExists);

            found.Email = user.Email;
            found.FirstName = user.FirstName;
            found.LastName = user.LastName;

            try
            {
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted);
            }
            catch (Exception)
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);
            }
        }

        /// <summary>
        /// Inserts or updates a user record
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse SaveUser(User user, string psw, Stream photo, bool photoIsDefined, bool sendEmail, string author = null)
        {
            // Check
            if (user == null || user.UserId < 0)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            // Find persona ref
            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.UserId == user.UserId && !u.Deleted
                         select u).SingleOrDefault();

            return found == null
                ? InsertInternal(db, user, psw, photo, sendEmail, author)
                : UpdateUserInternal(db, found, user, psw, photo, photoIsDefined, sendEmail, author);
        }

        public CommandResponse DeleteUser(long id)
        {
            // Find persona ref
            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.UserId == id
                         select u).SingleOrDefault();
            if(found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            try
            {
                db.Users.Remove(found);
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted);
            }
            catch (Exception)
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);
            }
        }

        /// <summary>
        /// Sets a new password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse ChangePassword(long userId, string oldPassword, string newPassword, string author = null)
        {
            // Check
            if (oldPassword == null || userId <= 0)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            // Find persona ref
            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.UserId == userId && !u.Deleted
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            // Verify old password matches
            if (!_password.Validate(oldPassword, found.Password))
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            return UpdatePasswordInternal(db, found, newPassword, author);
        }

        /// <summary>
        /// Overwrite old password for given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse ResetPassword(long userId, string newPassword, string author = null)
        {
            // Check
            if (userId <= 0)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            // Find persona ref
            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.UserId == userId && !u.Deleted
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            return UpdatePasswordInternal(db, found, newPassword, author);
        }

        /// <summary>
        /// Mark the user record to receive a password reset
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User MarkForPasswordReset(string email)
        {
            using var db = new SalottoDatabase();
            var user = (from u in db.Users
                           where u.Email == email && !u.Deleted
                           select u).SingleOrDefault();
            if (user == null)
                return null;

            user.PasswordResetRequest = DateTime.UtcNow;
            user.PasswordResetToken = Guid.NewGuid().ToString("N"); // Just numbers
            db.SaveChanges();
            return user;
        }

        /// <summary>
        /// Verify the given password reset token exists and returns matching record
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static User FindByPasswordToken(string token)
        {
            if (token.IsNullOrWhitespace())
                return null;

            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.PasswordResetToken == token
                         select u).SingleOrDefault();
            return found;
        }

        public static User FindBySetPasswordToken(string token)
        {
            if (token.IsNullOrWhitespace())
                return null;

            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.SetPasswordToken == token
                         select u).SingleOrDefault();
            return found;
        }

        /// <summary>
        /// Reset the password if the token is still valid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public CommandResponse ResetPasswordByToken(long userId, string token, string newPassword)
        {
            // Check
            if (token.IsNullOrWhitespace() || newPassword.IsNullOrWhitespace())
                return CommandResponse.Fail();

            // Find persona ref
            using var db = new SalottoDatabase();
            var found = (from u in db.Users
                         where u.PasswordResetToken == token && !u.Deleted
                         select u).SingleOrDefault();
            if (found == null)
                return CommandResponse.Fail();

            return UpdatePasswordInternal(db, found, newPassword, found.Email);
        }

        /// <summary>
        /// Insert a new record
        /// </summary>
        /// <param name="db"></param>
        /// <param name="persona"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        private static CommandResponse InsertInternal(SalottoDatabase db, User user, string psw, Stream photo, bool sendEmail, string author)
        {
            if (UserAlreadyExists(db, user))
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserAlreadyExists); ;

            user.Email = user.Email?.ToLower();
            user.FirstName = user.FirstName?.Capitalize();
            user.LastName = user.LastName?.Capitalize();

            if (sendEmail)
            {
                user.SetPasswordToken = Guid.NewGuid().ToString("N");
                user.Password = "N/A";
            }
            else if (!psw.IsNullOrWhitespace())
            {
                // Hash password
                user.Password = new DefaultPasswordService().Store(psw);
            }
            user.Mark(isEdit: false, author);

            db.Users.Add(user);

            try
            {
                db.SaveChanges();

            }
            catch (Exception)
            {

                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);
            }
            return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(user.SetPasswordToken);
        }

        /// <summary>
        /// Update ALL PROFILE FIELDS of first record with info from second record
        /// </summary>
        /// <param name="db"></param>
        /// <param name="found"></param>
        /// <param name="persona"></param>
        /// <param name="author"></param>
        /// <param name="profileOnly"></param>
        /// <returns></returns>
        private static CommandResponse UpdateUserInternal(SalottoDatabase db, User found, User user, string psw, Stream photo, bool photoIsDefined, bool sendEmail, string author)
        {
            if (UserAlreadyExists(db, user))
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserAlreadyExists);

            user.CopyPropertiesTo(found, "UserId", "Password", "PhotoUrl", "TimeStamp_Created", "TimeStamp_CreatedBy", "TimeStamp_Modified", "TimeStamp_ModifiedBy", "Active", "Deleted");

            if (sendEmail)
            {
                found.SetPasswordToken = Guid.NewGuid().ToString("N");
                found.Password = "N/A";
            }
            else if (!psw.IsNullOrWhitespace())
            {
                found.Password = new DefaultPasswordService().Store(psw);
            }

            found.Mark(isEdit:true, author);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);
            }
            return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted).AddKey(found.SetPasswordToken);
        }

        /// <summary>
        /// Update PASSWORD of given user
        /// </summary>
        /// <param name="db"></param>
        /// <param name="found"></param>
        /// <param name="password"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        private CommandResponse UpdatePasswordInternal(SalottoDatabase db, User user, string password, string author)
        {
            user.Password = _password.Store(password);
            user.LatestPasswordChange = DateTime.UtcNow;
            user.Mark(isEdit:true, author);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);
            }
            return CommandResponse.Ok().AddMessage(AppMessages.Success_PasswordChanged);
        }

        public static CommandResponse SetPassword(long userId, string token, string password)
        {
            using var db = new SalottoDatabase();
            var user = (from u in db.Users where u.UserId == userId && u.SetPasswordToken == token select u).FirstOrDefault();

            if (user == null)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound);

            user.Password = new DefaultPasswordService().Store(password);
            user.PasswordSetOn = DateTime.UtcNow;
            user.SetPasswordToken = null;

            try
            {
                db.SaveChanges();
                return CommandResponse.Ok().AddMessage(AppMessages.Success_OperationCompleted);
            }
            catch (Exception)
            {
                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);
            }
        }


        private static string SaveToBlobStorage(BlobContainerClient container, Stream data, string name)
        {
            var blob = container.GetBlobClient(name);
            blob.DeleteIfExists();
            blob.Upload(data);
            return blob.Uri.AbsoluteUri;
        }

        private static bool UserAlreadyExists(SalottoDatabase db, User user)
        {
            var found = (from u in db.Users where u.Email == user.Email && u.UserId != user.UserId select u).FirstOrDefault();
            return found != null;
        }
    }
}
