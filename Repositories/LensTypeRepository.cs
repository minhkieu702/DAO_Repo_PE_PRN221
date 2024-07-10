using DataAccessObject;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LensTypeRepository
    {
        LensTypeDAO lensTypeDAO = new();
        public List<LensType> GetAll() => lensTypeDAO.GetAll();
    }
}
