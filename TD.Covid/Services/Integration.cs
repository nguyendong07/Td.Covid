using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.Repositories.ThongTinKiemSoat;
using TD.Covid.Data.Repositories.ThongTinLuuTru;
using TD.Covid.Data.Repositories.ToKhaiYTe;
using TD.Core.Api.Mvc;
using Unity;

namespace TD.Covid.Services
{
    public class Integration
    {
        public void RegisterDIConfig(IUnityContainer container)
        {
            container
                // DbContext
                .RegisterType<CovidDataContext>()
                // Repositories
                .RegisterType<ITrieuChungRepository, TrieuChungRepository>()
                .RegisterType<IBenhNenRepository, BenhNenRepository>()
                .RegisterType<IToKhaiRepository, ToKhaiRepository>()
                .RegisterType<IPeopleRepository, PeopleRepository>()
                .RegisterType<ILichSuKiemSoatRepository, LichSuKiemSoatRepository>()
                .RegisterType<IPeopleBenhNenRepository, PeopleBenhNenRepository>()
                .RegisterType<IPeopleTrieuChungRepository, PeopleTrieuChungRepository>()
                .RegisterType<IToKhaiBenhNenRepository, ToKhaiBenhNenRepository>()
                .RegisterType<IToKhaiTrieuChungRepository, ToKhaiTrieuChungRepository>()
                .RegisterType<ITrangThaiToKhaiRepository, TrangThaiToKhaiRepository>()
                .RegisterType<IAreaRepository, AreaRepository>()
                .RegisterType<IQuyTrinhToKhaiRepository, QuyTrinhToKhaiRepository>()
                .RegisterType<IPhuongTienRepository, PhuongTienRepository>()
                .RegisterType<IChotKiemSoatRepository, ChotKiemSoatRepository>()
                .RegisterType<ICoSoLuuTruRepository, CoSoLuuTruRepository>()
                .RegisterType<IQuocTichRepository, QuocTichRepository>()
                .RegisterType<IDanTocRepository, DanTocRepository>()
                // Another
                .RegisterType<ICoreContextAccessor, CoreContextAcessor>()
                .RegisterFactory<ICoreServicesProvider>(c => new DefaultContextCoreServicesProvider());
        }
    }
}
