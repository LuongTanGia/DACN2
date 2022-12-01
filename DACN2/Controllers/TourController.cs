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
        public ActionResult Index(string searchString)
        {
            ViewBag.Keyword = searchString;
            var all_tour = from s in data.Tours select s;
            if (!string.IsNullOrEmpty(searchString)) all_tour = (IOrderedQueryable<Tour>)all_tour.Where(a => a.TenTour.Contains(searchString));           
            return View(all_tour);
        }


        public ActionResult Detail(int id)
        {
            Tour Tour = new Tour();
            var D_tour = data.Tours.FirstOrDefault(m => m.ID == id);
            /*Tour.Chang = data.Changs.ToList(); */
            
            return View(D_tour);
        }

        public ActionResult Listchang(int id)
        {
            var sptl = from ss in data.Changs where ss.ID == id select ss;
            return PartialView(sptl);

        }
        public ActionResult ListLichTrinh(int id)
        {
            var sptl = from ss in data.LichTrinhs where ss.MaChang == id select ss;
            return PartialView(sptl);

        }
        public ActionResult Create()
        {
            Tour Tour = new Tour();
            /* 
             Tour.LoaiKsan = data.KSans.ToList();

             Tour.MayBays = data.PhuongTiens.ToList();
             Tour.DiaDiems = data.DiaDiems.ToList();
             Tour.LichTrinhs = data.LichTrinhs.ToList();*/
            Tour.HuongDanViens = data.NhanViens.ToList();
            Tour.LoaiTours = data.LoaiTours.ToList();
            return View(Tour);
            
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Tour s)
        {
            /*
            s.LoaiKsan = data.KSans.ToList();
            s.MayBays = data.PhuongTiens.ToList();
            s.DiaDiems = data.DiaDiems.ToList();
            s.LichTrinhs = data.LichTrinhs.ToList();*/

            s.HuongDanViens = data.NhanViens.ToList();
            s.LoaiTours = data.LoaiTours.ToList();
            s.MaLoaiTour = int.Parse(Request.Form["MaLoaiTour"]);
            s.MaNV = int.Parse(Request.Form["MaNV"]);

            var E_TenTour = collection["TenTour"];
            var E_Gia = Convert.ToInt32(collection["Gia"]);
            var E_NgayKhoiHanh = Convert.ToDateTime(collection["NgayKhoiHanh"]);
            var E_NgayKetThuc = Convert.ToDateTime(collection["NgayKetThuc"]);
            var E_SoCho = Convert.ToInt32(collection["SoCho"]);
            var E_NoiDung = collection["NoiDung"];
            
            var E_MaLoaiTour = int.Parse(Request.Form["MaLoaiTour"]);
            
            var E_Hinh = collection["Hinh"];
            
           
            /*--------*/
            var E_NoiKhoiHanh = collection["NoiKhoiHanh"];
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
                


                s.Hinh = E_Hinh.ToString();

               
                /*--------*/
                s.NoiKhoiHanh = E_NoiKhoiHanh.ToString();
                
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
        public ActionResult Edit(int id)
        {

           
            var D_tour = data.Tours.FirstOrDefault(m => m.ID == id);
            
            /*D_tour.LoaiTours = data.LoaiTours.ToList();
            D_tour.LoaiKsan = data.KSans.ToList();
            D_tour.HuongDanViens = data.NhanViens.ToList();
            D_tour.MayBays = data.PhuongTiens.ToList();
            D_tour.DiaDiems = data.DiaDiems.ToList();
            D_tour.LichTrinhs = data.LichTrinhs.ToList();*/
            return View(D_tour);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            
            /*s.LoaiTours = data.LoaiTours.ToList();*/
            var D_tour = data.Tours.First(m => m.ID == id);
/*            D_tour.LoaiTours = data.LoaiTours.ToList();
            D_tour.LoaiKsan = data.KSans.ToList();
            D_tour.HuongDanViens = data.HuongDanViens.ToList();
            D_tour.MayBays = data.MayBays.ToList();
            D_tour.DiaDiems = data.DiaDiems.ToList();
            D_tour.LichTrinhs = data.LichTrinhs.ToList();*/
            var E_TenTour = collection["TenTour"];
            var E_Gia = Convert.ToDecimal(collection["Gia"]);
            var E_NgayKhoiHanh = Convert.ToDateTime(collection["NgayKhoiHanh"]);
            var E_NgayKetThuc = Convert.ToDateTime(collection["NgayKetThuc"]);
            var E_SoCho = Convert.ToInt32(collection["SoCho"]);
            var E_NoiDung = collection["NoiDung"];
            var E_ChiTietTour = collection["ChiTietTour"];
            var E_MaLoaiTour = int.Parse(Request.Form["MaLoaiTour"]);
            var E_MaKS = int.Parse(collection["MaKS"]);
            var E_MaTour = collection["MaTour"];
            var E_Hinh = collection["Hinh"];
            var E_MaDiaDiem = Convert.ToInt32(collection["MaDiaDiem"]);
            var E_MaMayBay = Convert.ToInt32(collection["MaMayBay"]);
            var E_IDHDV = Convert.ToInt32(collection["MaNV"]);
            /*--------*/
            var E_NoiKhoiHanh = collection["NoiKhoiHanh"];
            var E_MaLichTrinh = int.Parse(Request.Form["MaLichTrinh"]);
            var E_GiaNguoiLon = Convert.ToDecimal(collection["GiaNguoiLon"]);
            var E_GiaTreEm = Convert.ToDecimal(collection["GiaTreEm"]);
            var E_ThoiGian = collection["ThoiGian"];
            var E_Hinh2 = collection["Hinh2"];
            var E_Hinh3 = collection["Hinh3"];
/*            var E_Hinh4 = collection["Hinh4"];*/
            D_tour.ID = id;

            if (string.IsNullOrEmpty(E_TenTour))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                D_tour.TenTour = E_TenTour.ToString();
                D_tour.Gia = E_Gia;
                D_tour.NgayKhoiHanh = E_NgayKhoiHanh;
                D_tour.NgayKetThuc = E_NgayKetThuc;
                D_tour.SoCho = E_SoCho;
                D_tour.NoiDung = E_NoiDung.ToString();

                D_tour.MaLoaiTour = E_MaLoaiTour;
                
                D_tour.Hinh = E_Hinh.ToString();
/*               D_tour.MaKS = E_MaKS;
                D_tour.MaDiaDiem = E_MaDiaDiem;
                D_tour.MaMayBay = E_MaMayBay;*/
                D_tour.MaNV = E_IDHDV;
                /*--------*/
                D_tour.NoiKhoiHanh = E_NoiKhoiHanh.ToString();
                /*D_tour.MaLichTrinh = E_MaLichTrinh;*/
                D_tour.GiaNguoiLon = E_GiaNguoiLon;
                D_tour.GiaTreEm = E_GiaTreEm;
                D_tour.ThoiGian = E_ThoiGian.ToString();
                D_tour.Hinh2 = E_Hinh2.ToString();
                D_tour.Hinh3 = E_Hinh3.ToString();
                /*D_tour.Hinh4 = E_Hinh4.ToString();*/

                UpdateModel(D_tour);


                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_tour = data.Tours.First(m => m.ID == id);
            return View(D_tour);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_giay = data.Tours.Where(m => m.ID == id).First();
            data.Tours.DeleteOnSubmit(D_giay);
            data.SubmitChanges();
            return RedirectToAction("ListGiay");
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

        public ActionResult TinTuc()
        {
            var all_tt = from s in data.TinTucs select s;
            return View(all_tt);
        }

        public ActionResult DetailTintuc(int id)
        {
            var D_tt = data.TinTucs.FirstOrDefault(m => m.IDTinTuc == id);
            return View(D_tt);
        }

        public ActionResult LoaiSanPham()
        {
            var loaisanpham = from s in data.LoaiTours select s;
            return PartialView(loaisanpham);
        }


        public ActionResult SPTheoLoai(int id)
        {
            var sptl = from ss in data.Tours where ss.MaLoaiTour == id select ss;
            return PartialView(sptl);

        }
/*        public ActionResult DonHang()
        {

            var dh = from ss in data.ChiTietDatTours select ss;
            var tt = data.ChiTietDatTours.Sum(n => Convert.ToInt32(n.Gia));
            ViewBag.tongtien = tt;
            return View(dh);

        }*/
        

    }
}