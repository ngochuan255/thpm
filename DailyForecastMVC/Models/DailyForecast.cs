using System;
using System.ComponentModel.DataAnnotations;
namespace DailyForecastMVC.Models
{
    public class DailyForecast
    {
        [Display(Name = "Thành phố")]
        [Required(ErrorMessage = "Vui lòng chọn thành phố.")]
        public string CityID { get; set; }

        [Display(Name = "Ngày dự báo")]
        [Required(ErrorMessage = "Vui lòng nhập ngày dự báo.")]
        [DataType(DataType.Date)]
        public DateTime ForecastDate { get; set; }

        [Display(Name = "Nhiệt độ thấp nhất")]
        [Range(-100, 100, ErrorMessage = "Nhiệt độ phải từ -100 đến 100.")]
        public double MinTemperature { get; set; }

        [Display(Name = "Nhiệt độ cao nhất")]
        [Range(-100, 100, ErrorMessage = "Nhiệt độ phải từ -100 đến 100.")]
        public double MaxTemperature { get; set; }

        [Display(Name = "Độ ẩm")]
        [Range(0, 100, ErrorMessage = "Độ ẩm phải từ 0 đến 100.")]
        public int Humidity { get; set; }

        [Display(Name = "Nhiệt độ trung bình")]
        public double AvgTemperature
        {
            get { return (MaxTemperature + MinTemperature) / 2.0; }
        }
    }
}