using DataAccessObject;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EyeglassRepository
    {
        EyeglassDAO eyeglassDAO = new EyeglassDAO();
        public List<Eyeglass> GetEyeglasses(string? name, string? quantity) => eyeglassDAO.GetEyeglasses(name, quantity);
        public async Task<int> Update(Eyeglass eyeglass) => await eyeglassDAO.UpdateAsync(eyeglass);
        public async Task<int> Create(Eyeglass eyeglass) => await eyeglassDAO.CreateAsync(eyeglass);
        public async Task<bool> Delete(Eyeglass eyeglass) => await eyeglassDAO.RemoveAsync(eyeglass);
        public Eyeglass GetEyeglass(int id) => eyeglassDAO.GetEyeglass(id);
    }
}
