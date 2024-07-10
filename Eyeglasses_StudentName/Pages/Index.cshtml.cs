using DataAccessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace Eyeglasses_StudentName.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public StoreAccount StoreAccount { get; set; }

        public StoreAccountRepository _repo;
        public IndexModel()
        {
            _repo ??= new();
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {

            var account = _repo.CheckLogin(StoreAccount.EmailAddress, StoreAccount.AccountPassword);
            if (account == null)
            {
                TempData["message"] = "Login failed. Try again!!!";
                return Page();
            }
            if (!(account.Role == 2 || account.Role == 3))
            {
                TempData["message"] = "You don't have enough permission. Please try another email.";
                return Page();
            }
            HttpContext.Session.SetInt32("r", account.Role.Value);
            return RedirectToPage("./EyeglassPages/Index");
        }
    }
}
