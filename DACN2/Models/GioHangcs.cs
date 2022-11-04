using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DACN2.Models
{
    public class GioHang
    {
        MyDataDataContext data = new MyDataDataContext();



        public int ID { get; set; }
        public int max { get; set; }
        public int maxte { get; set; }

        [Display(Name = "Tên Tour")]
        public string TenTour { get; set; }
        [Display(Name = "Ảnh bìa")]
        public string Hinh { get; set; }
        [Display(Name = "Giá Người Lớn")]
        public Double GiaNguoiLon { get; set; }
        [Display(Name = "Giá Trẻ Em")]
        public Double GiaTreEm { get; set; }
        [Display(Name = "Số lượng Người Lớn")]
        public int iSoluongNguoiLon { get; set; }
        [Display(Name = "Số lượng Trẻ Em")]
        public int iSoluongTreEm { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return (iSoluongNguoiLon*GiaNguoiLon)+(iSoluongTreEm*GiaTreEm) ; }
        }
        public GioHang(int id)
        {

            ID = id;
            Tour tour = data.Tours.Single(n => n.ID == ID);
            TenTour = tour.TenTour;
            max = (int)tour.SoCho;
            Hinh = tour.Hinh;
            GiaNguoiLon = double.Parse(tour.GiaNguoiLon.ToString());
            GiaTreEm = double.Parse(tour.GiaTreEm.ToString());
           
            iSoluongNguoiLon = 1;
            iSoluongTreEm = 0;
        }

    }
}