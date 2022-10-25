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
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Tour s)
        {
            var E_TenTour = collection["TenTour"];
            var E_Gia = Convert.ToInt32(collection["Gia"]);
            var E_NgayKhoiHanh = Convert.ToDateTime(collection["NgayKhoiHanh"]);
            var E_NgayKetThuc = Convert.ToDateTime(collection["NgayKetThuc"]);
            var E_SoCho = Convert.ToInt32(collection["SoCho"]);
            var E_NoiDung = collection["NoiDung"];
            var E_ChiTietTour = collection["ChiTietTour"];
            /*var E_MaTour = collection["MaTour"];*/
            var E_Hinh = collection["Hinh"];
            /*var E_MaDiaDiem = Convert.ToInt32(collection["MaDiaDiem"]);
            var E_MaKS = Convert.ToInt32(collection["MaKS"]);
            var E_MaMayBay = Convert.ToInt32(collection["MaMayBay"]);
            var E_IDHDV = Convert.ToInt32(collection["IDHDV"]);*/
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
                /*s.MaTour = E_MaTour;*/
                s.Hinh = E_Hinh.ToString();
                /*s.MaDiaDiem = E_MaDiaDiem;
                s.MaKS = E_MaKS;
                s.MaMayBay = E_MaMayBay;
                s.IDHDV = E_IDHDV;*/
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