using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class VisitsController : Controller
    {
        private readonly WebApplication6Context _context;

        public VisitsController(WebApplication6Context context)
        {
            _context = context;
        }
        public List<Doctor> ListOfDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (Doctor item in _context.Doctor)
            {
                doctors.Add(item);
            }

            return doctors;
        }
        public List<Pacient> ListOfPacients()
        {
            List<Pacient> pacients = new List<Pacient>();
            foreach (Pacient item in _context.Pacient)
            {
                pacients.Add(item);
            }

            return pacients;
        }
        // GET: Visits
        public async Task<IActionResult> Index()
        {
              return _context.Visit != null ? 
                          View(await _context.Visit.ToListAsync()) :
                          Problem("Entity set 'WebApplication6Context.Visit'  is null.");
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visit == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewBag.Doctors = ListOfDoctors();
            ViewBag.Pacients = ListOfPacients();
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Description,PacientId,DoctorId")] Visit visit)
        {
            foreach (Visit item in _context.Visit)
            {
                
                DateTime itemEndTime = item.Date.AddHours(1);

                
                if (visit.DoctorId == item.DoctorId && visit.Date.Date == item.Date.Date && visit.Date >= item.Date && visit.Date < itemEndTime)
                {
                    
                    return NotFound();
                }
            }
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(visit);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visit == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewBag.Doctors = ListOfDoctors();
            ViewBag.Pacients = ListOfPacients();
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description,PacientId,DoctorId")] Visit visit)
        {
            foreach (Visit item in _context.Visit)
            {

                DateTime itemEndTime = item.Date.AddHours(1);


                if (visit.DoctorId == item.DoctorId && visit.Date.Date == item.Date.Date && visit.Date >= item.Date && visit.Date < itemEndTime)
                {

                    return NotFound();
                }
            }
            if (id != visit.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(visit);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visit == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visit == null)
            {
                return Problem("Entity set 'WebApplication6Context.Visit'  is null.");
            }
            var visit = await _context.Visit.FindAsync(id);
            if (visit != null)
            {
                _context.Visit.Remove(visit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
          return (_context.Visit?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
