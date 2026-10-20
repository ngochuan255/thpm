using System.Collections.Generic;
using DailyForecastMVC.Models;
namespace DailyForecastMVC.ViewModels
{
    public class ForecastIndexViewModel
    {
        public List<DailyForecast> ForecastsOnOctober20 { get; set; }
        public List<DailyForecast> HanoiForecasts { get; set; }
    }
}