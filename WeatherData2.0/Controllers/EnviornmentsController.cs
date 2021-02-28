using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherData2._0;
using WeatherData2._0.Models;

namespace WeatherData2._0.Controllers
{
    public class EnviornmentsController : Controller
    {
        private readonly WeatherDbContext _context;

        public EnviornmentsController(WeatherDbContext context)
        {
            _context = context;
        }

        // GET: Enviornments

        public async Task<IActionResult> Index(string sortOrder, string searchString, string searchString2, object searchStringResult)
        {

            ViewData["IndoorTemperatureSortParm"] = sortOrder == "IndoorTemperature" ? "IndoorTemperature_desc" : "IndoorTemperature";
            ViewData["IndoorHumiditySortParm"] = sortOrder == "IndoorHumidity" ? "IndoorHumidity_desc" : "IndoorHumidity";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewData["OutdoorTemperatureSortParm"] = sortOrder == "OutdoorTemperature" ? "OutdoorTemperature_desc" : "OutdoorTemperature";
            ViewData["OutdoorHumiditySortParm"] = sortOrder == "OutdoorHumidity" ? "OutdoorHumidity_desc" : "OutdoorHumidity";
            ViewData["CurrentFilter"] = searchString == "dayAverages" ? "dayaverages_desc" : "";

            //var temps = from t in _context.Enviornments
            //            select t;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    temps = temps.Where(t => t.Date.ToString().Contains(searchString));
            //}

            var dayAverages = from measure in _context.Enviornments
                                  group measure by new
                                  {
                                      measure.Date.Date
                                  } into dayMeasurent
                                  select new DayAvr
                                  {

                                      Date = dayMeasurent.Key.Date,
                                      IndoorTemperature = (int)dayMeasurent.Where(x => x.InsideOrOutside == 1).Average(x => x.Temperature),
                                      IndoorHumidity = (int)dayMeasurent.Where(x => x.InsideOrOutside == 1).Average(x => x.Humidity),
                                      OutdoorTemperature = (int)dayMeasurent.Where(x => x.InsideOrOutside == 2).Average(x => x.Temperature),
                                      OutdoorHumidity = (int)dayMeasurent.Where(x => x.InsideOrOutside == 2).Average(x => x.Humidity)
                                  };

            

            switch (sortOrder)
            {
                case "IndoorTemperature_desc":
                    dayAverages = dayAverages.OrderByDescending(t => t.IndoorTemperature);
                    break;
                case "OutdoorTemperature":
                    dayAverages = dayAverages.OrderBy(t => t.OutdoorTemperature);
                    break;
                case "OutdoorTemperature_desc":
                    dayAverages = dayAverages.OrderByDescending(t => t.OutdoorTemperature);
                    break;
                case "Date":
                    dayAverages = dayAverages.OrderBy(t => t.Date);
                    break;
                case "Date_desc":
                    dayAverages = dayAverages.OrderByDescending(t => t.Date);
                    break;
                case "IndoorHumidity":
                    dayAverages = dayAverages.OrderBy(t => t.IndoorHumidity);
                    break;
                case "IndoorHumidity_desc":
                    dayAverages = dayAverages.OrderByDescending(t => t.IndoorHumidity);
                    break;
                case "OutdoorHumidity":
                    dayAverages = dayAverages.OrderBy(t => t.OutdoorHumidity);
                    break;
                case "OutdoorHumidity_desc":
                    dayAverages = dayAverages.OrderByDescending(t => t.OutdoorHumidity);
                    break;
                default:
                    dayAverages = dayAverages.OrderBy(t => t.IndoorTemperature);
                    break;
            }


          
            return View(await dayAverages.AsNoTracking().ToListAsync());

        }

        // GET: Enviornments/DayDetail/5
        public ViewResult Details(DateTime? id, string sortOrder)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewData["InsideTempSortParm"] = sortOrder == "InsideTemp" ? "InsideTemp_desc" : "InsideTemperature";
            ViewData["OutsideTempSortParm"] = sortOrder == "OutsideTemp" ? "OutsideTemp_desc" : "OutsideTemperature";
            ViewData["InsideHumiditySortParm"] = sortOrder == "InsideHumidity" ? "InsideHumidity_desc" : "InsideHumidity";
            ViewData["OutsideHumiditySortParm"] = sortOrder == "OutsideHumidity" ? "OutsideHumidity_desc" : "OutsideHumidity";


            var dayDetails = (from measure in _context.Enviornments
                              where measure.Date.Date == id.Value.Date
                              group measure by new
                              {
                                  measure.Date  //.Date
                              } into dayMeasurent
                              select new DayDetail
                              {

                                  Date = dayMeasurent.Key.Date, 
                                  InsideTemperature = (from x in dayMeasurent where x.InsideOrOutside == 1 select x).Count() > 0 ? (float)(from x in dayMeasurent where x.InsideOrOutside == 1 select x.Temperature).Sum() : (float?)null,
                                  OutsideTemperature =(from x in dayMeasurent where x.InsideOrOutside == 2 select x).Count() > 0 ? (float)(from x in dayMeasurent where x.InsideOrOutside == 2 select x.Temperature).Sum() : (float?)null,
                                  InsideHumidity = (from x in dayMeasurent where x.InsideOrOutside == 1 select x).Count() > 0 ? (int)(from x in dayMeasurent where x.InsideOrOutside == 1 select x.Humidity).Sum() : (int?)null,
                                  OutsideHumidity = (from x in dayMeasurent where x.InsideOrOutside == 2 select x).Count() > 0 ? (int)(from x in dayMeasurent where x.InsideOrOutside == 1 select x.Humidity).Sum() : (int?)null,
                              }) ;
         

            switch (sortOrder)
            {
                case "InsideTemp_desc":
                    dayDetails = dayDetails.OrderByDescending(t => t.InsideTemperature);
                    break;
                case "OutsideTemp":
                    dayDetails = dayDetails.OrderBy(t => t.OutsideTemperature);
                    break;
                case "OutsideTemp_desc":
                    dayDetails = dayDetails.OrderByDescending(t => t.OutsideTemperature);
                    break;
                case "Date":
                    dayDetails = dayDetails.OrderBy(t => t.Date);
                    break;
                case "Date_desc":
                    dayDetails = dayDetails.OrderByDescending(t => t.Date);
                    break;
                case "InsideHumidity":
                    dayDetails = dayDetails.OrderBy(t => t.InsideHumidity);
                    break;
                case "InsideHumidity_desc":
                    dayDetails = dayDetails.OrderByDescending(t => t.InsideHumidity);
                    break;
                case "OutsideHumidity":
                    dayDetails = dayDetails.OrderBy(t => t.OutsideHumidity);
                    break;
                case "OutsideHumidity_desc":
                    dayDetails = dayDetails.OrderByDescending(t => t.OutsideHumidity);
                    break;
                default:
                    dayDetails = dayDetails.OrderBy(t => t.InsideTemperature);
                    break;
            }


            return View(dayDetails);
        }

        // GET: Enviornments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enviornments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Temperature,Humidity,InsideOrOutside")] Enviornment enviornment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enviornment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enviornment);
        }

        // GET: Enviornments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enviornment = await _context.Enviornments.FindAsync(id);
            if (enviornment == null)
            {
                return NotFound();
            }
            return View(enviornment);
        }

        // POST: Enviornments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Temperature,Humidity,InsideOrOutside")] Enviornment enviornment)
        {
            if (id != enviornment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enviornment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnviornmentExists(enviornment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enviornment);
        }

        // GET: Enviornments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enviornment = await _context.Enviornments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enviornment == null)
            {
                return NotFound();
            }

            return View(enviornment);
        }






        // POST: Enviornments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enviornment = await _context.Enviornments.FindAsync(id);
            _context.Enviornments.Remove(enviornment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnviornmentExists(int id)
        {
            return _context.Enviornments.Any(e => e.Id == id);
        }


    }
}






