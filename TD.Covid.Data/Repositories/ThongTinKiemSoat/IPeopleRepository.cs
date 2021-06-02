using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;

namespace TD.Covid.Data.Repositories.ThongTinKiemSoat
{
    public interface IPeopleRepository : IRepository<People>
    {
        People GetByIdentificationID(string identificationID);
        ICollection<People> Get(string areaCode, string chotKiemSoatID, string fromDate,
              string toDate, string provinceTo, string provinceFrom,
              string districtTo, string districtFrom, string wardTo, string wardFrom,
              string tinhTrangTheoDoiID, string skip, string top, string q);
        int Count(string areaCode, string chotKiemSoatIDStr, string fromDateStr, string toDateStr, string provinceTo, string provinceFrom, string districtTo, string districtFrom,
            string wardTo, string wardFrom, string tinhTrangTheoDoiIDStr, string q);
    }
}
