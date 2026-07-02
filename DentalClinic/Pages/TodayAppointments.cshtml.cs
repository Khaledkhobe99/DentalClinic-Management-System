using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Pages
{
    public class TodayAppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public TodayAppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> Appointments { get; set; } = new();

        public async Task OnGetAsync()
        {
            var today = DateTime.Now.Date;
            Appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.AppointmentDate.Year == today.Year &&
                            a.AppointmentDate.Month == today.Month &&
                            a.AppointmentDate.Day == today.Day)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }
    }
}