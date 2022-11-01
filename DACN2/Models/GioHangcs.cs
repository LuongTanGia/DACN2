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



        public int Matour { get; set; }
        public int max { get; set; }
        [Display(Name = "Tên Tour")]
        public string TenTour { get; set; }
        [Display(Name = "Ảnh bìa")]
        public string Hinh { get; set; }
        [Display(Name = "Giá Bán")]
        public Double Gia { get; set; }
        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return iSoluong * Gia; }
        }
        public GioHang(int id)
        {

            Matour = id;
            Tour tour = data.Tours.Single(n => n.ID == Matour);
            TenTour = tour.TenTour;
            max = (int)tour.SoCho;
            Hinh = tour.Hinh;
            Gia = double.Parse(tour.Gia.ToString());
            iSoluong = 1;
        }

    }
}