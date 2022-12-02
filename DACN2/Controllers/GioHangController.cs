using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN2.Models;


namespace DACN2.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        MyDataDataContext data = new MyDataDataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int id, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.ID == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {

                sanpham.iSoluongNguoiLon++;
                sanpham.iSoluongTreEm++;

                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => (n.iSoluongNguoiLon + n.iSoluongTreEm));
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;


        }



        [HttpGet]

        public ActionResult DangKy(int id, string number)
        {
            //for (int i = TongSoLuong(); i >= 0; i--)
            //{
            //    if (TongSoLuong() - i != 0)
            //    {
            //        return View();
            //    }
            //}

            if (id > 0 && number != null)
            {
                int number1 = int.Parse(number);
                Tour tour = data.Tours.FirstOrDefault(s => s.ID == id);
                if (tour != null)
                {
                    int number2 = 0;
                    List<NguoiDiTour> count = data.NguoiDiTours.Where(s => s.ID == tour.ID).ToList();
                    if (count != null)
                    {
                        number2 = (int)tour.SoCho - count.Count;
                    }
                    else
                    {
                        number2 = (int)tour.SoCho;
                    }
                    if (number2 > number1)
                    {
                        ViewBag.number = number1;
                        ViewBag.id = id;
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, NguoiDiTour khs)
        {

            for (int i = TongSoLuong(); i >= 0;)
            {
                var Ten = collection["Ten"];
                var Email = collection["Email"];
                var DiaChi = collection["DiaChi"];
                var SDT = Convert.ToInt32(collection["SDT"]);
                var number = collection["number"];
                int number1 = int.Parse(number);
                number1--;
                var abc = collection["abc"];
                int id = int.Parse(abc);
                if (string.IsNullOrEmpty(Ten))
                {
                    ViewData["Error"] = "Don't empty!";
                }
                else
                {
                    khs.Ten = Ten.ToString();
                    khs.Email = Email.ToString();
                    khs.SDT = SDT;
                    data.NguoiDiTours.InsertOnSubmit(khs);
                    data.SubmitChanges();

                    ViewBag.number = number1;
                    if (number1 > 0)
                    {

                        return RedirectPermanent("/GioHang/DangKy/" + id + "?number=" + number1);
                    }
                    else
                    {
                        return RedirectToAction("GioHang", "GioHang");
                    }
                }

            }





            return RedirectToAction("Index");
        }

        public ActionResult Giohang()
        {
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            List<GioHang> lstGiohang = Laygiohang();

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.TienGiam1 = TongTien() * 0.7;
            ViewBag.TienGiam2 = TongTien() * 0.5;

            return View(lstGiohang);
        }
        public ActionResult miniGiohang()
        {
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView(lstGiohang);
        }

        public ActionResult GiohangPartial()
        {

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            return PartialView();
        }


        public ActionResult XoaGiohang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.ID == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.ID == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGiohang(int id, System.Web.Mvc.FormCollection collection)
        {
            var E_Tour = data.Tours.First(m => m.ID == id);
            ViewBag.Max = Convert.ToInt32(E_Tour.SoCho);
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.ID == id);
            if (sanpham != null)
            {
                sanpham.iSoluongNguoiLon = int.Parse(collection["txtSolg1"].ToString());
                sanpham.iSoluongTreEm = int.Parse(collection["txtSolg2"].ToString());

            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGiohang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        [HttpPost]
        public ActionResult DatHang()
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Tour");
            }
            List<GioHang> lstGiohang = Laygiohang();

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.TienGiam1 = TongTien() * 0.7;
            ViewBag.TienGiam2 = TongTien() * 0.5;
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);

        }

        public ActionResult DatHang(System.Web.Mvc.FormCollection collection)
        {
            DatTour dt = new DatTour();
            KhachHang kh = (KhachHang)Session["Taikhoan"];

            List<GioHang> gh = Laygiohang();
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            dt.SoCho = lstGiohang.Sum(n => (n.iSoluongNguoiLon + n.iSoluongTreEm));
            dt.NgayDat = DateTime.Now;
            dt.MaKhachHang = kh.MaKhachHang;
            data.DatTours.InsertOnSubmit(dt);
            data.SubmitChanges();
            double tong = 0;
            foreach (var item in gh)
            {
                ChiTietDatTour ctdh = new ChiTietDatTour();
                ctdh.MaDatTour = dt.MaDatTour;
                ctdh.ID = item.ID;
                data.ChiTietDatTours.InsertOnSubmit(ctdh);
                data.SubmitChanges();
                tong += item.dThanhtien;
                List<NguoiDiTour> nguoiDiTours = data.NguoiDiTours.Where(s => s.MaDatTour == null && s.ID == item.ID).ToList();
                if (nguoiDiTours != null)
                {
                    for (int i = 0; i < nguoiDiTours.Count; i++)
                    {
                        nguoiDiTours[i].MaDatTour = dt.MaDatTour;
                        UpdateModel(nguoiDiTours[i]);
                    }
                }
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Index", "Giohang");


        }

        /*public ActionResult NguoiDung()
        {
            KhachHang kh = (KhachHang)Session["Taikhoan"];


            var Ten = kh.hoten;
            
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Ten = Ten;
            return PartialView();
        }*/
        /* public ActionResult XacnhanDonhang()
         {
             return View();
         }
         public ActionResult XacNhanAdmin()
         {
             return View();
         }
         public ActionResult Ten()
         {

             return PartialView();


         }
 */

    }
}