using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TD.Covid.Data.DataContext;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.Repositories.BanDoDiaDiem;
using TD.Covid.Data.Repositories.ThongBao;
using TD.Covid.Data.Repositories.ThongTinKiemSoat;
using TD.Covid.Data.Repositories.ThongTinLuuTru;
using TD.Covid.Data.Repositories.ToKhaiYTe;
using TD.Covid.Data.Services;
using TD.Core.Api.Mvc;
using TD.Core.Api.Mvc.Integration;
using Unity;

namespace TD.Covid.Api.Integration
{
    public class CovidApiModule : IApiModule
    {
        public string GetPrefix()
        {
            return "covidapi";
        }

        public ICollection<Assembly> GetAssemblies()
        {
            return new Assembly[] { typeof(CovidApiModule).Assembly };
        }

        public void RegisterApiConfig(HttpRouteCollection routes)
        {
            /*routes.MapHttpRoute(
                name: "covidapi",
                routeTemplate: "covidapi/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = RouteParameter.Optional
                }
            );*/
        }

        public void RegisterDIConfig(UnityContainer container)
        {
            container
                // DbContext
                .RegisterType<CovidDataContext>()
                // Repositories
                .RegisterType<IThongBaoKhanCapRepository, ThongBaoKhanCapRepository>()
                .RegisterType<IVungBiNhiemRepository, VungBiNhiemRepository>()
                .RegisterType<IBenhNenRepository, BenhNenRepository>()
                .RegisterType<IChotKiemSoatRepository, ChotKiemSoatRepository>()
                .RegisterType<ILichSuKiemSoatRepository, LichSuKiemSoatRepository>()
                .RegisterType<IPeopleRepository, PeopleRepository>()
                .RegisterType<IPhuongTienRepository, PhuongTienRepository>()
                .RegisterType<IQuocTichRepository, QuocTichRepository>()
                .RegisterType<ITrieuChungRepository, TrieuChungRepository>()
                .RegisterType<ICoSoLuuTruRepository, CoSoLuuTruRepository>()
                .RegisterType<ILichSuLuuTruRepository, LichSuLuuTruRepository>()
                .RegisterType<IQuyTrinhToKhaiRepository, QuyTrinhToKhaiRepository>()
                .RegisterType<IToKhaiRepository, ToKhaiRepository>()
                .RegisterType<ITrangThaiToKhaiRepository, TrangThaiToKhaiRepository>()
                .RegisterType<ITinhTrangTheoDoiRepository, TinhTrangTheoDoiRepository>()
                .RegisterType<IAreaRepository, AreaRepository>()
                .RegisterType<IPeopleBenhNenRepository, PeopleBenhNenRepository>()
                .RegisterType<IPeopleTrieuChungRepository, PeopleTrieuChungRepository>()
                .RegisterType<IToKhaiBenhNenRepository, ToKhaiBenhNenRepository>()
                .RegisterType<IToKhaiTrieuChungRepository, ToKhaiTrieuChungRepository>()
                .RegisterType<IPhuongTienRepository, PhuongTienRepository>()
                .RegisterType<IQuocTichRepository, QuocTichRepository>()
                .RegisterType<IDanTocRepository, DanTocRepository>()



                .RegisterType<ILocationRepository, LocationRepository>()
                .RegisterType<ILocationTypeRepository, LocationTypeRepository>()
                .RegisterType<ICovidPatientRepository, CovidPatientRepository>()
                .RegisterType<IPatientFtLocationRepository, PatientFtLocationRepository>()
                // Service
                .RegisterType<INotificationService, NotificationService>()
             
                // Another
                .RegisterType<ICoreContextAccessor, CoreContextAcessor>()
                .RegisterFactory<ICoreServicesProvider>(c => new DefaultContextCoreServicesProvider());
        }
    }
}
