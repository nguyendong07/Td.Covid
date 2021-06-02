using System;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Model.ThongTinKiemSoat
{
    public class LichSuDiChuyen
    {
        public LichSuDiChuyen()
        {

        }

        public LichSuDiChuyen(ToKhai toKhai, People people)
        {
            PeopleID = people.ID;
            ProvinceCodeFrom = toKhai.ProvinceCodeFrom;
            ProvinceCodeTo = toKhai.ProvinceCodeTo;
            DistrictCodeFrom = toKhai.DistrictCodeFrom;
            DistrictCodeTo = toKhai.DistrictCodeTo;
            WardCodeFrom = toKhai.WardCodeFrom;
            WardCodeTo = toKhai.WardCodeTo;
            NgayKhoiHanh = toKhai.NgayKhoiHanh;
            NgayToi = toKhai.NgayToi;
            GhiChuDiChuyen = toKhai.GhiChuDiChuyen;
        }

        public int ID { get; set; }
        public int PeopleID { get; set; }
        public People People { get; set; }
        public string ProvinceCodeFrom { get; set; }
        public string ProvinceCodeTo { get; set; }
        public string DistrictCodeFrom { get; set; }
        public string DistrictCodeTo { get; set; }
        public string WardCodeFrom { get; set; }
        public string WardCodeTo { get; set; }
        public DateTime? NgayKhoiHanh { get; set; }
        public DateTime? NgayToi { get; set; }
        public string GhiChuDiChuyen { get; set; }
    }
}
