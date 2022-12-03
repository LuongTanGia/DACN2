using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN2.Models
{
    public partial class LichTrinh
    {


        public List<Chang> Changs = new List<Chang>();
        public List<PhuongTien> PhuongTiens = new List<PhuongTien>();
        public List<DiaDiem> DiaDiems = new List<DiaDiem>();
        public List<AnUong> AnUongs = new List<AnUong>();
        public List<KSan> Ksans = new List<KSan>();
    }

}