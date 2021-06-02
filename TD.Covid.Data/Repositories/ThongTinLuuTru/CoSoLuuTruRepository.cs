using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ThongTinLuuTru;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.ThongTinLuuTru
{
    public class CoSoLuuTruRepository : Repository<CoSoLuuTru>, ICoSoLuuTruRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public CoSoLuuTruRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }
        //public int GetCoSoLuuTruTheoNguoiDung(string nguoidung)
        //{
        //    int id = 0;
        //    var cks = _context.CoSoLuuTrus.FirstOrDefault(x => x.DanhSachNguoiDung.Contains(nguoidung));
        //    if (cks != null)
        //    {
        //        id = cks.ID;
        //    }
        //    return id;
        //}
    }
}
