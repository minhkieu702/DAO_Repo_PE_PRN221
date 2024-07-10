using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessObject.Models;
using NuGet.Protocol.Core.Types;
using Repositories;

namespace Eyeglasses_StudentName.Pages.EyeglassPages
{
    public class DetailsModel : PageModel
    {
        private readonly EyeglassRepository _service;

        public DetailsModel()
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
    }
}
