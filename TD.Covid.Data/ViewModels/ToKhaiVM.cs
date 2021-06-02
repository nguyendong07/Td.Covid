using System;
using System.Collections.Generic;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.ViewModels
{
    public class ToKhaiVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IdentificationID { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public int QuocTichID { get; set; }
        public int DanTocID { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string DiaChi { get; set; }
        public int PhuongTienID { get; set; }
        public PhuongTien PhuongTien { get; set; }
        public string PhuongTienSoGhe { get; set; }
        public DateTime? NgayKhoiHanh { get; set; }
        public DateTime? NgayToi { get; set; }
        public string ProvinceCodeFrom { get; set; }
        public string ProvinceCodeTo { get; set; }
        public string DistrictCodeFrom { get; set; }
        public string DistrictCodeTo { get; set; }
        public string WardCodeFrom { get; set; }
        public string WardCodeTo { get; set; }
        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public string GhiChuDiChuyen { get; set; }
        public int TrangThaiToKhaiID { get; set; }
        public TrangThaiToKhai TrangThaiToKhai { get; set; }
        public int ChotKiemSoatID { get; set; }
        public ChotKiemSoat ChotKiemSoat { get; set; }
        public string NguoiKiemSoatId { get; set; }
        public string NguoiKiemSoatName { get; set; }
        public bool DenTuVungDich { get; set; }

        public List<int> TrieuChungs { get; set; }
        public List<int> BenhNens { get; set; }
        public string BienSo { get; set; }
        public string NamSinh { get; set; }
    }
}
