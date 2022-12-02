
namespace DACN2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public partial class Tour
    {
        public decimal TongChang { get; set; }
        public decimal LoiNhuan { get; set; }


        /*        
                public List<KSan> LoaiKsan = new List<KSan>();


                
                public List<DiaDiem> DiaDiems = new List<DiaDiem>();
                public List<PhuongTien> MayBays = new List<PhuongTien>();
                public List<LichTrinh> LichTrinhs = new List<LichTrinh>();
                public List<Chang> Chang = new List<Chang>();*/
        public List<NhanVien> HuongDanViens = new List<NhanVien>();
                public List<LoaiTour> LoaiTours = new List<LoaiTour>();





    }
}