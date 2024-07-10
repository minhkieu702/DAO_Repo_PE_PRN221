using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;
using Repositories;

namespace Eyeglasses_StudentName.Pages.EyeglassPages
{
    public class DeleteModel : PageModel
    {
        private readonly EyeglassRepository _service;

        public DeleteModel()
        {
            _service ??= new();
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;

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

            var eyeglass = _service.GetEyeglass(id.Value);

            if (eyeglass == null)
            {
                return NotFound();
            }
            else
            {
                Eyeglass = eyeglass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToPage("./Index");
                }
                var opa = _service.GetEyeglass(Eyeglass.EyeglassesId);
                Helper.CopyValues(opa, Eyeglass);
                var check = await _service.Delete(opa);
                if (!check) await OnGetAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                await OnGetAsync(id);
                return RedirectToPage("./Index");
            }
        }
    }
}
