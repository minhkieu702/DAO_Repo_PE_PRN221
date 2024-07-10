using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class StoreAccountDAO : BaseDAO<StoreAccount>
    {
        public StoreAccount CheckLogin(string email, string pwd)
        {
            return GetAll().FirstOrDefault(e => e.EmailAddress.Equals(email) && e.AccountPassword.Equals(pwd));
        }
    }
}
