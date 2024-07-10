using DataAccessObject;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StoreAccountRepository
    {
        StoreAccountDAO accountDAO = new();
        public StoreAccount CheckLogin(string email, string password) => accountDAO.CheckLogin(email, password);
    }
}
