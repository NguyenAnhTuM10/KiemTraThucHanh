using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThucHanhWebMVC.Models;
using ThucHanhWebMVC.Models.Authentication;
using X.PagedList;

namespace ThucHanhWebMVC.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db  = new QlbanVaLiContext(); // dong them vao

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[Authentication]
        public IActionResult Index(int? page)
        {
            //List<TDanhMucSp> listSP = db.TDanhMucSps.ToList(); // them vao
            //return View(listSP);

            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSP = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listSP, pageNumber, pageSize);
            return View(list);
        }
        //[Authentication]

        // hiển thi danh mục theo loại sp

        //public IActionResult SanPhamTheoLoai(String maloai)
        //{
        //    List<TDanhMucSp> list = db.TDanhMucSps.Where
        //        (x => x.MaLoai==maloai).OrderBy(x => x.TenSp).ToList();
        //    return View(list);
        //}



        // vừa hiện thị danh mục vừa phân trang nhỏ cho các danh mục nhỏ
        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham,pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }

        public IActionResult ChiTietSanPham(String maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
