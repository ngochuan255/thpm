using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bai_3._2.Models;

namespace Bai_3._2.Controllers
{
    public class NhapNhanVienController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var nhanVien = Session["NhanVien"] as NhanVien ?? new NhanVien
            {
                GioiTinh = "Nam",
                ChucVu = "Trưởng phòng",
                NgoaiNgu = new[] { "Anh" }
            };

            return View(nhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return View(nhanVien);
            }

            Session["NhanVien"] = nhanVien;
            TempData["ThongBao"] = "Đã lưu thông tin nhân viên trong phiên làm việc.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xoa()
        {
            ModelState.Clear();
            TempData.Remove("ThongBao");
            return View("Index", new NhanVien());
        }

        [HttpGet]
        public ActionResult HienThi()
        {
            return View(Session["NhanVien"] as NhanVien);
        }
    }
}
