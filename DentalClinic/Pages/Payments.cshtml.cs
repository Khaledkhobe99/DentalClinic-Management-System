using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Pages
{
    public class PaymentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public PaymentsModel(AppDbContext context)
        {
            _context = context;
        }

        public Patient Patient { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
        public decimal TotalPaid { get; set; }
        public decimal Remaining { get; set; }

        public async Task OnGetAsync(int patientId)
        {
            Patient = await _context.Patients.FindAsync(patientId) ?? new();

            Payments = await _context.Payments
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            TotalPaid = Payments.Sum(p => p.Amount);
            Remaining = Patient.TotalAmount - TotalPaid;
        }

        public async Task<IActionResult> OnPostAsync(int patientId, decimal Amount)
        {
            if (Amount <= 0)
                return RedirectToPage(new { patientId });

            var payment = new Payment
            {
                PatientId = patientId,
                Amount = Amount,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return RedirectToPage(new { patientId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, int patientId)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { patientId });
        }
    }
}