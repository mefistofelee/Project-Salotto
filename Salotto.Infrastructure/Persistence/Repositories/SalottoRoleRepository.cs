using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.Infrastructure.Persistence.Repositories
{
    public class SalottoRoleRepository
    {
        public List<SalottoRole> All()
        {
            using var db = new SalottoDatabase();
            return db.SalottoRoles?.ToList();
        }
    }
}
