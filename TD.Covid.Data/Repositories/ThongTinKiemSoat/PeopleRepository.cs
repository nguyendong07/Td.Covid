using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.ThongTinKiemSoat
{
    public class PeopleRepository : Repository<People>, IPeopleRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public PeopleRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

        public ICollection<People> Get(string areaCode, string chotKiemSoatIDStr, string fromDateStr, string toDateStr, string provinceTo, string provinceFrom, string districtTo, string districtFrom,
            string wardTo, string wardFrom, string tinhTrangTheoDoiIDStr, string skipStr, string topStr, string q)
        {
            var peoples = CreateQuery(areaCode, chotKiemSoatIDStr, fromDateStr, toDateStr, provinceTo, provinceFrom, districtTo, districtFrom, wardTo, wardFrom, tinhTrangTheoDoiIDStr, q);

            peoples = peoples.OrderBy(x => x.ID);

            if (int.TryParse(skipStr, out int skip))
            {
                peoples = peoples.Skip(skip);
            }
            else
            {
                peoples = peoples.Skip(0);
            }

            if (int.TryParse(topStr, out int top))
            {
                peoples = peoples.Take(top);
            }
            else
            {
                peoples = peoples.Take(20);
            }

            return peoples.ToList();
        }

        public int Count(string areaCode, string chotKiemSoatIDStr, string fromDateStr, string toDateStr, string provinceTo, string provinceFrom, string districtTo, string districtFrom,
            string wardTo, string wardFrom, string tinhTrangTheoDoiIDStr, string q)
        {
            var peoples = CreateQuery(areaCode, chotKiemSoatIDStr, fromDateStr, toDateStr, provinceTo, provinceFrom, districtTo, districtFrom, wardTo, wardFrom, tinhTrangTheoDoiIDStr, q);
            return peoples.Count();
        }

        private IQueryable<People> CreateQuery(string areaCode, string chotKiemSoatIDStr, string fromDateStr, string toDateStr, string provinceTo, string provinceFrom, string districtTo, string districtFrom,
            string wardTo, string wardFrom, string tinhTrangTheoDoiIDStr, string q)
        {
            var tokhais = _context.ToKhais.AsQueryable();

            if (!string.IsNullOrEmpty(areaCode))
            {
                tokhais = tokhais.Where(x => x.ProvinceCodeTo == areaCode || x.DistrictCodeTo == areaCode || x.WardCodeTo == areaCode);
            }

            if (int.TryParse(chotKiemSoatIDStr, out int chotKiemSoatID))
            {
                tokhais = tokhais.Where(x => x.ChotKiemSoatID == chotKiemSoatID);
            }

            var checkFromDate = DateTime.TryParse(fromDateStr, out DateTime fromDate);
            if (checkFromDate)
            {
                tokhais = tokhais.Where(x => x.CreatedAt >= fromDate);
            }

            var checkToDate = DateTime.TryParse(toDateStr, out DateTime toDate);
            if (checkToDate)
            {
                tokhais = tokhais.Where(x => x.CreatedAt <= toDate);
            }

            if (!string.IsNullOrEmpty(provinceTo))
            {
                tokhais = tokhais.Where(x => x.ProvinceCodeTo == provinceTo);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                tokhais = tokhais.Where(x => x.DistrictCodeTo == districtTo);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                tokhais = tokhais.Where(x => x.WardCodeTo == wardTo);
            }

            if (!string.IsNullOrEmpty(provinceFrom))
            {
                tokhais = tokhais.Where(x => x.ProvinceCodeFrom == provinceFrom);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                tokhais = tokhais.Where(x => x.DistrictCodeFrom == districtFrom);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                tokhais = tokhais.Where(x => x.WardCodeFrom == wardFrom);
            }

            var identificationIDs = tokhais.Select(x => x.IdentificationID).Distinct().ToList();

            var peoples = _context.Peoples.Where(x => identificationIDs.Contains(x.IdentificationID));

            if (int.TryParse(tinhTrangTheoDoiIDStr, out int tinhTrangTheoDoiID))
            {
                peoples = peoples.Where(x => x.TinhTrangTheoDoiID == tinhTrangTheoDoiID);
            }

            if (!string.IsNullOrEmpty(q))
            {
                peoples = peoples.Where(x => x.Name.Contains(q) || x.DiaChi.Contains(q));
            }

            return peoples;
        }

        public People GetByIdentificationID(string identificationID)
        {
            var people = _context.Peoples.FirstOrDefault(x => x.IdentificationID == identificationID);
            return people;
        }
    }
}
