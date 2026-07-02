using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Pages
{
    public class PatientsModel : PageModel
    {
        private readonly AppDbContext _context;

        public PatientsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Patient> Patients { get; set; } = new();

        public async Task OnGetAsync()
        {
            Patients = await _context.Patients.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string Name, string Phone, decimal TotalAmount)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone))
            {
                return Page();
            }

            var patient = new Patient
            {
                Name = Name,
                Phone = Phone,
                TotalAmount = TotalAmount
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}