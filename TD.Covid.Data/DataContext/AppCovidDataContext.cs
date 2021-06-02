using System.Data.Entity;
using TD.Covid.Data.Model;
using TD.Covid.Data.Model.ThongBao;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using TD.Covid.Data.Model.ThongTinLuuTru;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Covid.Data.Model.BanDoDiaDiem;


namespace TD.Covid.Data.DataContext
{
    public class CovidDataContext : DbContext
    {
        public CovidDataContext() : base("CovidDataContext") {
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<ThongBaoKhanCap> ThongBaoKhanCaps { get; set; }
        public DbSet<VungBiNhiem> VungBiNhiems { get; set; }
        public DbSet<BenhNen> BenhNens { get; set; }
        public DbSet<ChotKiemSoat> ChotKiemSoats { get; set; }
        public DbSet<LichSuDiChuyen> LichSuDiChuyens { get; set; }
        public DbSet<LichSuKiemSoat> LichSuKiemSoats { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<PhuongTien> PhuongTiens { get; set; }
        public DbSet<QuocTich> QuocTichs { get; set; }
        public DbSet<TrieuChung> TrieuChungs { get; set; }
        public DbSet<CoSoLuuTru> CoSoLuuTrus { get; set; }
        public DbSet<LichSuLuuTru> LichSuLuuTrus { get; set; }
        public DbSet<QuyTrinhToKhai> QuyTrinhToKhais { get; set; }
        public DbSet<ToKhai> ToKhais { get; set; }
        public DbSet<TrangThaiToKhai> TrangThaiToKhais { get; set; }
        public DbSet<ToKhaiTrieuChung> ToKhaiTrieuChungs { get; set; }
        public DbSet<ToKhaiBenhNen> ToKhaiBenhNens { get; set; }
        public DbSet<PeopleTrieuChung> PeopleTrieuChungs { get; set; }
        public DbSet<PeopleBenhNen> PeopleBenhNens { get; set; }
        public DbSet<TinhTrangTheoDoi> TinhTrangTheoDois { get; set; }
        public DbSet<DanToc> DanTocs { get; set; }


        public DbSet<CovidPatient> CovidPatients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<PatientFtLocation> PatientFtLocations { get; set; }
        public DbSet<SickCondition> SickConditions { get; set; }
     



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .ToTable("Area");

            modelBuilder.Entity<ThongBaoKhanCap>()
                .ToTable("ThongBaoKhanCap");

            modelBuilder.Entity<VungBiNhiem>()
                .ToTable("VungBiNhiem");

            modelBuilder.Entity<BenhNen>()
               .ToTable("BenhNen");

            modelBuilder.Entity<ChotKiemSoat>()
              .ToTable("ChotKiemSoat");

            modelBuilder.Entity<LichSuDiChuyen>()
              .ToTable("LichSuDiChuyen");

            modelBuilder.Entity<LichSuKiemSoat>()
              .ToTable("LichSuKiemSoat");

            modelBuilder.Entity<People>()
              .ToTable("People");

            modelBuilder.Entity<PhuongTien>()
              .ToTable("PhuongTien");

            modelBuilder.Entity<QuocTich>()
              .ToTable("QuocTich");

            modelBuilder.Entity<DanToc>()
              .ToTable("DanToc");

            modelBuilder.Entity<TrieuChung>()
              .ToTable("TrieuChung");

            modelBuilder.Entity<CoSoLuuTru>()
              .ToTable("CoSoLuuTru");

            modelBuilder.Entity<LichSuLuuTru>()
              .ToTable("LichSuLuuTru");

            modelBuilder.Entity<QuyTrinhToKhai>()
              .ToTable("QuyTrinhToKhai");

            modelBuilder.Entity<ToKhai>()
              .ToTable("ToKhai");

            modelBuilder.Entity<TrangThaiToKhai>()
              .ToTable("TrangThaiToKhai");

            modelBuilder.Entity<ToKhaiTrieuChung>()
                .ToTable("ToKhaiTrieuChung");

            modelBuilder.Entity<ToKhaiTrieuChung>()
                .HasRequired(x => x.ToKhai)
                .WithMany()
                .HasForeignKey(x => x.ToKhaiId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ToKhaiTrieuChung>()
                .HasRequired(x => x.TrieuChung)
                .WithMany()
                .HasForeignKey(x => x.TrieuChungId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ToKhaiBenhNen>()
                .ToTable("ToKhaiBenhNen");

            modelBuilder.Entity<ToKhaiBenhNen>()
                .HasRequired(x => x.ToKhai)
                .WithMany()
                .HasForeignKey(x => x.ToKhaiId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ToKhaiBenhNen>()
                .HasRequired(x => x.BenhNen)
                .WithMany()
                .HasForeignKey(x => x.BenhNenId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PeopleBenhNen>()
                .ToTable("PeopleBenhNen");

            modelBuilder.Entity<PeopleBenhNen>()
                .HasRequired(x => x.People)
                .WithMany()
                .HasForeignKey(x => x.PeopleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PeopleBenhNen>()
                .HasRequired(x => x.BenhNen)
                .WithMany()
                .HasForeignKey(x => x.BenhNenId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PeopleTrieuChung>()
             .ToTable("PeopleTrieuChung");

            modelBuilder.Entity<PeopleTrieuChung>()
                .HasRequired(x => x.People)
                .WithMany()
                .HasForeignKey(x => x.PeopleId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<PeopleTrieuChung>()
                .HasRequired(x => x.TrieuChung)
                .WithMany()
                .HasForeignKey(x => x.TrieuChungId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TinhTrangTheoDoi>()
                .ToTable("TinhTrangTheoDoi");


             modelBuilder.Entity<CovidPatient>()
                .ToTable("CovidPatient");
            modelBuilder.Entity<Location>()
               .ToTable("Location");
            modelBuilder.Entity<LocationType>()
              .ToTable("LocationType");
            modelBuilder.Entity<PatientFtLocation>()
              .ToTable("PatientFtLocation");
            modelBuilder.Entity<SickCondition>()
              .ToTable("SickCondition");

            modelBuilder.Entity<Location>()
               .HasRequired(x => x.LocationType)
               .WithMany()
               .HasForeignKey(x => x.LocationTypeId)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<CovidPatient>()
               .HasRequired(x => x.People)
               .WithMany()
               .HasForeignKey(x => x.PeopleId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<CovidPatient>()
               .HasRequired(x => x.SickCondition)
               .WithMany()
               .HasForeignKey(x => x.SickConditionId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientFtLocation>()
             .HasRequired(x => x.CovidPatient)
             .WithMany()
             .HasForeignKey(x => x.CovidPatientId)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientFtLocation>()
            .HasRequired(x => x.Location)
            .WithMany()
            .HasForeignKey(x => x.LocationId)
            .WillCascadeOnDelete(false);



            base.OnModelCreating(modelBuilder);
        }
    }
}
