using DACN2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using DACN2.Controllers;


namespace DACN2.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: NguoiDung
        MyDataDataContext data = new MyDataDataContext();

        
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var TenDangNhap = collection["TenDangNhap"];
            var MatKhau = collection["MatKhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TenDangNhap == TenDangNhap && n.MatKhau == MatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;

                return RedirectToAction("Index", "Tour");

            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
            return View();
        }

        /*       public ActionResult Nhom()
               {
                   return PartialView();
               }
               public ActionResult forgotpass()
               {
                   return View();
               }
               public ActionResult DangXuat()
               {
                   Session.Clear();
                   return RedirectToAction("DangNhap");
               }

               public ActionResult profile(int id)
               {
                   KhachHang kh = (KhachHang)Session["Taikhoan"];
                   kh.MaKhachHang = id;
                   var E_Pro = data.KhachHangs.First(m => m.MaKhachHang == id);
                   return View(E_Pro);
               }
               [HttpPost]
               public ActionResult profile(int id, FormCollection collection)
               {
                   KhachHang kh = (KhachHang)Session["Taikhoan"];
                   kh.MaKhachHang = id;
                   var E_Pro = data.KhachHangs.First(m => m.MaKhachHang == id);

                   var Ten = collection["Ten"];
                   var TenDangNhap = collection["TenDangNhap"];
                   var MatKhau = collection["MatKhau"];
                   var MatKhauXacNhan = collection["MatKhauXacNhan"];
                   var Email = collection["Email"];
                   var DiaChi = collection["DiaChi"];
                   var SDT = collection["SDT"];
                   var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);

                   E_Pro.MaKhachHang = id;
                   if (string.IsNullOrEmpty(Ten))
                   {
                       ViewData["Error"] = "Don't empty!";
                   }
                   else
                   {

                       kh.TenDangNhap = TenDangNhap;
                       kh.MatKhau = MatKhau;
                       kh.Email = Email;
                       kh.DiaChi = DiaChi;
                       kh.SDT = SDT;
                       kh.ngaysinh = DateTime.Parse(ngaysinh);
                       kh.id = Convert.ToInt32(collection["id"]);

                       UpdateModel(E_Pro);
                       data.SubmitChanges();
                       return RedirectToAction("Index", "Home");
                   }
                   return this.profile(id);
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

                   if (tt <= 300000)
                   {
                       return tt;
                   }
                   else
                   {
                       if (tt > 300000 && tt <= 1000000)
                       {
                           return tt * 0.7;
                       }
                       else
                       {
                           return tt * 0.5;
                       }
                   }
               }
       */


        /*        public void sendPass(string pass , System.Web.Mvc.FormCollection collection)

                {
                    DonHang dh = new DonHang();
                    KhachHang kh = (KhachHang)Session["Taikhoan"];

                    var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
                    dh.ngaydat = DateTime.Now;



                    MailAddress fromAddress = new MailAddress("luong1tan1gia123@gmail.com", "Cua Hang Giay");

                    MailAddress toAddress = new MailAddress(kh.Email.ToString());

                    const string fromPassword = "fccyjktykdfjcgaq";

                    string subject = "Xac Nhan Don Hang";

                    string Tien = TongTien().ToString();

                    string Soluong = TongSoLuongSanPham().ToString();

                    string Ten = kh.Ten.ToString();

                    string NgayDat = dh.ngaydat.ToString();

                    string madh = dh.madon.ToString();


                    SmtpClient smtp = new SmtpClient

                    {

                        Host = "smtp.gmail.com",

                        Port = 587,

                        EnableSsl = true,

                        DeliveryMethod = SmtpDeliveryMethod.Network,

                        UseDefaultCredentials = false,

                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                    };

                    using (MailMessage message = new MailMessage(fromAddress, toAddress)

                    {

                        Subject = subject,

                        Body =

                                "<p>Cam On Ban Da Dung Dich Vu Cua Shop Giay : </p>" +
                                 "<p>Don Hang Gom : " + Soluong + " San Pham  </p>" +
                                 "<p>Ten : " + Ten + "   </p>" +
                                  "<p>Email : " + kh.Email.ToString() + "   </p>" +
                                 "<p color = red>Tong Gia Tien : " + Tien + " VND  </p>"
                                + "<p>Ngay Nhan Hang :" + NgayDat + "</p>"
                                + "<p>Ma Don Hang : " + madh + "</p>" +
                                "<button> Xac Nhan </Button>",

                        IsBodyHtml = true,

                    })
                    {

                        smtp.Send(message);
                        XacnhanDonhang();

                    }



                }*/
        public ActionResult XacnhanDonhang()
        {
            return RedirectToAction("XacnhanDonhang", "GioHang");

        }


        public JsonResult CheckUsername(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = data.KhachHangs.Where(x => x.TenDangNhap == userdata).SingleOrDefault();

            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult CheckEmail(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = data.KhachHangs.Where(x => x.Email == userdata).SingleOrDefault();

            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult CheckPhone(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = data.KhachHangs.Where(x => x.SDT.ToString() == userdata).SingleOrDefault();

            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
    }
}