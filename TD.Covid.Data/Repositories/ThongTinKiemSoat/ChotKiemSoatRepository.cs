using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.ThongTinKiemSoat
{
    public class ChotKiemSoatRepository : Repository<ChotKiemSoat>, IChotKiemSoatRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public ChotKiemSoatRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

        public string AddDanhSachNguoiDung(int id, List<string> danhSachNguoiDung)
        {
            var chotKiemSoat = GetById(id);

            var dsndRaw = "";
            foreach (var item in danhSachNguoiDung)
            {
                dsndRaw += item + ",";
            }

            dsndRaw = dsndRaw.Remove(dsndRaw.Length - 1);

            //chotKiemSoat.DanhSachNguoiDung = dsndRaw;

            Update(chotKiemSoat);
            return dsndRaw;
        }

        //public ICollection<string> GetDanhSachNguoiDung(int id)
        //{
        //    var nguoiDung = GetById(id);
        //    return nguoiDung.DanhSachNguoiDung.Split(',');
        //}
        //public int GetChotKiemSoatTheoNguoiDung(string nguoidung)
        //{
        //    if (String.IsNullOrEmpty(nguoidung))
        //    {
        //        nguoidung = Microsoft.SharePoint.SPContext.Current.Site.RootWeb.CurrentUser.LoginName.Split('|')[2];
        //    }
        //    int id = 0;
        //    var cks = _context.ChotKiemSoats.FirstOrDefault(x => x.DanhSachNguoiDung.Contains(nguoidung));
        //    if (cks!=null)
        //    {
        //        id = cks.ID;
        //    }
        //    return id;
        //}
    }
}
