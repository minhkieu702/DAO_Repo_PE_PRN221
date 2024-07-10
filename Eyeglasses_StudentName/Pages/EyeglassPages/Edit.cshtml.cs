using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;
using Repositories;

namespace Eyeglasses_StudentName.Pages.EyeglassPages
{
    public class EditModel : PageModel
    {
        private readonly LensTypeRepository _sCService;
        private readonly EyeglassRepository _ilPaintingArtService;
        public EditModel()
        {
            _ilPaintingArtService ??= new();
            _sCService = new();
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; }
        public List<LensType> LensType { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("r") == 2)
            {
                TempData["message"] = "You don't have enough permission. Please try another email.";
                return RedirectToPage("./Index");
            }
            if (HttpContext.Session.GetInt32("r") != 3)
            {
                TempData["message"] = "You don't have enough permission. Please try another email.";
                return RedirectToPage("../Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var eyeglass = _ilPaintingArtService.GetEyeglass(id.Value);

            if (eyeglass == null)
            {
                return NotFound();
            }
            if (Eyeglass == null)
            {
                Eyeglass = eyeglass;
            }
            LensType = _sCService.GetAll();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    OnGetAsync(Eyeglass.EyeglassesId);
                    return Page();
                }

                try
                {
                    var opa = _ilPaintingArtService.GetEyeglass(Eyeglass.EyeglassesId);
                    Helper.CopyValues(opa, Eyeglass);

                    int check = await _ilPaintingArtService.Update(opa);
                    if (check == 0)
                    {
                        OnGetAsync(Eyeglass.EyeglassesId);
                        return Page();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    TempData["message"] = ex.Message;
                    OnGetAsync(Eyeglass.EyeglassesId);
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                OnGetAsync(Eyeglass.EyeglassesId);
                return Page();
            }
        }
    }
}
