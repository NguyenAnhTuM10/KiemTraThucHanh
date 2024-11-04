using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucHanhWebMVC.Models;
using X.PagedList;

namespace ThucHanhWebMVC.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class AdminHomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        //public IActionResult DanhMucSanPham()
        //{
        //    var lstSanPham = db.TDanhMucSps.ToList();
        //    return View(lstSanPham);
        //}

        // cái này tương tư cái trên + thêm phân trang các table 
        public IActionResult DanhMucSanPham(int? page)
        {
            //List<TDanhMucSp> listSP = db.TDanhMucSps.ToList(); // them vao
            //return View(listSP);

            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //var listSP = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            var listSP = db.TDanhMucSps.OrderBy(sp => sp.MaSp).ToList();
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSP, pageNumber, pageSize);
            return View(list);

        }


        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");



            return View();
        }

        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanpham);
                db.SaveChanges();
                TempData["Message"] = " Thêm Sản Phẩm Thành Công! Vui Lòng Kiểm Tra Toàn Bộ Danh Sách ";
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanpham);
        }



        [Route("ChinhSuaSanPham")]
        [HttpGet]
        public IActionResult ChinhSuaSanPham(string masanpham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var SanPham = db.TDanhMucSps.Find(masanpham);


            return View(SanPham);
        }

        [Route("ChinhSuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChinhSuaSanPham(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Update(sanpham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanpham);
        }


        [Route("XoaSanPham")]
        [HttpGet]

        public IActionResult XoaSanPham(string masanpham)
        {
            // Kiểm tra các bản ghi liên quan trong bảng TChiTietSanPhams
            var chiTietSanPham = db.TChiTietSanPhams.Where(x => x.MaSp == masanpham).ToList();
            if (chiTietSanPham.Count > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này ";
                return RedirectToAction("DanhMucSanPham", "AdminHome");
            }

            // Tìm ảnh sản phẩm liên quan
            var anhSanPhams = db.TAnhSps.Where(x => x.MaSp == masanpham).ToList();
            if (anhSanPhams.Any())
            {
                db.RemoveRange(anhSanPhams);
            }

            // Tìm sản phẩm trong TDanhMucSps
            var sanPham = db.TDanhMucSps.Find(masanpham);
            if (sanPham != null)
            {
                db.Remove(sanPham);
            }
            else
            {
                TempData["Message"] = "Sản phẩm không tồn tại.";
                return RedirectToAction("DanhMucSanPham", "AdminHome");
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            TempData["Message"] = "Sản phẩm đã được xóa.";
            return RedirectToAction("DanhMucSanPham", "AdminHome");
        }


        //    public IActionResult XoaSanPham(string masanpham)
        //    {
        //        var chiTietSanPham = db.TChiTietSanPhams.Where(x => x.MaSp == masanpham).ToList();

        //        if(chiTietSanPham.Count > 0)
        //        {
        //            TempData["Message"] = "Không xóa được sản phẩm này ";
        //            return RedirectToAction("DanhMucSanPham", "HomeAdmin");



        //        }

        //        var anhSanPhams = db.TAnhSps.Where(x => x.MaSp == masanpham);

        //        if (anhSanPhams.Any())
        //            db.RemoveRange(anhSanPhams);

        //            db.Remove(db.TDanhMucSps.Find(masanpham));
        //            db.SaveChanges();

        //            TempData["Message"] = "Sản Phẩm đã được xóa ";
        //            return RedirectToAction("DanhMucSanPham", "HomeAdmin");


        //    }
        //}
    }
}
