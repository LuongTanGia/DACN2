using DACN2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN2.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_tour = from s in data.Tours select s;
            return View(all_tour);
        }


        public ActionResult Detail(int id)
        {
            var D_tour = data.Tours.FirstOrDefault(m => m.ID == id);
            return View(D_tour);
        }

       
        public ActionResult Create()
        {
            Tour Tour = new Tour();            
            Tour.LoaiTours = data.LoaiTours.ToList();
            Tour.LoaiKsan = data.KSans.ToList();
            Tour.HuongDanViens = data.HuongDanViens.ToList();
            Tour.MayBays = data.MayBays.ToList();
            Tour.DiaDiems = data.DiaDiems.ToList();
            Tour.LichTrinhs = data.LichTrinhs.ToList();
            return View(Tour);
            
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Tour s)
        {
            s.LoaiTours = data.LoaiTours.ToList();
            s.LoaiKsan = data.KSans.ToList();
            s.HuongDanViens = data.HuongDanViens.ToList();
            s.MayBays = data.MayBays.ToList();
            s.DiaDiems = data.DiaDiems.ToList();
            s.LichTrinhs = data.LichTrinhs.ToList();

            var E_TenTour = collection["TenTour"];
            var E_Gia = Convert.ToInt32(collection["Gia"]);
            var E_NgayKhoiHanh = Convert.ToDateTime(collection["NgayKhoiHanh"]);
            var E_NgayKetThuc = Convert.ToDateTime(collection["NgayKetThuc"]);
            var E_SoCho = Convert.ToInt32(collection["SoCho"]);
            var E_NoiDung = collection["NoiDung"];
            var E_ChiTietTour = collection["ChiTietTour"];
            var E_MaLoaiTour = int.Parse(Request.Form["MaLoaiTour"]);
            var E_MaKS = int.Parse(Request.Form["MaKS"]);
            var E_MaTour = collection["MaTour"];
            var E_Hinh = collection["Hinh"];
            var E_MaDiaDiem = int.Parse(Request.Form["MaDiaDiem"]);
            var E_MaMayBay = int.Parse(Request.Form["MaMayBay"]);
            var E_IDHDV = int.Parse(Request.Form["IDHDV"]);
            /*--------*/
            var E_NoiKhoiHanh = collection["NoiKhoiHanh"];
            var E_MaLichTrinh = int.Parse(Request.Form["MaLichTrinh"]);
            var E_GiaNguoiLon = Convert.ToDecimal(collection["GiaNguoiLon"]);
            var E_GiaTreEm = Convert.ToDecimal(collection["GiaTreEm"]);
            var E_ThoiGian = collection["ThoiGian"];
            var E_Hinh2 = collection["Hinh2"];
            var E_Hinh3 = collection["Hinh3"];
           
            if (string.IsNullOrEmpty(E_TenTour))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.TenTour = E_TenTour.ToString();
                s.Gia = E_Gia;
                s.NgayKhoiHanh = E_NgayKhoiHanh;
                s.NgayKetThuc = E_NgayKetThuc;
                s.SoCho = E_SoCho;
                s.NoiDung = E_NoiDung.ToString();
                s.ChiTietTour = E_ChiTietTour.ToString();
                s.MaLoaiTour = E_MaLoaiTour;
                s.MaTour = E_MaTour;
                s.Hinh = E_Hinh.ToString();
                s.MaKS = E_MaKS;
                s.MaDiaDiem = E_MaDiaDiem;
                s.MaMayBay = E_MaMayBay;
                s.IDHDV = E_IDHDV;
                /*--------*/
                s.NoiKhoiHanh = E_NoiKhoiHanh.ToString();
                s.MaLichTrinh = E_MaLichTrinh;
                s.GiaNguoiLon = E_GiaNguoiLon;
                s.GiaTreEm = E_GiaTreEm;
                s.ThoiGian = E_ThoiGian.ToString();
                s.Hinh2 = E_Hinh2.ToString();
                s.Hinh3 = E_Hinh3.ToString();
              
                data.Tours.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return this.Create();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}