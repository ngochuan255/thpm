using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DailyForecastMVC.Models;
using DailyForecastMVC.ViewModels;

namespace DailyForecastMVC.Controllers
{
    public class DailyForecastController : Controller
    {
        private static readonly object SyncRoot = new object();
        private static List<DailyForecast> danhsach;

        public DailyForecastController()
        {
            lock (SyncRoot)
            {
                if (danhsach == null)
                    danhsach = new List<DailyForecast>
                    {
                        new DailyForecast { CityID = "001", ForecastDate = new DateTime(2026,10,19), MinTemperature = 24, MaxTemperature = 34, Humidity = 87 },
                        new DailyForecast { CityID = "002", ForecastDate = new DateTime(2026,10,19), MinTemperature = 26, MaxTemperature = 33, Humidity = 40 },
                        new DailyForecast { CityID = "003", ForecastDate = new DateTime(2026,10,19), MinTemperature = 20, MaxTemperature = 32, Humidity = 98 },
                        new DailyForecast { CityID = "001", ForecastDate = new DateTime(2026,10,20), MinTemperature = 20, MaxTemperature = 27, Humidity = 68 },
                        new DailyForecast { CityID = "002", ForecastDate = new DateTime(2026,10,20), MinTemperature = 23, MaxTemperature = 31, Humidity = 90 },
                        new DailyForecast { CityID = "003", ForecastDate = new DateTime(2026,10,20), MinTemperature = 18, MaxTemperature = 26, Humidity = 90 }
                    };
            }
        }

        public ActionResult Index()
        {
            ForecastIndexViewModel model;
            lock (SyncRoot)
            {
                model = new ForecastIndexViewModel
                {
                    ForecastsOnOctober20 = danhsach.Where(x => x.ForecastDate.Date == new DateTime(2026,10,20)).OrderBy(x => x.CityID).ToList(),
                    HanoiForecasts = danhsach.Where(x => x.CityID == "001").OrderBy(x => x.ForecastDate).ToList()
                };
            }
            return View(model);
        }

        private void LoadCities()
        {
            ViewBag.CityOptions = new List<SelectListItem>
            {
                new SelectListItem { Value="001", Text="001 - Hà Nội" },
                new SelectListItem { Value="002", Text="002 - TP. Hồ Chí Minh" },
                new SelectListItem { Value="003", Text="003 - Hải Phòng" }
            };
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadCities();
            return View(new DailyForecast { ForecastDate = new DateTime(2026,10,20) });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(DailyForecast model)
        {
            if (!new[] { "001", "002", "003" }.Contains(model.CityID))
                ModelState.AddModelError("CityID", "Thành phố không hợp lệ.");
            if (model.ForecastDate == DateTime.MinValue)
                ModelState.AddModelError("ForecastDate", "Vui lòng chọn ngày dự báo.");
            if (model.MinTemperature > model.MaxTemperature)
                ModelState.AddModelError("MaxTemperature", "Nhiệt độ cao nhất phải lớn hơn hoặc bằng nhiệt độ thấp nhất.");
            lock (SyncRoot)
            {
                if (danhsach.Any(x => x.CityID == model.CityID && x.ForecastDate.Date == model.ForecastDate.Date))
                    ModelState.AddModelError("", "Ngày này đã có dự báo cho thành phố được chọn.");
                if (ModelState.IsValid)
                {
                    danhsach.Add(model);
                    return RedirectToAction("Details", new { cityId = model.CityID, date = model.ForecastDate.ToString("yyyy-MM-dd") });
                }
            }
            LoadCities();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string cityId, string date)
        {
            DateTime parsedDate;
            if (string.IsNullOrWhiteSpace(cityId) ||
                !DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out parsedDate)) return HttpNotFound();
            DailyForecast result;
            lock (SyncRoot)
                result = danhsach.FirstOrDefault(x => x.CityID == cityId && x.ForecastDate.Date == parsedDate.Date);
            if (result == null) return HttpNotFound();
            return View(result);
        }
    }
}