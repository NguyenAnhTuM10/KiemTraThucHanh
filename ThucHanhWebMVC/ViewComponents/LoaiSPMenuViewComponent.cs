using ThucHanhWebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Repository;
namespace ThucHanhWebMVC.ViewComponents
{
    public class LoaiSPMenuViewComponent:ViewComponent
    {
        private readonly ILoaiSPRepository _loaiSP;

        public LoaiSPMenuViewComponent(ILoaiSPRepository loaiSPRepository)
        {
            _loaiSP = loaiSPRepository;
        }

        public IViewComponentResult Invoke()
        {
            var loaiSP = _loaiSP.GetAllLoaiSP().OrderBy(x => x.Loai);
            return View(loaiSP);
        }


    }
}
