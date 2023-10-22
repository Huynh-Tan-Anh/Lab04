using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;

namespace SachOnline.Controllers
{
    public class GioHangController : Controller
    {
        dbSachOnlineDataContext dbSachOnlineDataContext;
        // GET: GioHang
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Khởi tạo Giỏ hàng (giỏ hàng chưa tồn tại) 
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;

        }
        public ActionResult ThemGioHang(int ms, string url) 
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n=>n.iSachID==ms);
            if (sp == null)
            {
                sp = new GioHang(ms);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }

		public ActionResult XoaSPKhoiGioHang(int ms)
		{
			List<GioHang> lstGioHang = LayGioHang();
			GioHang sp = lstGioHang.SingleOrDefault(n => n.iSachID == ms);
			if (sp != null)
			{				
				lstGioHang.RemoveAll(n => n.iSachID == ms);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index","SachOnline");
                }
			}

            return RedirectToAction("GioHang");
		}

		public ActionResult CapNhapGioHang(int ms, FormCollection f)
		{
			List<GioHang> lstGioHang = LayGioHang();
			GioHang sp = lstGioHang.SingleOrDefault(n => n.iSachID == ms);
			if (sp != null)
			{
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
			}

			return RedirectToAction("GioHang");
		}

        public ActionResult XoaGioHang()
        {
			List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "SachOnline");
		}

		private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGIoHang = Session["GioHang"] as List <GioHang>;
            if(lstGIoHang != null)
            {
                iTongSoLuong = lstGIoHang.Sum(n=>n.iSoLuong);
            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dTongTien);
            }    
            return dTongTien;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();

        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
			if (Session["GioHang"] == null )
            {
                return RedirectToAction("Index", "SachOnline");
            }

            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = new KHACHHANG();
            List<GioHang> lstGioHang = LayGioHang();
            ddh.KhachHangID = kh.KhachHangID;
            ddh.NgayDat = DateTime.Now;
            var NgayGiao = String.Format("0:MM/dd/yyyy}");
        }
    }
}