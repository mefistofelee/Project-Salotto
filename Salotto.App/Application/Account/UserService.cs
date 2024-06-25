///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;
using Salotto.App.Models.Login;
using Salotto.App.Models.UserAccount;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;

namespace Salotto.App.Application.Account
{
    public class UserService : ApplicationServiceBase
    {
        private readonly UserRepository _userRepo;
        private readonly SalottoRoleRepository _roleRepo;
        private readonly SalottoRoleRepository _logicoRoleRepository = new();
        public UserService(AppSettings settings) : base(settings)
        {
            _userRepo = new();
            _roleRepo = new();
        }
       
        public UserViewModel GetUserViewModel(string permissions, long id = 0)
        {
            var isEdit = id > 0 ? true : false;
            var model = new UserViewModel(permissions, Settings)
            {
                IsEdit = isEdit,
                RelatedUser = isEdit ? _userRepo.FindById(id) : new User(),
                Roles = _logicoRoleRepository.All()
            };
            return model;
        }
        /// <summary>
        /// Retrieves the record of given user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(long id)
        {
            return _userRepo.FindById(id);
        }

        /// <summary>
        /// Update a user password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public CommandResponse ChangePassword(long userId, string oldPassword, string newPassword, string author)
        {
            var response = _userRepo.ChangePassword(userId, oldPassword, newPassword, author);
            if (response.Success)
                response.AddMessage(AppMessages.Success_PasswordChanged);
            return response;
        }

        public UsersViewModel GetUsersViewModel(string permissions)
        {
            var model = new UsersViewModel(permissions, Settings)
            {
                Users = _userRepo.All()?.ToList()
            };
            return model;
        }

        public CommandResponse SaveUser(User user, string psw, Stream photo, bool photoIsDefined, bool sendEmail, string author)
        {
            return _userRepo.SaveUser(user, psw, photo, photoIsDefined, sendEmail, author);
        }

        public CommandResponse SaveProfile(User user, Stream photo, bool photoIsDefined, string author)
        {
            return _userRepo.SaveProfile(user, photo, photoIsDefined, author);
        }

        public CommandResponse DeleteUser(long id)
        {
            return _userRepo.DeleteUser(id);
        }

        

        //public (byte[], string) PhotoById(long id)
        //{
        //    var found = UserPhotoRepository.ByUserId(id);
        //    return (found?.Photo ?? null, found?.PhotoType ?? null);
        //}
    }
}
