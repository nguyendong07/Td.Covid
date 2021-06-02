using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Model.ThongTinKiemSoat
{
    public class LichSuKiemSoat
    {
        public LichSuKiemSoat()
        {

        }

        public LichSuKiemSoat(ToKhai toKhai, People people)
        {
            PeopleID = people.ID;
            NguoiKiemSoatID = toKhai.NguoiKiemSoatId;
            NguoiKiemSoatName = toKhai.NguoiKiemSoatName;
            NgayKiemSoat = toKhai.NgayToi;
            ChotKiemSoatID = toKhai.ChotKiemSoatID;
            DenTuVungDich = toKhai.DenTuVungDich;
        }


        public LichSuKiemSoat(ToKhai toKhai, People people, string comment, int trangThaiToKhaiId, string nguoiKiemSoatName)
        {
            PeopleID = people.ID;
            ToKhaiId = toKhai.ID;
            NguoiKiemSoatName = nguoiKiemSoatName;
            NgayKiemSoat = DateTime.Now;
            TinhTrang = trangThaiToKhaiId;
            Comment = comment;
        }

        public int ID { get; set; }
        public int PeopleID { get; set; }
        public int TinhTrang { get; set; }
        public string NguoiKiemSoatID { get; set; }
        public string NguoiKiemSoatName { get; set; }
        public DateTime? NgayKiemSoat { get; set; }
        public int ChotKiemSoatID { get; set; }
        public bool DenTuVungDich { get; set; }

        public string Comment { get; set; }
        public int ToKhaiId { get; set; }
    }
}
