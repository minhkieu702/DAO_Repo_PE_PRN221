using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessObject.Models;
using Repositories;

namespace Eyeglasses_StudentName.Pages.EyeglassPages
{
    public class CreateModel : PageModel
    {
        private readonly LensTypeRepository _sCService;
        private readonly EyeglassRepository _ilPaintingArtService;
        public CreateModel()
        {
            _ilPaintingArtService ??= new();
            _sCService = new();
        }
        public IActionResult OnGet()
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
            if (Eyeglass == null)
            {
                Eyeglass = new Eyeglass();
            }
            Eyeglass.CreatedDate = DateTime.Now;
            LensType = _sCService.GetAll();
            return Page();
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<LensType> LensType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    OnGet();
                    return Page();
                }
                int check = await _ilPaintingArtService.Create(Eyeglass);
                if (check == 0)
                {
                    OnGet();
                    return Page();
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message.ToString();
                OnGet();
                return Page();
            }
        }
    }
}
