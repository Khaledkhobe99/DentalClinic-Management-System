using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "يرجى إدخال اسم المستخدم وكلمة المرور";
                return Page();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);

            if (user == null)
            {
                ErrorMessage = "اسم المستخدم أو كلمة المرور غير صحيحة";
                return Page();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToPage("/Index");
        }
    }
}