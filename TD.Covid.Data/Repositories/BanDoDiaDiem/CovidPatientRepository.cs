using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Model.BanDoDiaDiem;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.ViewModels;
using TD.Core.Api.Mvc;

namespace TD.Covid.Data.Repositories.BanDoDiaDiem
{
    public class CovidPatientRepository : Repository<CovidPatient>, ICovidPatientRepository
    {
        private readonly CovidDataContext _context;
        private readonly ICoreContextAccessor _coreContextAccessor;
        public CovidPatientRepository(CovidDataContext context, ICoreContextAccessor coreContextAccessor)
            : base(context, coreContextAccessor)
        {
            _context = context;
            _coreContextAccessor = coreContextAccessor;
        }

       

        public  int CreateCovidPatient(CovidPatientRequest request)
        {
            int peopleId = 0;
            var people = _context.Peoples.Where(i => i.IdentificationID == request.IdentificationID).FirstOrDefault();
            if (people==null)
            {
                var peopleTmp = new People()
                {
                    Name = request.FullName,
                    Email = request.Email,
                    IdentificationID = request.IdentificationID,
                    GioiTinh = request.Gender,
                    NgaySinh = request.NgaySinh,
                    ProvinceCode = request.ProvinceCode,
                    DistrictCode = request.DistrictCode,
                    WardCode = request.WardCode,
                    DiaChi = request.Address,
                    QuocTichID = request.QuocTichID,
                    PhuongTienID = 1 // hack
                };
                _context.Peoples.Add(peopleTmp);
                _context.SaveChanges();
                peopleId = peopleTmp.ID;
            } else
            {
                peopleId = people.ID;
            }

            var covidPatient = new CovidPatient()
            {
                Name = request.Name,
                SickConditionId = request.SickConditionId,
                PeopleId = peopleId,
                EpideInformation = request.EpideInformation,
                IssueDate = DateTime.Parse(request.IssueDate)
            };
            var model1 = _context.CovidPatients.Add(covidPatient);

             _context.SaveChanges();
            return model1.ID;
        }
    }
}

