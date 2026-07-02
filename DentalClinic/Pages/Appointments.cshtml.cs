using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Pages
{
    public class AppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public AppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> Appointments { get; set; } = new();
        public string PatientName { get; set; } = string.Empty;

        public async Task OnGetAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                PatientName = patient.Name;
            }
            Appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int patientId, DateTime AppointmentDate)
        {
            var conflict = await _context.Appointments
                .AnyAsync(a => a.AppointmentDate == AppointmentDate &&
                               a.Status != "Cancelled");

            if (conflict)
            {
                TempData["ErrorMessage"] = "conflict";
                return RedirectToPage(new { patientId });
            }

            var appointment = new Appointment
            {
                PatientId = patientId,
                AppointmentDate = AppointmentDate,
                Status = "Scheduled"
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToPage(new { patientId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, int patientId)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { patientId });
        }

        public async Task<IActionResult> OnPostRescheduleAsync(int id, DateTime NewDate, int patientId)
        {
            var conflict = await _context.Appointments
                .AnyAsync(a => a.AppointmentDate == NewDate &&
                               a.Id != id &&
                               a.Status != "Cancelled");

            if (conflict)
            {
                TempData["ErrorMessage"] = "conflict";
                return RedirectToPage(new { patientId });
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.AppointmentDate = NewDate;
                appointment.Status = "Rescheduled";
                await _context.SaveChangesAsync();
                return RedirectToPage(new { patientId = appointment.PatientId });
            }

            return RedirectToPage(new { patientId });
        }
    }
}