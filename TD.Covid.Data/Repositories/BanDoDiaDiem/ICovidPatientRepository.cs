using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.BanDoDiaDiem;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.Repositories.ThongTinKiemSoat;
using TD.Covid.Data.ViewModels;

namespace TD.Covid.Data.Repositories
{
     public interface ICovidPatientRepository : IRepository<CovidPatient> {

        int CreateCovidPatient(CovidPatientRequest request);

        
    }
}
