using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class EyeglassDAO : BaseDAO<Eyeglass>
    {
        public List<Eyeglass> GetEyeglasses(string? name, string? quantity)
        {
            name ??= string.Empty;
            quantity ??= string.Empty;
            var all = GetAll().OrderByDescending(p => p.CreatedDate).ToList();
            all.ForEach(e => e.LensType = _context.LensTypes.ToList().FirstOrDefault(c => c.LensTypeId == e.LensTypeId));
            if (int.TryParse(quantity, out int q))
            {
                return all.Where(e => e.EyeglassesName.Trim().ToUpper().Contains(name.Trim().ToUpper()) && e.Quantity == q).ToList();
            }
            return all.Where(e => e.EyeglassesName.Trim().ToUpper().Contains(name.Trim().ToUpper())).ToList();
        }
        public Eyeglass GetEyeglass(int id) => GetEyeglasses(string.Empty, string.Empty).FirstOrDefault(c => c.EyeglassesId == id);        }
}
