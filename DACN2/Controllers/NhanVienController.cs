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
            return PartialView(sptl);

        }
        public ActionResult ListTour()
        {
            var sptl = from ss in data.Tours select ss;
            return PartialView(sptl);

        }
    }
}