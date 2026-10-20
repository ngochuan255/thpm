using Bai_3._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bai_3._1.Controllers
{
    public class ThongTinSinhVienController : Controller
    {
        // GET: ThongTinSinhVien
        public ActionResult Index()
        {
            SinhVien sinhVien = new SinhVien()
            {
                MaSV = "SV001",
                HoTen = "Lê Thị Vân",
                date = new DateTime(2003, 09, 16),
                DiaChi = "Từ Liêm"
            };
            return View(sinhVien);
        }
    }
}