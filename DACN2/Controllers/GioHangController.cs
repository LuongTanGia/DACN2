using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACN2.Models;


namespace QuanLyCH.Controllers
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
        public ActionResult DatHang()
        {
           /* if (Session["Taikhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }*/
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
            Tour s = new Tour();
            List<GioHang> gh = Laygiohang();                      
            dt.NgayDat = DateTime.Now;
            dt.MaKhachHang = kh.MaKhachHang;
            data.DatTours.InsertOnSubmit(dt);
            data.SubmitChanges();
            double tong = 0;
            foreach (var item in gh)
            {
                ChiTietDatTour ctdh = new ChiTietDatTour();
                ctdh.MaDatTour = dt.MaDatTour;
                ctdh.ID= item.ID;
                ctdh.SoCho = (item.iSoluongNguoiLon + item.iSoluongTreEm);
                ctdh.Gia = (decimal)item.dThanhtien;
                s = data.Tours.Single(n => n.ID == item.ID);
                s.SoCho -= ctdh.SoCho;
                data.SubmitChanges();
                data.ChiTietDatTours.InsertOnSubmit(ctdh);
                tong += item.dThanhtien;
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacnhanDonHang", "Giohang");


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