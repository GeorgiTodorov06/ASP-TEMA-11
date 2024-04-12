using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebApplication6Context _context;
        public HomeController(ILogger<HomeController> logger , WebApplication6Context context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            int activeDoctorsCount = _context.Doctor.Count();
            int registeredPatientsCount = _context.Pacient.Count();
            ViewData["ActiveDoctorsCount"] = activeDoctorsCount;
            ViewData["RegisteredPatientsCount"] = registeredPatientsCount;

            int completedVisits = 0;
            int upcomingVisits = 0;
            foreach (Visit visit in _context.Visit)
            {
                if (visit.Date.Date > DateTime.Now.Date)
                {
                    upcomingVisits++;
                }
                else if (visit.Date.Date == DateTime.Now.Date && visit.Date.Hour > DateTime.Now.Hour)
                {
                    upcomingVisits++;
                }
                else
                {
                    completedVisits++;
                }
            }
            ViewData["CompletedVisitsCount"] = completedVisits;
            ViewData["UpcomingAppointmentsCount"] = upcomingVisits;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
