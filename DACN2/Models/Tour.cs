
namespace DACN2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public partial class Tour
    {

        
        public List<LoaiTour> LoaiTours = new List<LoaiTour>();
        public List<KSan> LoaiKsan = new List<KSan>();


        public List<HuongDanVien> HuongDanViens = new List<HuongDanVien>();
        public List<DiaDiem> DiaDiems = new List<DiaDiem>();
        public List<MayBay> MayBays = new List<MayBay>();
        public List<LichTrinh> LichTrinhs = new List<LichTrinh>();





    }
}