using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Covid.Data.Repositories.ThongTinKiemSoat;
using TD.Covid.Data.ViewModels;
using TD.Core.Api.Mvc;
using TD.Core.UserProfiles.Controllers;
using TD.Core.UserProfiles.Models;

namespace TD.Covid.Data.Repositories.ToKhaiYTe
{
    public class ToKhaiRepository : Repository<ToKhai>, IToKhaiRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        private readonly IAreaRepository _areaRepository;
        private readonly IToKhaiTrieuChungRepository _toKhaiTrieuChungRepository;
        private readonly IToKhaiBenhNenRepository _toKhaiBenhNenRepository;
        private readonly ITrieuChungRepository _trieuChungRepository;
        private readonly IBenhNenRepository _benhNenRepository;

        public ToKhaiRepository(CovidDataContext context,
            ICoreContextAccessor coreContextAccessor,
            IAreaRepository areaRepository,
            IToKhaiTrieuChungRepository toKhaiTrieuChungRepository,
            IToKhaiBenhNenRepository toKhaiBenhNenRepository,
            ITrieuChungRepository trieuChungRepository,
            IBenhNenRepository benhNenRepository
            )
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
            _areaRepository = areaRepository;
            _toKhaiTrieuChungRepository = toKhaiTrieuChungRepository;
            _trieuChungRepository = trieuChungRepository;
        }



        public ICollection<ToKhaiBase> GetAllBase(
            string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiIDStr, string phuongTienIDStr, string bienSo, string skipStr, string topStr, string q,
            string fromNgayKhoiHanhStr, string toNgayKhoiHanhStr, string fromNgayToiStr, string toNgayToiStr, string chotKiemSoatIDStr)
        {
            var query = _context.ToKhais.AsQueryable();

            query = query
                .Include(x => x.PhuongTien)
                .Include("TrangThaiToKhai")
                .OrderByDescending(x => x.ID);

            if (!string.IsNullOrEmpty(identificationID))
            {
                query = query.Where(x => x.IdentificationID == identificationID);
            }

            if (!string.IsNullOrEmpty(provinceTo))
            {
                query = query.Where(x => x.ProvinceCodeTo == provinceTo);
            }

            if (!string.IsNullOrEmpty(provinceFrom))
            {
                query = query.Where(x => x.ProvinceCodeFrom == provinceFrom);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                query = query.Where(x => x.DistrictCodeTo == districtTo);
            }

            if (!string.IsNullOrEmpty(districtFrom))
            {
                query = query.Where(x => x.DistrictCodeFrom == districtFrom);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                query = query.Where(x => x.WardCodeTo == wardTo);
            }

            if (!string.IsNullOrEmpty(wardFrom))
            {
                query = query.Where(x => x.WardCodeFrom == wardFrom);
            }

            var checkTrangThaiToKhai = int.TryParse(trangThaiToKhaiIDStr, out int trangThaiToKhaiID);
            if (checkTrangThaiToKhai)
            {
                query = query.Where(x => x.TrangThaiToKhaiID == trangThaiToKhaiID);
            }

            var checkPhuongTien = int.TryParse(phuongTienIDStr, out int phuongTienID);
            if (checkPhuongTien)
            {
                query = query.Where(x => x.PhuongTienID == phuongTienID);
            }

            var checkChotKiemSoat = int.TryParse(chotKiemSoatIDStr, out int chotKiemSoatID);
            if (checkChotKiemSoat)
            {
                query = query.Where(x => x.ChotKiemSoatID == chotKiemSoatID);
            }

            if (!string.IsNullOrEmpty(bienSo))
            {
                query = query.Where(x => x.BienSo == bienSo);
            }

            var checkFromNgayKhoiHanh = DateTime.TryParse(fromNgayKhoiHanhStr, out DateTime fromNgayKhoiHanh);
            if (checkFromNgayKhoiHanh)
            {
                query = query.Where(x => x.NgayKhoiHanh >= fromNgayKhoiHanh);
            }

            var checkToNgayKhoiHanh = DateTime.TryParse(toNgayKhoiHanhStr, out DateTime toNgayKhoiHanh);
            if (checkToNgayKhoiHanh)
            {
                query = query.Where(x => x.NgayKhoiHanh <= toNgayKhoiHanh);
            }

            var checkFromNgayToi = DateTime.TryParse(fromNgayToiStr, out DateTime fromNgayToi);
            if (checkFromNgayKhoiHanh)
            {
                query = query.Where(x => x.NgayToi >= fromNgayToi);
            }

            var checkToNgayToi = DateTime.TryParse(toNgayToiStr, out DateTime toNgayToi);
            if (checkToNgayToi)
            {
                query = query.Where(x => x.NgayToi <= toNgayToi);
            }

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(x => x.Name.Contains(q) || x.DiaChi.Contains(q) || x.DiaChiDen.Contains(q));
            }

            // skip, top
            query = query.OrderByDescending(x => x.ID);

            var checkSkip = int.TryParse(skipStr, out int skip);
            if (checkSkip)
            {
                query = query.Skip(skip);
            } else
            {
                query = query.Skip(0);
            }

            var checkTop = int.TryParse(topStr, out int top);
            if (checkTop)
            {
                query = query.Take(top);
            }
            else
            {
                query = query.Take(20);
            }

            var tokhais = query.ToList();



            // ToKhai to ToKhaiBase
            var toKhaiBases = new List<ToKhaiBase>();

            foreach (var item in tokhais)
            {
                var provinceTo1 = _areaRepository.GetByCode(item.ProvinceCodeTo);
                var districtTo1 = _areaRepository.GetByCode(item.DistrictCodeTo);
                var wardTo1 = _areaRepository.GetByCode(item.WardCodeTo);

                var provinceFrom1 = _areaRepository.GetByCode(item.ProvinceCodeFrom);
                var districtFrom1 = _areaRepository.GetByCode(item.DistrictCodeFrom);
                var wardFrom1 = _areaRepository.GetByCode(item.WardCodeFrom);

                var diaDiemToi = (provinceTo1 == null ? "" : provinceTo1.Name) + ", " +
                    (districtTo1 == null ? "" : districtTo1.Name) + ", " +
                    (wardTo1 == null ? "" : wardTo1.Name);
                var diaDiemKhoiHanh = (provinceFrom1 == null ? "" : provinceFrom1.Name) + ", " +
                    (districtFrom1 == null ? "" : districtFrom1.Name) + ", " +
                    (wardFrom1 == null ? "" : wardFrom1.Name);

                var toKhaiBase = new ToKhaiBase
                {
                    ID = item.ID, 
                    SoDienThoai = item.DienThoai,
                    TenPhuongTien = item.PhuongTien == null ? string.Empty : item.PhuongTien.Name,
                    NgayToi = item.NgayToi,
                    NgayKhoiHanh = item.NgayKhoiHanh,
                    DiaDiemToi = diaDiemToi,
                    DiaDiemKhoiHanh = diaDiemKhoiHanh,
                    Name = item.Name,
                    TrangThaiToKhai = item.TrangThaiToKhai,
                    NguoiKiemSoatName = item.NguoiKiemSoatName,
                    IdentificationID = item.IdentificationID,
                    ThoiGianTao = item.CreatedAt
                };

                toKhaiBases.Add(toKhaiBase);
            }

            return toKhaiBases;
        }

        private IQueryable<ToKhai> CreateQuery(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiIDStr, string phuongTienIDStr, string bienSo,
            string fromNgayKhoiHanhStr, string toNgayKhoiHanhStr, string fromNgayToiStr, 
            string toNgayToiStr, string chotKiemSoatIDStr, string areaCode,
            string fromNgayTaoStr, string toNgayTaoStr, string diTuVungDich, string q)
        {
            var query = _context.ToKhais.AsQueryable();

            query = query
                .Include(x => x.PhuongTien)
                .Include("TrangThaiToKhai")
                .OrderByDescending(x => x.ID);

            if (!string.IsNullOrEmpty(identificationID))
            {
                query = query.Where(x => x.IdentificationID == identificationID);
            }

            if (!string.IsNullOrEmpty(diTuVungDich) && (diTuVungDich == "1" || string.Equals(diTuVungDich, "true", StringComparison.OrdinalIgnoreCase)))
            {
                query = query.Where(x => x.DenTuVungDich == true);
            }


            if (!string.IsNullOrEmpty(areaCode))
            {
                query = query.Where(x => x.ProvinceCodeTo == areaCode || x.DistrictCodeTo == areaCode || x.WardCodeTo == areaCode);
            }

            if (!string.IsNullOrEmpty(provinceTo))
            {
                query = query.Where(x => x.ProvinceCodeTo == provinceTo);
            }

            if (!string.IsNullOrEmpty(provinceFrom))
            {
                query = query.Where(x => x.ProvinceCodeFrom == provinceFrom);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                query = query.Where(x => x.DistrictCodeTo == districtTo);
            }

            if (!string.IsNullOrEmpty(districtFrom))
            {
                query = query.Where(x => x.DistrictCodeFrom == districtFrom);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                query = query.Where(x => x.WardCodeTo == wardTo);
            }

            if (!string.IsNullOrEmpty(wardFrom))
            {
                query = query.Where(x => x.WardCodeFrom == wardFrom);
            }

            var checkTrangThaiToKhai = int.TryParse(trangThaiToKhaiIDStr, out int trangThaiToKhaiID);
            if (checkTrangThaiToKhai)
            {
                query = query.Where(x => x.TrangThaiToKhaiID == trangThaiToKhaiID);
            }

            var checkPhuongTien = int.TryParse(phuongTienIDStr, out int phuongTienID);
            if (checkPhuongTien)
            {
                query = query.Where(x => x.PhuongTienID == phuongTienID);
            }

            var checkChotKiemSoat = int.TryParse(chotKiemSoatIDStr, out int chotKiemSoatID);
            if (checkChotKiemSoat)
            {
                query = query.Where(x => x.ChotKiemSoatID == chotKiemSoatID);
            }

            if (!string.IsNullOrEmpty(bienSo))
            {
                query = query.Where(x => x.BienSo == bienSo);
            }

            var checkFromNgayKhoiHanh = DateTime.TryParse(fromNgayKhoiHanhStr, out DateTime fromNgayKhoiHanh);
            if (checkFromNgayKhoiHanh)
            {
                query = query.Where(x => x.NgayKhoiHanh >= fromNgayKhoiHanh);
            }

            var checkToNgayKhoiHanh = DateTime.TryParse(toNgayKhoiHanhStr, out DateTime toNgayKhoiHanh);
            if (checkToNgayKhoiHanh)
            {
                query = query.Where(x => x.NgayKhoiHanh <= toNgayKhoiHanh);
            }

            var checkFromNgayToi = DateTime.TryParse(fromNgayToiStr, out DateTime fromNgayToi);
            if (checkFromNgayKhoiHanh)
            {
                query = query.Where(x => x.NgayToi >= fromNgayToi);
            }



            var checkToNgayTao = DateTime.TryParse(toNgayTaoStr, out DateTime toNgayTao);
            if (checkToNgayTao)
            {
                query = query.Where(x => x.CreatedAt <= toNgayTao);
            }

            var checkFromNgayTao = DateTime.TryParse(fromNgayTaoStr, out DateTime fromNgayTao);
            if (checkFromNgayTao)
            {
                query = query.Where(x => x.CreatedAt >= fromNgayTao);
            }




            var checkToNgayToi = DateTime.TryParse(toNgayToiStr, out DateTime toNgayToi);
            if (checkToNgayToi)
            {
                query = query.Where(x => x.NgayToi <= toNgayToi);
            }

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(x => x.Name.Contains(q) || x.DiaChi.Contains(q) || x.DiaChiDen.Contains(q));
            }

            return query;
        }

        public int Count(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiIDStr, string phuongTienIDStr, string bienSo,
            string fromNgayKhoiHanhStr, string toNgayKhoiHanhStr, string fromNgayToiStr,
            string toNgayToiStr, string chotKiemSoatIDStr, string areaCode,
            string fromNgayTaoStr, string toNgayTaoStr, string diTuVungDich, string q)
        {
            var query = CreateQuery(identificationID, provinceTo, provinceFrom, districtTo,
                districtFrom, wardTo, wardFrom, trangThaiToKhaiIDStr, phuongTienIDStr, bienSo,
                fromNgayKhoiHanhStr, toNgayKhoiHanhStr, fromNgayToiStr, toNgayToiStr, chotKiemSoatIDStr,
                areaCode, fromNgayTaoStr, toNgayTaoStr, diTuVungDich, q);

            return query.Count();
        }

        public ICollection<ToKhaiBase> GetAllBase(
            string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiIDStr, string phuongTienIDStr, string bienSo, string skipStr, string topStr, string q,
            string fromNgayKhoiHanhStr, string toNgayKhoiHanhStr, string fromNgayToiStr, string toNgayToiStr, string chotKiemSoatIDStr, string areaCode, string fromNgayTaoStr, string toNgayTaoStr, string diTuVungDich)
        {
            var query = CreateQuery(identificationID, provinceTo, provinceFrom, districtTo, 
                districtFrom, wardTo, wardFrom, trangThaiToKhaiIDStr, phuongTienIDStr, bienSo, 
                fromNgayKhoiHanhStr, toNgayKhoiHanhStr, fromNgayToiStr, toNgayToiStr, chotKiemSoatIDStr, 
                areaCode, fromNgayTaoStr, toNgayTaoStr, diTuVungDich, q);

            // skip, top
            query = query.OrderByDescending(x => x.ID);

            var checkSkip = int.TryParse(skipStr, out int skip);
            if (checkSkip)
            {
                query = query.Skip(skip);
            }
            else
            {
                query = query.Skip(0);
            }

            var checkTop = int.TryParse(topStr, out int top);
            if (checkTop)
            {
                query = query.Take(top);
            }
            else
            {
                query = query.Take(20);
            }
            var tokhais = query.ToList();



            // ToKhai to ToKhaiBase
            var toKhaiBases = new List<ToKhaiBase>();

            foreach (var item in tokhais)
            {
                var provinceTo1 = _areaRepository.GetByCode(item.ProvinceCodeTo);
                var districtTo1 = _areaRepository.GetByCode(item.DistrictCodeTo);
                var wardTo1 = _areaRepository.GetByCode(item.WardCodeTo);

                var provinceFrom1 = _areaRepository.GetByCode(item.ProvinceCodeFrom);
                var districtFrom1 = _areaRepository.GetByCode(item.DistrictCodeFrom);
                var wardFrom1 = _areaRepository.GetByCode(item.WardCodeFrom);

                var diaDiemToi = (provinceTo1 == null ? "" : provinceTo1.Name) + ", " +
                    (districtTo1 == null ? "" : districtTo1.Name) + ", " +
                    (wardTo1 == null ? "" : wardTo1.Name);
                var diaDiemKhoiHanh = (provinceFrom1 == null ? "" : provinceFrom1.Name) + ", " +
                    (districtFrom1 == null ? "" : districtFrom1.Name) + ", " +
                    (wardFrom1 == null ? "" : wardFrom1.Name);

                var toKhaiBase = new ToKhaiBase
                {
                    ID = item.ID,
                    SoDienThoai = item.DienThoai,
                    TenPhuongTien = item.PhuongTien == null ? string.Empty : item.PhuongTien.Name,
                    NgayToi = item.NgayToi,
                    NgayKhoiHanh = item.NgayKhoiHanh,
                    DiaDiemToi = diaDiemToi,
                    DiaDiemKhoiHanh = diaDiemKhoiHanh,
                    Name = item.Name,
                    TrangThaiToKhai = item.TrangThaiToKhai,
                    NguoiKiemSoatName = item.NguoiKiemSoatName,
                    IdentificationID = item.IdentificationID,
                    ThoiGianTao = item.CreatedAt
                };

                toKhaiBases.Add(toKhaiBase);
            }

            return toKhaiBases;
        }

        public ToKhai GetByIdIncludeFull(int id)
        {
            var toKhai = _context.ToKhais
                .Include("TrangThaiToKhai")
                .Include("ChotKiemSoat")
                .Include("PhuongTien")
                .Include("QuocTich")
                .FirstOrDefault(x => x.ID == id);

            toKhai.Province = _areaRepository.GetByCode(toKhai.ProvinceCode);
            toKhai.District = _areaRepository.GetByCode(toKhai.DistrictCode);
            toKhai.Ward = _areaRepository.GetByCode(toKhai.WardCode);
            toKhai.ProvinceFrom = _areaRepository.GetByCode(toKhai.ProvinceCodeFrom);
            toKhai.DistrictFrom = _areaRepository.GetByCode(toKhai.DistrictCodeFrom);
            toKhai.WardFrom = _areaRepository.GetByCode(toKhai.WardCodeFrom);
            toKhai.ProvinceTo = _areaRepository.GetByCode(toKhai.ProvinceCodeTo);
            toKhai.DistrictTo = _areaRepository.GetByCode(toKhai.DistrictCodeTo);
            toKhai.WardTo = _areaRepository.GetByCode(toKhai.WardCodeTo);

            var tktcs = _toKhaiTrieuChungRepository.GetByToKhaiId(toKhai.ID);
            toKhai.TrieuChungs = new List<TrieuChung>();
            foreach (var item in tktcs)
            {
                var trieuChung = _trieuChungRepository.GetById(item.TrieuChungId);
                toKhai.TrieuChungs.Add(trieuChung); 
            }

            //var tkbns = _toKhaiBenhNenRepository.GetByToKhaiId(toKhaiId);
            //toKhai.BenhNens = new List<BenhNen>();
            //foreach (var item in tkbns)
            //{
            //    var benhNen = _benhNenRepository.GetById(item.BenhNenId);
            //    toKhai.BenhNens.Add(benhNen);
            //}

            return toKhai;
        }

        public ICollection<ToKhai> GetByTrangThai(string trangthai,string areaCode)
        {
            var query = _context.ToKhais.AsQueryable();
            if (trangthai!="")
            {
                query = query.Where(x => x.TrangThaiToKhai.Name == trangthai);
            }
            if (!String.IsNullOrEmpty(areaCode))
            {
                query = query.Where(x => x.ProvinceCodeTo == areaCode || x.DistrictCodeTo == areaCode || x.WardCodeTo == areaCode);
            }
            return query.ToList();
        }

        public ICollection<ToKhai> GetByCreatedAt(DateTime created, string trangthai,string areaCode)
        {
            var query = _context.ToKhais.AsQueryable();
            if (trangthai != "" && created == null)
            {
                query = query.Where(x => x.TrangThaiToKhai.Name == trangthai);
            }
            if (created != null)
            {
                DateTime startDate = new DateTime(created.Year, created.Month, created.Day, 0, 0, 0);
                DateTime endDate = startDate.AddDays(1);
                if (trangthai == "")
                {
                    query = query.Where(x => x.CreatedAt >= startDate 
                    && x.CreatedAt < endDate);
                }
                if (trangthai != "")
                {
                    query = query.Where(x => x.TrangThaiToKhai.Name == trangthai
                    && x.CreatedAt >= startDate
                    && x.CreatedAt < endDate);
                }
            }
            if (!String.IsNullOrEmpty(areaCode))
            {
                query = query.Where(x => x.ProvinceCodeTo == areaCode || x.DistrictCodeTo == areaCode || x.WardCodeTo == areaCode);
            }
            else
            {
                areaCode = GetAreaCodeByCurrentUser();
                query = query.Where(x => x.ProvinceCodeTo == areaCode || x.DistrictCodeTo == areaCode || x.WardCodeTo == areaCode);
            }

            return query.ToList();
        }

        public ICollection<ToKhaiBase> GetTuNgayDenngay(string identificationID, string provinceTo, string provinceFrom, string districtTo, string districtFrom, string wardTo, string wardFrom, string trangThaiToKhaiIDStr, string fromNgayKhoiHanh, string toNgayKhoiHanh)
        {
            var query = _context.ToKhais.AsQueryable();

            query = query
                .Include(x => x.PhuongTien)
                .Include("TrangThaiToKhai")
                .OrderByDescending(x => x.ID);

            if (!string.IsNullOrEmpty(identificationID))
            {
                query = query.Where(x => x.IdentificationID == identificationID);
            }

            if (!string.IsNullOrEmpty(provinceTo))
            {
                query = query.Where(x => x.ProvinceCodeTo == provinceTo);
            }

            if (!string.IsNullOrEmpty(provinceFrom))
            {
                query = query.Where(x => x.ProvinceCodeFrom == provinceFrom);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                query = query.Where(x => x.DistrictCodeTo == districtTo);
            }

            if (!string.IsNullOrEmpty(districtFrom))
            {
                query = query.Where(x => x.DistrictCodeFrom == districtFrom);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                query = query.Where(x => x.WardCodeTo == wardTo);
            }

            if (!string.IsNullOrEmpty(wardFrom))
            {
                query = query.Where(x => x.WardCodeFrom == wardFrom);
            }
            var checkTrangThaiToKhai = int.TryParse(trangThaiToKhaiIDStr, out int trangThaiToKhaiID);
            if (checkTrangThaiToKhai)
            {
                query = query.Where(x => x.TrangThaiToKhaiID == trangThaiToKhaiID);
            }

            var checkFromNgayToi = DateTime.TryParse(fromNgayKhoiHanh, out DateTime fromNgayToi);
            if (checkFromNgayToi)
            {
                query = query.Where(x => x.CreatedAt >= fromNgayToi);
            }

            var checkToNgayToi = DateTime.TryParse(toNgayKhoiHanh, out DateTime toNgayToi);
            if (checkToNgayToi)
            {
                query = query.Where(x => x.CreatedAt <= toNgayToi);
            }

            // skip, top
            query = query.OrderBy(x => x.ID);

            var tokhais = query.ToList();



            // ToKhai to ToKhaiBase
            var toKhaiBases = new List<ToKhaiBase>();

            foreach (var item in tokhais)
            {
                var provinceTo1 = _areaRepository.GetByCode(item.ProvinceCodeTo);
                var districtTo1 = _areaRepository.GetByCode(item.DistrictCodeTo);
                var wardTo1 = _areaRepository.GetByCode(item.WardCodeTo);

                var provinceFrom1 = _areaRepository.GetByCode(item.ProvinceCodeFrom);
                var districtFrom1 = _areaRepository.GetByCode(item.DistrictCodeFrom);
                var wardFrom1 = _areaRepository.GetByCode(item.WardCodeFrom);

                var diaDiemToi = (provinceTo1 == null ? "" : provinceTo1.Name) + ", " +
                    (districtTo1 == null ? "" : districtTo1.Name) + ", " +
                    (wardTo1 == null ? "" : wardTo1.Name);
                var diaDiemKhoiHanh = (provinceFrom1 == null ? "" : provinceFrom1.Name) + ", " +
                    (districtFrom1 == null ? "" : districtFrom1.Name) + ", " +
                    (wardFrom1 == null ? "" : wardFrom1.Name);

                var toKhaiBase = new ToKhaiBase
                {
                    ID = item.ID,
                    SoDienThoai = item.DienThoai,
                    TenPhuongTien = item.PhuongTien == null ? string.Empty : item.PhuongTien.Name,
                    NgayToi = item.NgayToi,
                    NgayKhoiHanh = item.NgayKhoiHanh,
                    DiaDiemToi = diaDiemToi,
                    DiaDiemKhoiHanh = diaDiemKhoiHanh,
                    Name = item.Name,
                    TrangThaiToKhai = item.TrangThaiToKhai,
                    NguoiKiemSoatName = item.NguoiKiemSoatName,
                    IdentificationID = item.IdentificationID,
                    ThoiGianTao = item.CreatedAt
                };

                toKhaiBases.Add(toKhaiBase);
            }

            return toKhaiBases;
        }


        public ICollection<ToKhaiBase> GetTuNgayDenngay(string identificationID, string provinceTo, string provinceFrom, string districtTo, string districtFrom, string wardTo, string wardFrom, string trangThaiToKhaiIDStr, string fromNgayKhoiHanh, string toNgayKhoiHanh, string diTuVungDich)
        {
            var query = _context.ToKhais.AsQueryable();

            query = query
                .Include(x => x.PhuongTien)
                .Include("TrangThaiToKhai")
                .OrderByDescending(x => x.ID);

            if (!string.IsNullOrEmpty(identificationID))
            {
                query = query.Where(x => x.IdentificationID == identificationID);
            }

            if (!string.IsNullOrEmpty(diTuVungDich) && (diTuVungDich == "1" || string.Equals(diTuVungDich, "true", StringComparison.OrdinalIgnoreCase)))
            {
                query = query.Where(x => x.DenTuVungDich == true);
            }

            if (!string.IsNullOrEmpty(provinceTo))
            {
                query = query.Where(x => x.ProvinceCodeTo == provinceTo);
            }

            if (!string.IsNullOrEmpty(provinceFrom))
            {
                query = query.Where(x => x.ProvinceCodeFrom == provinceFrom);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                query = query.Where(x => x.DistrictCodeTo == districtTo);
            }

            if (!string.IsNullOrEmpty(districtFrom))
            {
                query = query.Where(x => x.DistrictCodeFrom == districtFrom);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                query = query.Where(x => x.WardCodeTo == wardTo);
            }

            if (!string.IsNullOrEmpty(wardFrom))
            {
                query = query.Where(x => x.WardCodeFrom == wardFrom);
            }
            var checkTrangThaiToKhai = int.TryParse(trangThaiToKhaiIDStr, out int trangThaiToKhaiID);
            if (checkTrangThaiToKhai)
            {
                query = query.Where(x => x.TrangThaiToKhaiID == trangThaiToKhaiID);
            }

            var checkFromNgayToi = DateTime.TryParse(fromNgayKhoiHanh, out DateTime fromNgayToi);
            if (checkFromNgayToi)
            {
                query = query.Where(x => x.CreatedAt >= fromNgayToi);
            }

            var checkToNgayToi = DateTime.TryParse(toNgayKhoiHanh, out DateTime toNgayToi);
            if (checkToNgayToi)
            {
                query = query.Where(x => x.CreatedAt <= toNgayToi);
            }

            // skip, top
            query = query.OrderBy(x => x.ID);

            var tokhais = query.ToList();



            // ToKhai to ToKhaiBase
            var toKhaiBases = new List<ToKhaiBase>();

            foreach (var item in tokhais)
            {
                var provinceTo1 = _areaRepository.GetByCode(item.ProvinceCodeTo);
                var districtTo1 = _areaRepository.GetByCode(item.DistrictCodeTo);
                var wardTo1 = _areaRepository.GetByCode(item.WardCodeTo);

                var provinceFrom1 = _areaRepository.GetByCode(item.ProvinceCodeFrom);
                var districtFrom1 = _areaRepository.GetByCode(item.DistrictCodeFrom);
                var wardFrom1 = _areaRepository.GetByCode(item.WardCodeFrom);

                var diaDiemToi = (provinceTo1 == null ? "" : provinceTo1.Name) + ", " +
                    (districtTo1 == null ? "" : districtTo1.Name) + ", " +
                    (wardTo1 == null ? "" : wardTo1.Name);
                var diaDiemKhoiHanh = (provinceFrom1 == null ? "" : provinceFrom1.Name) + ", " +
                    (districtFrom1 == null ? "" : districtFrom1.Name) + ", " +
                    (wardFrom1 == null ? "" : wardFrom1.Name);

                var toKhaiBase = new ToKhaiBase
                {
                    ID = item.ID,
                    SoDienThoai = item.DienThoai,
                    TenPhuongTien = item.PhuongTien == null ? string.Empty : item.PhuongTien.Name,
                    NgayToi = item.NgayToi,
                    NgayKhoiHanh = item.NgayKhoiHanh,
                    DiaDiemToi = diaDiemToi,
                    DiaDiemKhoiHanh = diaDiemKhoiHanh,
                    Name = item.Name,
                    TrangThaiToKhai = item.TrangThaiToKhai,
                    NguoiKiemSoatName = item.NguoiKiemSoatName,
                    IdentificationID = item.IdentificationID,
                    ThoiGianTao = item.CreatedAt
                };

                toKhaiBases.Add(toKhaiBase);
            }

            return toKhaiBases;
        }


        public ICollection<ToKhaiBase> GetTuNgayDenngay(string identificationID, string provinceTo, string provinceFrom, string districtTo, string districtFrom, string wardTo, string wardFrom, string trangThaiToKhaiIDStr, string fromNgayKhoiHanh, string toNgayKhoiHanh, string chotKiemSoatID, string diTuVungDich)
        {
            var query = _context.ToKhais.AsQueryable();

            query = query
                .Include(x => x.PhuongTien)
                .Include("TrangThaiToKhai")
                .OrderByDescending(x => x.ID);

            if (!string.IsNullOrEmpty(identificationID))
            {
                query = query.Where(x => x.IdentificationID == identificationID);
            }

            if (!string.IsNullOrEmpty(diTuVungDich) && (diTuVungDich == "1" || string.Equals(diTuVungDich, "true", StringComparison.OrdinalIgnoreCase)))
            {
                query = query.Where(x => x.DenTuVungDich == true);
            }

            if (!string.IsNullOrEmpty(provinceTo))
            {
                query = query.Where(x => x.ProvinceCodeTo == provinceTo);
            }

            if (!string.IsNullOrEmpty(provinceFrom))
            {
                query = query.Where(x => x.ProvinceCodeFrom == provinceFrom);
            }

            if (!string.IsNullOrEmpty(districtTo))
            {
                query = query.Where(x => x.DistrictCodeTo == districtTo);
            }

            if (!string.IsNullOrEmpty(districtFrom))
            {
                query = query.Where(x => x.DistrictCodeFrom == districtFrom);
            }

            if (!string.IsNullOrEmpty(wardTo))
            {
                query = query.Where(x => x.WardCodeTo == wardTo);
            }

            if (!string.IsNullOrEmpty(wardFrom))
            {
                query = query.Where(x => x.WardCodeFrom == wardFrom);
            }
            var checkTrangThaiToKhai = int.TryParse(trangThaiToKhaiIDStr, out int trangThaiToKhaiID);
            if (checkTrangThaiToKhai)
            {
                query = query.Where(x => x.TrangThaiToKhaiID == trangThaiToKhaiID);
            }

            var checkFromNgayToi = DateTime.TryParse(fromNgayKhoiHanh, out DateTime fromNgayToi);
            if (checkFromNgayToi)
            {
                query = query.Where(x => x.CreatedAt >= fromNgayToi);
            }

            var checkToNgayToi = DateTime.TryParse(toNgayKhoiHanh, out DateTime toNgayToi);
            if (checkToNgayToi)
            {
                query = query.Where(x => x.CreatedAt <= toNgayToi);
            }

            if (int.TryParse(chotKiemSoatID, out int chotKiemSoatIdint))
            {
                query = query.Where(x => x.ChotKiemSoatID == chotKiemSoatIdint);
            }
            // skip, top
            query = query.OrderBy(x => x.ID);

            var tokhais = query.ToList();



            // ToKhai to ToKhaiBase
            var toKhaiBases = new List<ToKhaiBase>();

            foreach (var item in tokhais)
            {
                var provinceTo1 = _areaRepository.GetByCode(item.ProvinceCodeTo);
                var districtTo1 = _areaRepository.GetByCode(item.DistrictCodeTo);
                var wardTo1 = _areaRepository.GetByCode(item.WardCodeTo);

                var provinceFrom1 = _areaRepository.GetByCode(item.ProvinceCodeFrom);
                var districtFrom1 = _areaRepository.GetByCode(item.DistrictCodeFrom);
                var wardFrom1 = _areaRepository.GetByCode(item.WardCodeFrom);

                var diaDiemToi = (provinceTo1 == null ? "" : provinceTo1.Name) + ", " +
                    (districtTo1 == null ? "" : districtTo1.Name) + ", " +
                    (wardTo1 == null ? "" : wardTo1.Name);
                var diaDiemKhoiHanh = (provinceFrom1 == null ? "" : provinceFrom1.Name) + ", " +
                    (districtFrom1 == null ? "" : districtFrom1.Name) + ", " +
                    (wardFrom1 == null ? "" : wardFrom1.Name);

                var toKhaiBase = new ToKhaiBase
                {
                    ID = item.ID,
                    SoDienThoai = item.DienThoai,
                    TenPhuongTien = item.PhuongTien == null ? string.Empty : item.PhuongTien.Name,
                    NgayToi = item.NgayToi,
                    NgayKhoiHanh = item.NgayKhoiHanh,
                    DiaDiemToi = diaDiemToi,
                    DiaDiemKhoiHanh = diaDiemKhoiHanh,
                    Name = item.Name,
                    TrangThaiToKhai = item.TrangThaiToKhai,
                    NguoiKiemSoatName = item.NguoiKiemSoatName,
                    IdentificationID = item.IdentificationID,
                    ThoiGianTao = item.CreatedAt
                };

                toKhaiBases.Add(toKhaiBase);
            }

            return toKhaiBases;
        }

        public string GetAreaCodeByCurrentUser()
        {
            string areaCode = string.Empty;
            string urlRoot = SPContext.Current.Site.RootWeb.Url;
            using (SPSite oSite = new SPSite(urlRoot))
            {
                var webApp = oSite.WebApplication;
                var zone = oSite.Zone;

                UserProfileController userProfileCtrlr = new UserProfileController(webApp, zone);
                UserProfile obj = userProfileCtrlr.GetByCurrentUser();
                areaCode = obj.AreaCode;
            }
            return areaCode;
        }

        public ICollection<ToKhai> GetByTrangThai(int trangthaiToKhaiID)
        {
            var curArea = GetAreaCodeByCurrentUser();

            var query = _context.ToKhais.AsQueryable();
            query = query.Where(x => x.TrangThaiToKhaiID == trangthaiToKhaiID);
            query = query.Where(x => x.ProvinceCodeTo == curArea || x.DistrictCodeTo == curArea || x.WardCodeTo == curArea);
            return query.ToList();
        }

        public ICollection<ToKhai> Get(int? trangThaiToKhaiID, bool? isCurrent, int? chotKiemSoatID)
        {
            var query = CreateQuery(trangThaiToKhaiID, isCurrent, chotKiemSoatID);
            return query.OrderByDescending(x => x.ID).ToList();
        }

        public int Count(int? trangThaiToKhaiID, bool? isCurrent, int? chotKiemSoatID)
        {
            var query = CreateQuery(trangThaiToKhaiID, isCurrent, chotKiemSoatID);
            return query.Count();
        }

        private IQueryable<ToKhai> CreateQuery(int? trangThaiToKhaiID, bool? isCurrent, int? chotKiemSoatID)
        {
            var query = _context.ToKhais.AsQueryable();

            if (trangThaiToKhaiID != null && trangThaiToKhaiID.Value > 0)
            {
                query = query.Where(x => x.TrangThaiToKhaiID == trangThaiToKhaiID);
            }

            if (chotKiemSoatID != null && chotKiemSoatID.Value > 0)
            {
                query = query.Where(x => x.ChotKiemSoatID == chotKiemSoatID);
            }

            if (isCurrent.HasValue)
            {
                var curArea = GetAreaCodeByCurrentUser();
                query = query.Where(x => x.ProvinceCodeTo == curArea || x.DistrictCodeTo == curArea || x.WardCodeTo == curArea);
            }

            return query;
        }
    }
}
