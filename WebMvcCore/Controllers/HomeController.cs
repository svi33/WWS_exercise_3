using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClassLibraryForDB.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvcCore.Models;

namespace WebMvcCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Result> listResults = _context.Results.OrderBy(b => b.Id)
                       .Skip(Math.Max(0, _context.Results.OrderBy(b => b.Id).Count() - 5)).ToList();
            return View(listResults);
        }


        public IActionResult AddCalculation(long? number)
        {
            if (number == null)
            {
                return NotFound();
            }
            Result currentResult = new Result();
            currentResult.Input = (long)number;
            currentResult.Answer = ConsoleFindInt.Work.CalcResult.GetNextSmaller((long)number);
            _context.Results.Add(currentResult);
            _context.SaveChanges();
            return PartialView(currentResult);

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
