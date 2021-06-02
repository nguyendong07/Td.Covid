
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using TD.Covid.Data.Model.ThongTinKiemSoat;

namespace TD.Covid.Data.Model.BanDoDiaDiem
{
    public class CovidPatient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        
       
      
        public int SickConditionId { get; set; }
        public SickCondition SickCondition { get; set; }
        //Thong tin dich te
        [Column(TypeName = "ntext")]
        public string EpideInformation { get; set; }
        public DateTime? IssueDate { get; set; }
        public People People { get; set; }
        public int PeopleId { get; set; }

    }
}
