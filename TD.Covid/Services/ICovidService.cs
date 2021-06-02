using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Covid.Data.ViewModels;
using TD.Core.Api.Common.Http;

namespace TD.Covid.Services
{
    [ServiceContract]
    public interface ICovidService
    {
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        APIResult GetUserTokenKey(string user, string pass, string tokenDevice);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        APIResult LogoutUser(string token, string tokenDevice);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        APIResult GetDashboard(string token, string areaCode, string fromDate,string toDate, string chotKiemSoatID);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        APIResult GetToKhaiTheoDiaBan(string token,string areaCode, string fromDate, string toDate);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        APIResult GetMenu(string token);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        APIResult GetContentByNavigate(string token,string navigate);

        [OperationContract]
        [WebGet(UriTemplate = "Areas/current",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetCurrentAreas();

        [OperationContract]
        [WebGet(UriTemplate = "Areas/Code/{code}/Children",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetAreaChildren(string code);

        [OperationContract]
        [WebGet(UriTemplate = "Areas/Root",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetAreasByLevelRoot();

        [OperationContract]
        [WebGet(UriTemplate = "Areas/Code/{code}",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetAreasByCode(string code);

        [OperationContract]
        [WebGet(UriTemplate = "TrieuChungs",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetTrieuChungs();

        [OperationContract]
        [WebGet(UriTemplate = "BenhNens",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetBenhNens();

        [OperationContract]
        [WebGet(UriTemplate = "TrangThaiToKhais",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetTrangThaiToKhais();

        [OperationContract]
        [WebGet(UriTemplate = "PhuongTiens",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetPhuongTiens();

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "ToKhais",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Stream AddToKhai(ToKhaiVM toKhai);

        [OperationContract]
        [WebGet(UriTemplate = "ToKhais/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetToKhaiById(string id);

        [OperationContract]
        [WebGet(UriTemplate = "ToKhais?identificationID={identificationID}&provinceFrom={provinceFrom}&provinceTo={provinceTo}" +
            "&districtTo={districtTo}&districtFrom={districtFrom}&wardTo={wardTo}&wardFrom={wardFrom}" +
            "&trangThaiToKhaiID={trangThaiToKhaiID}&phuongTienID={phuongTienID}&bienSo={bienSo}&areaCode={areaCode}&skip={skip}&top={top}&q={q}" +
            "&fromNgayKhoiHanh={fromNgayKhoiHanh}&toNgayKhoiHanh={toNgayKhoiHanh}&fromNgayToi={fromNgayToi}&toNgayToi={toNgayToi}&fromDate={fromNgayTao}&toDate={toNgayTao}" +
            "&chotKiemSoatID={chotKiemSoatID}&denTuVungDich={denTuVungDich}",
             ResponseFormat = WebMessageFormat.Json)]
        Stream GetToKhais(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiID, string phuongTienID, string bienSo, string skip, string top, string q,
            string fromNgayKhoiHanh, string toNgayKhoiHanh, string fromNgayToi, string toNgayToi, string chotKiemSoatID, string areaCode, string fromNgayTao, string toNgayTao, string denTuVungDich);

        [OperationContract]
        [WebGet(UriTemplate = "Peoples?provinceTo={provinceTo}&provinceFrom={provinceFrom}" +
            "&districtTo={districtTo}&districtFrom={districtFrom}&wardTo={wardTo}&wardFrom={wardFrom}" +
            "&tinhTrangTheoDoiID={tinhTrangTheoDoiID}&skip={skip}&top={top}&q={q}&areaCode={areaCode}" +
            "&chotKiemSoatID={chotKiemSoatID}&fromDate={fromDate}&toDate={toDate}",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetPeoples(string areaCode, string chotKiemSoatID, string fromDate,
              string toDate, string provinceTo, string provinceFrom,
              string districtTo, string districtFrom, string wardTo, string wardFrom,
              string tinhTrangTheoDoiID, string skip, string top, string q);

        [OperationContract]
        [WebGet(UriTemplate = "Peoples/identificationID/{identificationID}",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetPeopleByIdentificationID(string identificationID);

        [OperationContract]
        [WebGet(UriTemplate = "Peoples/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetPeopleById(string id);

        [OperationContract]
        [WebGet(UriTemplate = "ChotKiemSoats",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetChotKiemSoats();

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "XacNhan",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream XacNhan(int toKhaiId, int trangThaiToKhaiId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "XacNhanHangLoat",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream XacNhanHangLoat(List<int> toKhaiIds, int trangThaiToKhaiId, string comment, string nguoiKiemSoatName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "ChotKiemSoat/AddNguoiDung",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream AddNguoiDungToChotKiemSoat(int chotKiemSoatID, List<string> nguoiDungs);

        [OperationContract]
        [WebGet(UriTemplate = "ChotKiemSoats/{id}/NguoiDung",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetNguoiDungChotKiemSoat(string id);

        [OperationContract]
        [WebGet(UriTemplate = "QuocTichs",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetQuocTichs();

        [OperationContract]
        [WebGet(UriTemplate = "DanTocs",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetDanTocs();

        [OperationContract]
        [WebGet(UriTemplate = "Area/Code/{code}/Children",
             ResponseFormat = WebMessageFormat.Json)]
        Stream GetChildrenArea(string code);

        [OperationContract]
        [WebGet(UriTemplate = "Area/Root",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetAreasRoot();

    }
}
