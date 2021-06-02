namespace TD.Covid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        ParentId = c.Int(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(),
                        Name = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BenhNen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChotKiemSoat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DiaChi = c.String(),
                        CuaKhau = c.Int(nullable: false),
                        DanhSachNguoiDung = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CoSoLuuTru",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DiaChi = c.String(),
                        DienThoai = c.String(),
                        Email = c.String(),
                        ProvinceCode = c.Int(nullable: false),
                        DistrictCode = c.Int(nullable: false),
                        WardCode = c.Int(nullable: false),
                        DanhSachNguoiDung = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CovidPatient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.String(),
                        SickConditionId = c.Int(nullable: false),
                        EpideInformation = c.String(storeType: "ntext"),
                        IssueDate = c.DateTime(),
                        PeopleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .ForeignKey("dbo.SickCondition", t => t.SickConditionId)
                .Index(t => t.SickConditionId)
                .Index(t => t.PeopleId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhuongTienID = c.Int(nullable: false),
                        IdentificationID = c.String(),
                        GioiTinh = c.String(),
                        NgaySinh = c.DateTime(),
                        DienThoai = c.String(),
                        Email = c.String(),
                        ProvinceCode = c.String(),
                        DistrictCode = c.String(),
                        WardCode = c.String(),
                        DiaChi = c.String(),
                        QuocTichID = c.Int(),
                        DanTocID = c.Int(),
                        TinhTrangTheoDoiID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DanToc", t => t.DanTocID)
                .ForeignKey("dbo.PhuongTien", t => t.PhuongTienID, cascadeDelete: true)
                .ForeignKey("dbo.QuocTich", t => t.QuocTichID)
                .ForeignKey("dbo.TinhTrangTheoDoi", t => t.TinhTrangTheoDoiID)
                .Index(t => t.PhuongTienID)
                .Index(t => t.QuocTichID)
                .Index(t => t.DanTocID)
                .Index(t => t.TinhTrangTheoDoiID);
            
            CreateTable(
                "dbo.DanToc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhuongTien",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LoaiPhuongTien = c.Int(nullable: false),
                        BienSo = c.String(),
                        SoGhe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuocTich",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TinhTrangTheoDoi",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SickCondition",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Describe = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LichSuDiChuyen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeopleID = c.Int(nullable: false),
                        ProvinceCodeFrom = c.String(),
                        ProvinceCodeTo = c.String(),
                        DistrictCodeFrom = c.String(),
                        DistrictCodeTo = c.String(),
                        WardCodeFrom = c.String(),
                        WardCodeTo = c.String(),
                        NgayKhoiHanh = c.DateTime(),
                        NgayToi = c.DateTime(),
                        GhiChuDiChuyen = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PeopleID, cascadeDelete: true)
                .Index(t => t.PeopleID);
            
            CreateTable(
                "dbo.LichSuKiemSoat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeopleID = c.Int(nullable: false),
                        TinhTrang = c.Int(nullable: false),
                        NguoiKiemSoatID = c.String(),
                        NguoiKiemSoatName = c.String(),
                        NgayKiemSoat = c.DateTime(),
                        ChotKiemSoatID = c.Int(nullable: false),
                        DenTuVungDich = c.Boolean(nullable: false),
                        Comment = c.String(),
                        ToKhaiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LichSuLuuTru",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeopleID = c.Int(nullable: false),
                        CoSoLuuTruID = c.Int(nullable: false),
                        CheckInDate = c.Int(nullable: false),
                        CheckOutDate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CoSoLuuTru", t => t.CoSoLuuTruID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PeopleID, cascadeDelete: true)
                .Index(t => t.PeopleID)
                .Index(t => t.CoSoLuuTruID);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Describer = c.String(),
                        ProvinceCode = c.Int(nullable: false),
                        DistrictCode = c.String(),
                        WardCode = c.String(),
                        Address = c.String(),
                        Lat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Long = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LocationType", t => t.LocationTypeId)
                .Index(t => t.LocationTypeId);
            
            CreateTable(
                "dbo.LocationType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Describe = c.String(),
                        Icon = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PatientFtLocation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CovidPatientId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CovidPatient", t => t.CovidPatientId)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .Index(t => t.CovidPatientId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.PeopleBenhNen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeopleId = c.Int(nullable: false),
                        BenhNenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BenhNen", t => t.BenhNenId)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .Index(t => t.PeopleId)
                .Index(t => t.BenhNenId);
            
            CreateTable(
                "dbo.PeopleTrieuChung",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeopleId = c.Int(nullable: false),
                        TrieuChungId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .ForeignKey("dbo.TrieuChung", t => t.TrieuChungId)
                .Index(t => t.PeopleId)
                .Index(t => t.TrieuChungId);
            
            CreateTable(
                "dbo.TrieuChung",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuyTrinhToKhai",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ToKhaiID = c.Int(nullable: false),
                        Name = c.String(),
                        NguoiKiemSoatID = c.String(),
                        NguoiKiemSoatName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ToKhai", t => t.ToKhaiID, cascadeDelete: true)
                .Index(t => t.ToKhaiID);
            
            CreateTable(
                "dbo.ToKhai",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IdentificationID = c.String(),
                        NgaySinh = c.DateTime(),
                        GioiTinh = c.String(),
                        QuocTichID = c.Int(),
                        DanTocID = c.Int(),
                        DienThoai = c.String(),
                        Email = c.String(),
                        ProvinceCode = c.String(),
                        DistrictCode = c.String(),
                        WardCode = c.String(),
                        DiaChi = c.String(),
                        PhuongTienID = c.Int(nullable: false),
                        PhuongTienSoGhe = c.String(),
                        NgayKhoiHanh = c.DateTime(),
                        NgayToi = c.DateTime(),
                        ProvinceCodeFrom = c.String(),
                        ProvinceCodeTo = c.String(),
                        DistrictCodeFrom = c.String(),
                        DistrictCodeTo = c.String(),
                        WardCodeFrom = c.String(),
                        WardCodeTo = c.String(),
                        StationFrom = c.String(),
                        StationTo = c.String(),
                        GhiChuDiChuyen = c.String(),
                        TrangThaiToKhaiID = c.Int(nullable: false),
                        ChotKiemSoatID = c.Int(nullable: false),
                        NguoiKiemSoatId = c.String(),
                        NguoiKiemSoatName = c.String(),
                        DenTuVungDich = c.Boolean(nullable: false),
                        BienSo = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(),
                        DiaChiDen = c.String(),
                        NamSinh = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChotKiemSoat", t => t.ChotKiemSoatID, cascadeDelete: true)
                .ForeignKey("dbo.DanToc", t => t.DanTocID)
                .ForeignKey("dbo.PhuongTien", t => t.PhuongTienID, cascadeDelete: true)
                .ForeignKey("dbo.QuocTich", t => t.QuocTichID)
                .ForeignKey("dbo.TrangThaiToKhai", t => t.TrangThaiToKhaiID, cascadeDelete: true)
                .Index(t => t.QuocTichID)
                .Index(t => t.DanTocID)
                .Index(t => t.PhuongTienID)
                .Index(t => t.TrangThaiToKhaiID)
                .Index(t => t.ChotKiemSoatID);
            
            CreateTable(
                "dbo.TrangThaiToKhai",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThongBaoKhanCap",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(),
                        ProvinceCode = c.Int(nullable: false),
                        DistrictCode = c.Int(nullable: false),
                        WardCode = c.Int(nullable: false),
                        DienThoai = c.String(),
                        DiaChi = c.String(),
                        NoiDung = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ToKhaiBenhNen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ToKhaiId = c.Int(nullable: false),
                        BenhNenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BenhNen", t => t.BenhNenId)
                .ForeignKey("dbo.ToKhai", t => t.ToKhaiId)
                .Index(t => t.ToKhaiId)
                .Index(t => t.BenhNenId);
            
            CreateTable(
                "dbo.ToKhaiTrieuChung",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ToKhaiId = c.Int(nullable: false),
                        TrieuChungId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ToKhai", t => t.ToKhaiId)
                .ForeignKey("dbo.TrieuChung", t => t.TrieuChungId)
                .Index(t => t.ToKhaiId)
                .Index(t => t.TrieuChungId);
            
            CreateTable(
                "dbo.VungBiNhiem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProvinceCode = c.Int(nullable: false),
                        DistrictCode = c.Int(nullable: false),
                        WardCode = c.Int(nullable: false),
                        Active = c.Int(nullable: false),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        ProvinceText = c.String(),
                        DistrictText = c.String(),
                        WardTextpublic = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToKhaiTrieuChung", "TrieuChungId", "dbo.TrieuChung");
            DropForeignKey("dbo.ToKhaiTrieuChung", "ToKhaiId", "dbo.ToKhai");
            DropForeignKey("dbo.ToKhaiBenhNen", "ToKhaiId", "dbo.ToKhai");
            DropForeignKey("dbo.ToKhaiBenhNen", "BenhNenId", "dbo.BenhNen");
            DropForeignKey("dbo.QuyTrinhToKhai", "ToKhaiID", "dbo.ToKhai");
            DropForeignKey("dbo.ToKhai", "TrangThaiToKhaiID", "dbo.TrangThaiToKhai");
            DropForeignKey("dbo.ToKhai", "QuocTichID", "dbo.QuocTich");
            DropForeignKey("dbo.ToKhai", "PhuongTienID", "dbo.PhuongTien");
            DropForeignKey("dbo.ToKhai", "DanTocID", "dbo.DanToc");
            DropForeignKey("dbo.ToKhai", "ChotKiemSoatID", "dbo.ChotKiemSoat");
            DropForeignKey("dbo.PeopleTrieuChung", "TrieuChungId", "dbo.TrieuChung");
            DropForeignKey("dbo.PeopleTrieuChung", "PeopleId", "dbo.People");
            DropForeignKey("dbo.PeopleBenhNen", "PeopleId", "dbo.People");
            DropForeignKey("dbo.PeopleBenhNen", "BenhNenId", "dbo.BenhNen");
            DropForeignKey("dbo.PatientFtLocation", "LocationId", "dbo.Location");
            DropForeignKey("dbo.PatientFtLocation", "CovidPatientId", "dbo.CovidPatient");
            DropForeignKey("dbo.Location", "LocationTypeId", "dbo.LocationType");
            DropForeignKey("dbo.LichSuLuuTru", "PeopleID", "dbo.People");
            DropForeignKey("dbo.LichSuLuuTru", "CoSoLuuTruID", "dbo.CoSoLuuTru");
            DropForeignKey("dbo.LichSuDiChuyen", "PeopleID", "dbo.People");
            DropForeignKey("dbo.CovidPatient", "SickConditionId", "dbo.SickCondition");
            DropForeignKey("dbo.CovidPatient", "PeopleId", "dbo.People");
            DropForeignKey("dbo.People", "TinhTrangTheoDoiID", "dbo.TinhTrangTheoDoi");
            DropForeignKey("dbo.People", "QuocTichID", "dbo.QuocTich");
            DropForeignKey("dbo.People", "PhuongTienID", "dbo.PhuongTien");
            DropForeignKey("dbo.People", "DanTocID", "dbo.DanToc");
            DropIndex("dbo.ToKhaiTrieuChung", new[] { "TrieuChungId" });
            DropIndex("dbo.ToKhaiTrieuChung", new[] { "ToKhaiId" });
            DropIndex("dbo.ToKhaiBenhNen", new[] { "BenhNenId" });
            DropIndex("dbo.ToKhaiBenhNen", new[] { "ToKhaiId" });
            DropIndex("dbo.ToKhai", new[] { "ChotKiemSoatID" });
            DropIndex("dbo.ToKhai", new[] { "TrangThaiToKhaiID" });
            DropIndex("dbo.ToKhai", new[] { "PhuongTienID" });
            DropIndex("dbo.ToKhai", new[] { "DanTocID" });
            DropIndex("dbo.ToKhai", new[] { "QuocTichID" });
            DropIndex("dbo.QuyTrinhToKhai", new[] { "ToKhaiID" });
            DropIndex("dbo.PeopleTrieuChung", new[] { "TrieuChungId" });
            DropIndex("dbo.PeopleTrieuChung", new[] { "PeopleId" });
            DropIndex("dbo.PeopleBenhNen", new[] { "BenhNenId" });
            DropIndex("dbo.PeopleBenhNen", new[] { "PeopleId" });
            DropIndex("dbo.PatientFtLocation", new[] { "LocationId" });
            DropIndex("dbo.PatientFtLocation", new[] { "CovidPatientId" });
            DropIndex("dbo.Location", new[] { "LocationTypeId" });
            DropIndex("dbo.LichSuLuuTru", new[] { "CoSoLuuTruID" });
            DropIndex("dbo.LichSuLuuTru", new[] { "PeopleID" });
            DropIndex("dbo.LichSuDiChuyen", new[] { "PeopleID" });
            DropIndex("dbo.People", new[] { "TinhTrangTheoDoiID" });
            DropIndex("dbo.People", new[] { "DanTocID" });
            DropIndex("dbo.People", new[] { "QuocTichID" });
            DropIndex("dbo.People", new[] { "PhuongTienID" });
            DropIndex("dbo.CovidPatient", new[] { "PeopleId" });
            DropIndex("dbo.CovidPatient", new[] { "SickConditionId" });
            DropTable("dbo.VungBiNhiem");
            DropTable("dbo.ToKhaiTrieuChung");
            DropTable("dbo.ToKhaiBenhNen");
            DropTable("dbo.ThongBaoKhanCap");
            DropTable("dbo.TrangThaiToKhai");
            DropTable("dbo.ToKhai");
            DropTable("dbo.QuyTrinhToKhai");
            DropTable("dbo.TrieuChung");
            DropTable("dbo.PeopleTrieuChung");
            DropTable("dbo.PeopleBenhNen");
            DropTable("dbo.PatientFtLocation");
            DropTable("dbo.LocationType");
            DropTable("dbo.Location");
            DropTable("dbo.LichSuLuuTru");
            DropTable("dbo.LichSuKiemSoat");
            DropTable("dbo.LichSuDiChuyen");
            DropTable("dbo.SickCondition");
            DropTable("dbo.TinhTrangTheoDoi");
            DropTable("dbo.QuocTich");
            DropTable("dbo.PhuongTien");
            DropTable("dbo.DanToc");
            DropTable("dbo.People");
            DropTable("dbo.CovidPatient");
            DropTable("dbo.CoSoLuuTru");
            DropTable("dbo.ChotKiemSoat");
            DropTable("dbo.BenhNen");
            DropTable("dbo.Area");
        }
    }
}
