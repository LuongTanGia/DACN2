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
            List<Chang> changs = data.Changs.Where(s => s.GiaChang != null ).ToList();
            

            if (changs.Count > 0)   
            {
                for (int i = 0; i < changs.Count; i++)
                {
                    Chang chang = data.Changs.FirstOrDefault(s => s.MaChang == changs[i].MaChang);
       

                    if (chang != null)
                    {
                        decimal tongTien = 0;
                        List<LichTrinh> lichTrinhs = data.LichTrinhs.Where(s => s.MaChang == chang.MaChang ).ToList();
                        if (lichTrinhs.Count > 0)
                        {

                            for (int j = 0; j < lichTrinhs.Count; j++)
                            {

                                LichTrinh lichTrinh = data.LichTrinhs.FirstOrDefault(s => s.MaLichTrinh == lichTrinhs[j].MaLichTrinh && s.GiaLichTrinh != null);
                                if (lichTrinh != null)
                                {
                                    tongTien += (decimal)lichTrinh.GiaLichTrinh;
                                }

                                if (tongTien > 0)
                                {
                                    chang.GiaChang = tongTien;
                                    UpdateModel(chang);
                                    data.SubmitChanges();
                                }
                            }
                            
                        }
                    }
                }
            }

            return PartialView(sptl);

        }
        
        public ActionResult ListTour()
        {
            

            var sptl = from ss in data.Tours select ss;

            List<Tour> tours = data.Tours.Where(s => s.Gia != null).ToList();
            if (tours.Count > 0)
            {
                for (int i = 0; i < tours.Count; i++)
                {

                    Tour Tour = data.Tours.FirstOrDefault(s => s.ID == tours[i].ID);
                    decimal tongTien = 0;
                    decimal TongTienLoi = 0;
                    if (Tour != null)
                    {

                      
                        List<Chang> Changs = data.Changs.Where(s => s.ID == Tour.ID).ToList();
                        if (Changs.Count > 0)
                        {

                            for (int j = 0; j < Changs.Count; j++)
                            {

                                Chang chang = data.Changs.FirstOrDefault(s => s.ID == Changs[j].ID && s.GiaChang != null);
                                if (chang != null)
                                {
                                    tongTien += (decimal)chang.GiaChang;
                                }
                                if (tongTien > 0)
                                {
                                    Tour.TongChang = tongTien;
                                    Tour.LoiNhuan = (decimal)(Tour.Gia - Tour.TongChang);
                                    TongTienLoi = tours.Sum(s => s.LoiNhuan);
                                    UpdateModel(Tour);
                                    data.SubmitChanges();
                                    ViewBag.tongtien = TongTienLoi;
                                }
                            }
                        }
                    }
                }
            }
            
            

            return PartialView(sptl);

        }
       /* private decimal TongTien()
        {
            decimal tt = 0;
            List<LichTrinh> lst = new List<LichTrinh>();

            tt = (decimal)lst.Sum(n => n.PhuongTien.GiaPT);

            return tt;


        }*/
       /* public ActionResult ListLichTrinh()
        {
            ViewBag.Tongtien = TongTien();
            var sptl = from ss in data.LichTrinhs select ss;
            return PartialView(sptl);

        }*/
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
            var sptl = from ss in data.NguoiDiTours where ss.ChiTietDatTour.ID == id select ss;
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
       /* private decimal TongTien()
        {
            decimal tt = 0;
            List<LichTrinh> lst = new List<LichTrinh>();

            tt = (decimal)lst.Sum(n => n.PhuongTien.GiaPT);

            return tt;


        }*/
        public ActionResult ListLichTrinh()
        {
            /*ViewBag.Tongtien = TongTien();*/
            var sptl = from ss in data.LichTrinhs select ss;
            List<LichTrinh> lichTrinhs = data.LichTrinhs.Where(s => s.GiaLichTrinh == null).ToList();
            if (lichTrinhs.Count > 0)
            {
                for (int i = 0; i < lichTrinhs.Count; i++)
                {
                    LichTrinh lichTrinh = data.LichTrinhs.FirstOrDefault(s => s.MaLichTrinh == lichTrinhs[i].MaLichTrinh);
                    decimal tongTien = 0;
                    if (lichTrinh != null)
                    {
                        AnUong anUong = data.AnUongs.FirstOrDefault(s => s.MaAnUong == lichTrinh.MaAnUong && s.ChiPhi > 0);
                        if (anUong != null)
                        {
                            tongTien += (decimal)anUong.ChiPhi;
                        }
                        KSan kSan = data.KSans.FirstOrDefault(s => s.MaKS == lichTrinh.MaKS && s.GiaKS > 0);
                        if (kSan != null)
                        {
                            tongTien += (decimal)kSan.GiaKS;
                        }
                        DiaDiem diaDiem = data.DiaDiems.FirstOrDefault(s => s.MaDiaDiem == lichTrinh.MaDiaDiem
                        && s.ChiPhiDD > 0);
                        if (diaDiem != null)
                        {
                            tongTien += (decimal)diaDiem.ChiPhiDD;
                        }
                        PhuongTien phuongTien = data.PhuongTiens.FirstOrDefault(s => s.MaPhuongTien == lichTrinh.MaPhuongTien
                        && s.GiaPT > 0);
                        if (phuongTien != null)
                        {
                            tongTien += (decimal)phuongTien.GiaPT;
                        }
                        if (tongTien > 0)
                        {
                            lichTrinh.GiaLichTrinh = tongTien;
                            UpdateModel(lichTrinh);
                            data.SubmitChanges();
                        }
                    }
                }
            }
            return PartialView(sptl);
        }


        public ActionResult CreateChang()
        {
            Chang Chang = new Chang();          
            Chang.Tours = data.Tours.ToList();
            return View(Chang);

        }
        [HttpPost]
        public ActionResult CreateChang(FormCollection collection, Chang s)
        {
           
            s.Tours = data.Tours.ToList();
           
            s.ID = int.Parse(Request.Form["ID"]);

            var E_Ten = collection["TenChang"];
            var E_Gia = Convert.ToInt32(collection["Gia"]);
            var E_NoiDung = collection["NoiDungChang"];
            if (string.IsNullOrEmpty(E_Ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.TenChang = E_Ten.ToString();
                s.GiaChang = E_Gia;                
                s.NoiDungChang = E_NoiDung.ToString();
                data.Changs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return this.CreateChang();
        }
        public ActionResult CreateNhanVien()
        {
            NhanVien NhanVien = new NhanVien();
            NhanVien.chucVus = data.ChucVus.ToList();
            return View(NhanVien);

        }
        [HttpPost]
        public ActionResult CreateNhanVien(FormCollection collection, NhanVien s)
        {

            s.chucVus = data.ChucVus.ToList();

            s.MaCV = int.Parse(Request.Form["MaCV"]);

            var E_Ten = collection["TenNV"];
            var E_SDT = Convert.ToInt32(collection["SDT"]);
            var E_Email = collection["Email"];

            var E_MatKhau = collection["MatKhau"];
            if (string.IsNullOrEmpty(E_Ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.TenNV = E_Ten.ToString();
                s.SDT = E_SDT;
                s.Email = E_Email.ToString();
                s.MatKhau = E_MatKhau.ToString();

                data.NhanViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return this.CreateNhanVien();
        }
        public ActionResult Chitietdonhang(int id)
        {
            var D_tour = data.DatTours.FirstOrDefault(m => m.MaDatTour == id);
            return View(D_tour);
        }

        public ActionResult CreateLichTrinh()
        {
            LichTrinh lichTrinh = new LichTrinh();

            lichTrinh.Ksans = data.KSans.ToList();
            lichTrinh.Changs = data.Changs.ToList();
            lichTrinh.DiaDiems = data.DiaDiems.ToList();
            lichTrinh.PhuongTiens = data.PhuongTiens.ToList();
            lichTrinh.AnUongs = data.AnUongs.ToList();


            return View(lichTrinh);

        }
        [HttpPost]
        public ActionResult CreateLichTrinh(FormCollection collection, LichTrinh s)
        {

            s.Ksans = data.KSans.ToList();
            s.Changs = data.Changs.ToList();
            s.DiaDiems = data.DiaDiems.ToList();
            s.PhuongTiens = data.PhuongTiens.ToList();
            s.AnUongs = data.AnUongs.ToList();

            

            var E_Ten = collection["TenLichTrinh"];
            var E_Gia = Convert.ToInt32(collection["GiaLichTrinh"]);
            
            if (string.IsNullOrEmpty(E_Ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.TenLichTrinh = E_Ten.ToString();
                s.GiaLichTrinh = E_Gia;
                
                data.LichTrinhs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return this.CreateLichTrinh();
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