using Microsoft.AspNetCore.Mvc;
using Pr06_Api.Interface;

namespace Pr06_Api.Controllers
{
    public class NasaController : Controller
    {
        private readonly INasaHttpService _nasaService;

        public NasaController(INasaHttpService nasaService)
        {
            _nasaService = nasaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var apod = await _nasaService.GetApodAsync();
                return View(apod);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            try
            {
                var apod = await _nasaService.GetApodByDateAsync(date);
                return View("Index", apod);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var apods = await _nasaService.GetApodByDateRangeAsync(startDate, endDate);
                ViewBag.ApodList = apods;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }
    }
}