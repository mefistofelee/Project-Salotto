using Salotto.DomainModel.UserAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.Infrastructure.Persistence.Repositories
{
    public partial class UserRepository
    {
        public UserRepository()
        {

        }

        /// <summary>
        /// Find the role object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(long id)
        {
            using var db = new SalottoDatabase();
            var user = (from c in db.Users.Include(u => u.RoleInfo)
                           where c.UserId == id
                           select c).SingleOrDefault();
            return user;
        }

        /// <summary>
        /// Find the user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User FindByEmail(string email)
        {
            using var db = new SalottoDatabase();
            var user = (from u in db.Users.Include(u => u.RoleInfo)
                        where u.Email == email
                        && u.Active && !u.Deleted
                        select u).SingleOrDefault();
            return user;
        }

        /// <summary>
        /// Physical loader of records from the table to be held in memory
        /// </summary>
        /// <returns></returns>
        public IList<User> All()
        {
            using var db = new SalottoDatabase();
            var records = (from p in db.Users.Include(u => u.RoleInfo)
                           where !p.Deleted
                            select p).ToList();
            return records;
        }
    }
}
