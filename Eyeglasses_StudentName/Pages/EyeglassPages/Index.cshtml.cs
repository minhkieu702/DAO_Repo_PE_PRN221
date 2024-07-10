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
    public class IndexModel : PageModel
    {
        private readonly int pageSize = 5;
        private readonly EyeglassRepository _service;
        public IndexModel()
        {
            _service ??= new();
        }
        public string? Search1 { get; set; }
        public string? Search2 { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public List<Eyeglass> Eyeglass { get; set; }
        
        public IActionResult OnGet(int newCurPage = 1, string s1 = @"", string s2 = @"")
        {
            if (!(HttpContext.Session.GetInt32("r") == 2 || HttpContext.Session.GetInt32("r") == 3))
            {
                TempData["message"] = "You don't have enough permission. Please try another email.";
                return RedirectToPage("../Index");
            }
            try
            {
                CurrentPage = newCurPage;
                Search1 = s1;
                Search2 = s2;

                var list = _service.GetEyeglasses(Search1, Search2);

                TotalPages = (int)Math.Ceiling(list.Count / (double)pageSize);
                Eyeglass = list.Skip(
                (CurrentPage - 1) * pageSize)
                .Take(pageSize).ToList();
                return Page();
            }
            catch (Exception ez)
            {
                TempData["message"] = ez.Message;
                return Page();
            }
        }
    }
}
