using ThucHanhWebMVC.Models;
namespace ThucHanhWebMVC.Repository
{
    public interface ILoaiSPRepository
    {
        TLoaiSp Add(TLoaiSp loaiSP);
        TLoaiSp Update(TLoaiSp loaiSP);
        TLoaiSp Delete(TLoaiSp maloaiSP);
       
        TLoaiSp GetLoaiSP(String maloaiSP);
        IEnumerable<TLoaiSp> GetAllLoaiSP();

    }
}
