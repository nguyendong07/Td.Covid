using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.ViewModels
{
    public class ToKhaiBase
    {
        public int ID { get; set; }
        public string TenPhuongTien { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgayKhoiHanh { get; set; }
        public DateTime? NgayToi { get; set; }
        public string DiaDiemKhoiHanh { get; set; }
        public string DiaDiemToi { get; set; }
        public string Name { get; set; }
        public TrangThaiToKhai TrangThaiToKhai {get;set;}
        public string NguoiKiemSoatName { get; set; }
        public string IdentificationID { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }
}
