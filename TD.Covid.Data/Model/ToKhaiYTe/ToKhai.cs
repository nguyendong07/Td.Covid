using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.ViewModels;

namespace TD.Covid.Data.Model.ToKhaiYTe
{
    public class ToKhai : ITrackableModel
    {
        public ToKhai()
        {

        }
        
        public ToKhai(ToKhaiVM vm)
        {
            ID = vm.ID;
            Name = vm.Name;
            IdentificationID = vm.IdentificationID;
            NgaySinh = vm.NgaySinh;
            GioiTinh = vm.GioiTinh;
            QuocTichID = vm.QuocTichID;
            DanTocID = vm.DanTocID;
            DienThoai = vm.DienThoai;
            Email = vm.Email;
            ProvinceCode = vm.ProvinceCode;
            DistrictCode = vm.DistrictCode;
            WardCode = vm.WardCode;
            DiaChi = vm.DiaChi;
            PhuongTienID = vm.PhuongTienID;
            PhuongTienSoGhe = vm.PhuongTienSoGhe;
            NgayKhoiHanh = vm.NgayKhoiHanh;
            NgayToi = vm.NgayToi;
            ProvinceCodeFrom = vm.ProvinceCodeFrom;
            ProvinceCodeTo = vm.ProvinceCodeTo;
            DistrictCodeFrom = vm.DistrictCodeFrom;
            DistrictCodeTo = vm.DistrictCodeTo;
            WardCodeFrom = vm.WardCodeFrom;
            WardCodeTo = vm.WardCodeTo;
            StationFrom = vm.StationFrom;
            StationTo = vm.StationTo;
            GhiChuDiChuyen = vm.GhiChuDiChuyen;
            TrangThaiToKhaiID = vm.TrangThaiToKhaiID;
            ChotKiemSoatID = vm.ChotKiemSoatID;
            NguoiKiemSoatId = vm.NguoiKiemSoatId;
            NguoiKiemSoatName = vm.NguoiKiemSoatName;
            DenTuVungDich = vm.DenTuVungDich;
            NamSinh = vm.NamSinh;
            BienSo = vm.BienSo;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string IdentificationID { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public int? QuocTichID { get; set; }
        public QuocTich QuocTich { get; set; }
        public int? DanTocID { get; set; }
        public DanToc DanToc { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public Area Province { get; set; }
        public string ProvinceCode { get; set; }
        [NotMapped]
        public Area District { get; set; }
        public string DistrictCode { get; set; }
        [NotMapped]
        public Area Ward { get; set; }
        public string WardCode { get; set; }
        public string DiaChi { get; set; }
        public int PhuongTienID { get; set; }
        public PhuongTien PhuongTien { get; set; }
        public string PhuongTienSoGhe { get; set; }
        public DateTime? NgayKhoiHanh { get; set; }
        public DateTime? NgayToi { get; set; }
        [NotMapped]
        public Area ProvinceFrom { get; set; }
        public string ProvinceCodeFrom { get; set; }
        [NotMapped]
        public Area ProvinceTo { get; set; }
        public string ProvinceCodeTo { get; set; }
        [NotMapped]
        public Area DistrictFrom { get; set; }
        public string DistrictCodeFrom { get; set; }
        [NotMapped]
        public Area DistrictTo { get; set; }
        public string DistrictCodeTo { get; set; }
        [NotMapped]
        public Area WardFrom { get; set; }
        public string WardCodeFrom { get; set; }
        [NotMapped]
        public Area WardTo { get; set; }
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
        public string BienSo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        [NotMapped]
        public ICollection<TrieuChung> TrieuChungs { get; set; }
        [NotMapped]
        public ICollection<BenhNen> BenhNens { get; set; }
        public string DiaChiDen { get; set; }
        public string NamSinh { get; set; }
    }
}
