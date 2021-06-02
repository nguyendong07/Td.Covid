using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.ViewModels
{
    public class CovidPatientRequest
    {
        public string Name { get; set; }

        public string FullName { get; set; }
        public string IdentificationID { get; set; }
        public string Email { get; set; }
        public int? QuocTichID { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Number { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        
        public string ProvinceCode { get; set; }
       
        public string DistrictCode { get; set; }

       
        public string WardCode { get; set; }
        public int SickConditionId { get; set; }
        
        public string EpideInformation { get; set; }
        public string IssueDate { get; set; }
       
    }
}
