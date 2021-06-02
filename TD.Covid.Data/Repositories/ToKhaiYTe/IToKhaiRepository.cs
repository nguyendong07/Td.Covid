using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Covid.Data.ViewModels;

namespace TD.Covid.Data.Repositories.ToKhaiYTe
{
    public interface IToKhaiRepository : IRepository<ToKhai>
    {
        ICollection<ToKhai> Get(int? trangThaiToKhaiID, bool? isCurrent, int? chotKiemSoatID);
        ToKhai GetByIdIncludeFull(int id);
        ICollection<ToKhai> GetByTrangThai(string trangthai,string areaCode);
        ICollection<ToKhaiBase> GetAllBase(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiID, string phuongTienID, string bienSo, string skip, string top, string q,
            string fromNgayKhoiHanh, string toNgayKhoiHanh, string fromNgayToi, string toNgayToi, string chotKiemSoatID);
        ICollection<ToKhaiBase> GetAllBase(string identificationID, string provinceTo, string provinceFrom,
           string districtTo, string districtFrom, string wardTo, string wardFrom,
           string trangThaiToKhaiID, string phuongTienID, string bienSo, string skip, string top, string q,
           string fromNgayKhoiHanh, string toNgayKhoiHanh, string fromNgayToi, string toNgayToi, string chotKiemSoatID, string areaCode, string fromNgayTao, string toNgayTao, string denTuVungDich);
        ICollection<ToKhaiBase> GetTuNgayDenngay(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom, string trangThaiToKhaiID,
            string fromNgayKhoiHanh, string toNgayKhoiHanh);

        ICollection<ToKhaiBase> GetTuNgayDenngay(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom, string trangThaiToKhaiID,
            string fromNgayKhoiHanh, string toNgayKhoiHanh, string denTuVungDich);

        ICollection<ToKhaiBase> GetTuNgayDenngay(string identificationID, string provinceTo, string provinceFrom,
                    string districtTo, string districtFrom, string wardTo, string wardFrom, string trangThaiToKhaiID,
                    string fromNgayKhoiHanh, string toNgayKhoiHanh, string chotKiemSoatID, string denTuVungDich);

        ICollection<ToKhai> GetByCreatedAt(DateTime created, string trangthai,string areaCode);
        string GetAreaCodeByCurrentUser();

        ICollection<ToKhai> GetByTrangThai(int trangThaiToKhaiID);

        int Count(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiIDStr, string phuongTienIDStr, string bienSo,
            string fromNgayKhoiHanhStr, string toNgayKhoiHanhStr, string fromNgayToiStr,
            string toNgayToiStr, string chotKiemSoatIDStr, string areaCode,
            string fromNgayTaoStr, string toNgayTaoStr, string diTuVungDich, string q);

        int Count(int? trangThaiToKhaiID, bool? isCurrent, int? chotKiemSoatID);
    }
}
