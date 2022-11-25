using DACN2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN2.Controllers
{
    public class NhanVienController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: NhanVien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListChang()
        {
            var sptl = from ss in data.Changs select ss;
            return PartialView(sptl);

        }
        public ActionResult ListTour()
        {
            var sptl = from ss in data.Tours select ss;
            return PartialView(sptl);

        }
        private decimal TongTien()
        {
            decimal tt = 0;
            List<LichTrinh> lst = new List<LichTrinh>();

            tt = (decimal)lst.Sum(n => n.PhuongTien.GiaPT);

            return tt;


        }
        public ActionResult ListLichTrinh()
        {
            ViewBag.Tongtien = TongTien();
            var sptl = from ss in data.LichTrinhs select ss;
            return PartialView(sptl);

        }
        public ActionResult ListPhuongTien()
        {
            var sptl = from ss in data.PhuongTiens select ss;
            return PartialView(sptl);

        }
        public ActionResult ListDiaDiem()
        {
            var sptl = from ss in data.DiaDiems select ss;
            return PartialView(sptl);

        }
        
        public ActionResult ListAnUong()
        {
            var sptl = from ss in data.AnUongs select ss;
            return PartialView(sptl);

        }
        public ActionResult ListKhachSan()
        {
            var sptl = from ss in data.KSans select ss;
            return PartialView(sptl);

        }
        public ActionResult ListNguoiDiTour(int id)
        {
            var sptl = from ss in data.NguoiDiTours where ss.MaDatTour == id select ss;
            return PartialView(sptl);

        }
        public ActionResult ListDatTour()
        {
            var sptl = from ss in data.DatTours select ss;
            return PartialView(sptl);

        }
        
        public ActionResult ListKhachHang()
        {
            var sptl = from ss in data.KhachHangs select ss;
            return PartialView(sptl);

        }
        /*public ActionResult Listchang(int id)
        {
            var sptl = from ss in data.Changs where ss.ID == id select ss;
            return PartialView(sptl);

        }*/

        public ActionResult Chitietdonhang(int id)
        {
            var D_tour = data.DatTours.FirstOrDefault(m => m.MaDatTour == id);
            return View(D_tour);
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var TenDangNhap = collection["Email"];
            var MatKhau = collection["MatKhau"];
            NhanVien kh = data.NhanViens.SingleOrDefault(n => n.Email == TenDangNhap && n.MatKhau == MatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;

                return RedirectToAction("Index", "NhanVien");

            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
            return View();
        }
    }
}