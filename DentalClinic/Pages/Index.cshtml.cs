using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace DentalClinic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public int TotalPatients { get; set; }
        public int TodayAppointments { get; set; }
        public int TotalAppointments { get; set; }
        public List<TodayAppointmentRow> TodayList { get; set; } = new();
        public int WeekAppointments { get; set; }
        public int CancelledToday { get; set; }
        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                Response.Redirect("/Login");
                return;
            }
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            TotalPatients = await _context.Patients.CountAsync();
            TotalAppointments = await _context.Appointments.CountAsync();
            TodayAppointments = await _context.Appointments
                .Where(a => a.AppointmentDate.Date == today)
                .CountAsync();
            WeekAppointments = await _context.Appointments
                .Where(a => a.AppointmentDate >= startOfWeek)
                .CountAsync();
            CancelledToday = await _context.Appointments
                .Where(a => a.AppointmentDate.Date == today && a.Status == "Cancelled")
                .CountAsync();
            TodayList = await _context.Appointments
                .Where(a => a.AppointmentDate.Date == today)
                .Include(a => a.Patient)
                .OrderBy(a => a.AppointmentDate)
                .Select(a => new TodayAppointmentRow
                {
                    PatientName = a.Patient!.Name,
                    Time = a.AppointmentDate.ToString("HH:mm"),
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status
                })
                .ToListAsync();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
    public class TodayAppointmentRow
    {
        public string PatientName { get; set; } = "";
        public string Time { get; set; } = "";
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "";
    }
}