using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Repository
{
   
   

    public class LoaiSPRepository : ILoaiSPRepository
    {
        private readonly QlbanVaLiContext _context;

        public LoaiSPRepository(QlbanVaLiContext context)
        {
            _context = context;
        }
        public TLoaiSp Add(TLoaiSp loaiSP)
        {
            _context.TLoaiSps.Add(loaiSP);
            _context.SaveChanges();
            return loaiSP;

        }

        public TLoaiSp Delete(TLoaiSp maloaiSP)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSP()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp GetLoaiSP(string maloaiSP)
        {
            return _context.TLoaiSps.Find(maloaiSP);
           }

        public TLoaiSp Update(TLoaiSp loaiSP)
        {
            _context.Update(loaiSP);
            _context.SaveChanges();
            return loaiSP;
        }
    }
}
