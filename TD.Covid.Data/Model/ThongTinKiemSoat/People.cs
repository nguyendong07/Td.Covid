using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Model.ThongTinKiemSoat
{
    public class People
    {
        public People()
        {

        }

        public People(ToKhai toKhai)
        {
            Name = toKhai.Name;
            PhuongTienID = toKhai.PhuongTienID;
            IdentificationID = toKhai.IdentificationID;
            GioiTinh = toKhai.GioiTinh;
            NgaySinh = toKhai.NgaySinh;
            DienThoai = toKhai.DienThoai;
            Email = toKhai.Email;
            ProvinceCode = toKhai.ProvinceCode;
            DistrictCode = toKhai.DistrictCode;
            WardCode = toKhai.WardCode;
            DiaChi = toKhai.DiaChi;
            QuocTichID = toKhai.QuocTichID;
            DanTocID = toKhai.DanTocID;
            //TrieuChungs = toKhai.TrieuChungs;
            //BenhNens = toKhai.BenhNens;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int PhuongTienID { get; set; }
        public PhuongTien PhuongTien { get; set; }
        public string IdentificationID { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string DiaChi { get; set; }
        public int? QuocTichID { get; set; }
        public QuocTich QuocTich { get; set; }
        public int? DanTocID { get; set; }
        public DanToc DanToc { get; set; }
        public int? TinhTrangTheoDoiID { get; set; }
        public TinhTrangTheoDoi TinhTrangTheoDoi { get; set; }
    }
}
