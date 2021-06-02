using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using TD.Core.Api.Common;
using TD.Core.Api.Common.Http;
using TD.Core.Areas.Controllers;
using Microsoft.SharePoint;
using System.IO;
using System;
using TD.Covid.Data.Repositories.ThongTinKiemSoat;
using Unity;
using Newtonsoft.Json;
using TD.Covid.Data.Repositories.ToKhaiYTe;
using TD.Covid.Data.Model.ToKhaiYTe;
using TD.Covid.Data.Model.ThongTinKiemSoat;
using Microsoft.SharePoint.IdentityModel;
using System.Collections.Generic;
using TD.Covid.Data.ViewModels;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.Model;
using System.Runtime.Serialization;
using TD.Covid.Data.Repositories.ThongTinLuuTru;
using System.Linq;


namespace TD.Covid.Services
{
    [AspNetCompatibilityRequirements(
           RequirementsMode = AspNetCompatibilityRequirementsMode.Required),
    ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class CovidService : ICovidService, IApiWcfJsonService
    {
        private readonly IUnityContainer _unityContainer;
        private readonly ITrieuChungRepository _trieuChungRepository;
        private readonly IBenhNenRepository _benhNenRepository;
        private readonly IToKhaiRepository _toKhaiRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly ILichSuKiemSoatRepository _lichSuKiemSoatRepository;
        private readonly IToKhaiBenhNenRepository _toKhaiBenhNenRepository;
        private readonly IToKhaiTrieuChungRepository _toKhaiTrieuChungRepository;
        private readonly IPeopleBenhNenRepository _peopleBenhNenRepository;
        private readonly IPeopleTrieuChungRepository _peopleTrieuChungRepository;
        private readonly ITrangThaiToKhaiRepository _trangThaiToKhaiRepository;
        private readonly IQuyTrinhToKhaiRepository _quyTrinhToKhaiRepository;
        private readonly IPhuongTienRepository _phuongTienRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IChotKiemSoatRepository _chotKiemSoatRepository;
        private readonly ICoSoLuuTruRepository _coSoLuuTruRepository;
        private readonly IQuocTichRepository _quocTichRepository;
        private readonly IDanTocRepository _danTocRepository;
        public CovidService()
        {
            _unityContainer = new UnityContainer().EnableDiagnostic();
            var integration = new Integration();
            integration.RegisterDIConfig(_unityContainer);

            _trieuChungRepository = _unityContainer.Resolve<ITrieuChungRepository>();
            _benhNenRepository = _unityContainer.Resolve<IBenhNenRepository>();
            _toKhaiRepository = _unityContainer.Resolve<IToKhaiRepository>();
            _peopleRepository = _unityContainer.Resolve<IPeopleRepository>();
            _lichSuKiemSoatRepository = _unityContainer.Resolve<ILichSuKiemSoatRepository>();
            _toKhaiBenhNenRepository = _unityContainer.Resolve<IToKhaiBenhNenRepository>();
            _toKhaiTrieuChungRepository = _unityContainer.Resolve<IToKhaiTrieuChungRepository>();
            _peopleBenhNenRepository = _unityContainer.Resolve<IPeopleBenhNenRepository>();
            _peopleTrieuChungRepository = _unityContainer.Resolve<IPeopleTrieuChungRepository>();
            _trangThaiToKhaiRepository = _unityContainer.Resolve<ITrangThaiToKhaiRepository>();
            _quyTrinhToKhaiRepository = _unityContainer.Resolve<IQuyTrinhToKhaiRepository>();
            _phuongTienRepository = _unityContainer.Resolve<IPhuongTienRepository>();
            _areaRepository = _unityContainer.Resolve<IAreaRepository>();
            _chotKiemSoatRepository = _unityContainer.Resolve<IChotKiemSoatRepository>();
            _coSoLuuTruRepository = _unityContainer.Resolve<ICoSoLuuTruRepository>();
            _quocTichRepository = _unityContainer.Resolve<IQuocTichRepository>();
            _danTocRepository = _unityContainer.Resolve<IDanTocRepository>();
        }

        public HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public WebOperationContext OperationContext
        {
            get
            {
                return WebOperationContext.Current;
            }
        }


        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private Stream GetResultStream(APIResult apiResult)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            string resultString = JsonConvert.SerializeObject(apiResult);
            var resultStream = GenerateStreamFromString(resultString);
            return resultStream;
        }
        public Stream GetCurrentAreas()
        {            
            string code = System.Configuration.ConfigurationManager.AppSettings["ProvinceCode"] + "";
            var data = _areaRepository.GetByCode(code);

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetAreaChildren(string code)
        {
            var data = _areaRepository.GetByParentCode(code);

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }
        public Stream GetAreasByCode(string code)
        {
            var data = _areaRepository.GetByCode(code);
            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetAreasByLevelRoot()
        {
            // code = System.Configuration.ConfigurationManager.AppSettings["ProvinceCode"] + "";
            var data = _areaRepository.GetAreaLevel2();

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetTrieuChungs()
        {
            var data = _trieuChungRepository.GetAll();

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetBenhNens()
        {
            var data = _benhNenRepository.GetAll();

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetTrangThaiToKhais()
        {
            var data = _trangThaiToKhaiRepository.GetAll().Where(x=>x.ID!= 1);// không lấy đang xử lý

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetPeopleById(string idStr)
        {
            var checkId = int.TryParse(idStr, out int id);
            if (!checkId) throw new ApiBadArgumentsException();
            
            var data = _peopleRepository.GetById(id);

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetPeopleByIdentificationID(string IdentificationID)
        {
            var data = _peopleRepository.GetByIdentificationID(IdentificationID);

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetPeoples(string areaCode, string  chotKiemSoatID, string fromDate, string toDate, string provinceTo, string provinceFrom,
              string districtTo, string districtFrom, string wardTo, string wardFrom,
              string tinhTrangTheoDoiID, string skip, string top, string q)
        {
            var data = _peopleRepository.Get(areaCode, chotKiemSoatID, fromDate, toDate, provinceTo, provinceFrom, districtTo, districtFrom,
                wardTo, wardFrom, tinhTrangTheoDoiID, skip, top, q);
            var total = _peopleRepository.Count(areaCode, chotKiemSoatID, fromDate, toDate, provinceTo, provinceFrom, districtTo, districtFrom, wardTo, wardFrom, tinhTrangTheoDoiID, q);

            var apiResult = new APIResult
            {
                data = data,
                total = total
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetPhuongTiens()
        {
            var data = _phuongTienRepository.GetAll();

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream AddToKhai(ToKhaiVM toKhaiVM)
        {
            try
            {
                var toKhaiTmp = new ToKhai(toKhaiVM);
                toKhaiTmp.TrangThaiToKhaiID = 2; // Cho xac nhan

                var toKhai = _toKhaiRepository.Add(toKhaiTmp);

                foreach (var benhNenId in toKhaiVM.BenhNens)
                {
                    _toKhaiBenhNenRepository.Add(
                        new ToKhaiBenhNen
                        {
                            BenhNenId = benhNenId,
                            ToKhaiId = toKhai.ID
                        }
                    );
                }

                foreach (var trieuChungId in toKhaiVM.TrieuChungs)
                {
                    _toKhaiTrieuChungRepository.Add(
                        new ToKhaiTrieuChung
                        {
                            TrieuChungId = trieuChungId,
                            ToKhaiId = toKhai.ID
                        }
                    );
                }

                var checkExistPeople = _peopleRepository.GetByIdentificationID(toKhai.IdentificationID);

                var people = new People(toKhai);

                if (checkExistPeople == null)
                {
                    people = _peopleRepository.Add(people);
                }
                else
                {
                    people = checkExistPeople;
                }

                foreach (var benhNenId in toKhaiVM.BenhNens)
                {
                    _peopleBenhNenRepository.Add(
                        new PeopleBenhNen
                        {
                            BenhNenId = benhNenId,
                            PeopleId = people.ID
                        }
                    );
                }

                foreach (var trieuChungId in toKhaiVM.TrieuChungs)
                {
                    _peopleTrieuChungRepository.Add(
                        new PeopleTrieuChung
                        {
                            TrieuChungId = trieuChungId,
                            PeopleId = people.ID
                        }
                    );
                }


                //var lichSuDiChuyen = new LichSuDiChuyen(toKhai, people);
                //_lichSuDiChuyenRepository.Add(lichSuDiChuyen);

                var lichSuKiemSoat = new LichSuKiemSoat(toKhai, people);
                _lichSuKiemSoatRepository.Add(lichSuKiemSoat);

                _quyTrinhToKhaiRepository.Add(
                    new QuyTrinhToKhai
                    {
                        //Name = "Chốt nhập",
                        ToKhaiID = toKhai.ID
                    }
                );

                var provinceTo = _areaRepository.GetByCode(toKhai.ProvinceCodeTo);
                var districtTo = _areaRepository.GetByCode(toKhai.DistrictCodeTo);
                var wardTo = _areaRepository.GetByCode(toKhai.WardCodeTo);

                var proviceToName = provinceTo == null ? "" : provinceTo.Name;
                var districtToName = districtTo == null ? "" : districtTo.Name;
                var wardToName = wardTo == null ? "" : wardTo.Name;

                var notiService = new NotificationService();
                notiService.SendNotification(toKhai, proviceToName, districtToName, wardToName);

                var apiResult = new APIResult
                {
                    data = true
                };

                var result = GetResultStream(apiResult);
                return result;
            }
            catch (Exception ex)
            {
                var apiResult = new APIResult
                {
                    error = new ErrorResult
                    {
                        code = 500,
                        internalMessage = ex.ToString(),
                        userMessage = ex.ToString()
                    }
                };

                var result = GetResultStream(apiResult);
                return result;
            }
        }

        public Stream GetToKhaiById(string idStr)
        {
            bool checkId = int.TryParse(idStr, out int id);
            if (!checkId) throw new ApiBadArgumentsException();

            var data = _toKhaiRepository.GetByIdIncludeFull(id);

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }


        public Stream GetToKhais(string identificationID, string provinceTo, string provinceFrom,
            string districtTo, string districtFrom, string wardTo, string wardFrom,
            string trangThaiToKhaiID, string phuongTienID, string bienSo, string skip, string top, string q,
            string fromNgayKhoiHanh, string toNgayKhoiHanh, string fromNgayToi, string toNgayToi, string chotKiemSoatID, string areaCode, string fromNgayTao, string toNgayTao, string denTuVungDich)
        {
            var data = _toKhaiRepository.GetAllBase(
                identificationID, provinceTo, provinceFrom,
                districtTo, districtFrom, wardTo, wardFrom,
                trangThaiToKhaiID, phuongTienID, bienSo, skip, top, q,
                fromNgayKhoiHanh, toNgayKhoiHanh, fromNgayToi, toNgayToi, chotKiemSoatID, areaCode, fromNgayTao, toNgayTao, denTuVungDich);

            var total = _toKhaiRepository.Count(identificationID, provinceTo, provinceFrom, districtTo,
                districtFrom, wardTo, wardFrom, trangThaiToKhaiID, phuongTienID, bienSo,
                fromNgayKhoiHanh, toNgayKhoiHanh, fromNgayToi, toNgayToi, chotKiemSoatID,
                areaCode, fromNgayTao, toNgayTao, denTuVungDich, q);

            var apiResult = new APIResult
            {
                data = data,
                total = total
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetChotKiemSoats()
        {
            var data = _chotKiemSoatRepository.GetAll();

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public APIResult GetDashboard(string token,string areaCode, string fromDate, string toDate, string chotKiemSoatID)
        {
            APIResult result = new APIResult();
            try
            {
                string urlRoot = SPContext.Current.Site.RootWeb.Url;
                string payload = APICommon.ValidateJWT(token);
                PayloadJWT payloadJWT = JsonConvert.DeserializeObject<PayloadJWT>(payload);
                string user = payloadJWT.context.user.userName;
                string pass = APICommon.doDecryptAES(payloadJWT.hashpwd);
                bool login = SPClaimsUtility.AuthenticateFormsUser(new Uri(urlRoot), user, pass);
                
                if (login)
                {
                    string groups = "Khai báo y tế##Khai báo cư trú##Kiểm duyệt y tế##Xem báo cáo cấp xã phường##Xem báo cáo cấp quận huyện##Xem báo cáo cấp tỉnh thành##Quản trị danh mục";// tên trong groups
                    var checkuser = CheckUserInGroups(groups);
                    var qr = checkuser.Split('#');
                    var qrKBYT = qr[0];
                    var qrKBCT = qr[1];
                    var qrKDYT = qr[2];
                    var qrXP = qr[3];
                    var qrQH = qr[4];
                    var qrTT = qr[5];
                    var qrQT = qr[6];

                    Widget _widget = new Widget();
                    _widget.title = "Thống kê tình hình";
                    List<Datum> _widgetdata = new List<Datum>();
                    if ((chotKiemSoatID!= null || !String.IsNullOrEmpty(areaCode)) && int.TryParse(chotKiemSoatID, out int chotKiemSoatIdint))
                    {
                        _widgetdata.Add(new Datum() { text = "Tổng số tờ khai", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "", fromDate, toDate, chotKiemSoatID, null).Count, navigate = "BC_DSTKScreen", trangThaiId = "" });
                        //_widgetdata.Add(new Datum() { text = "Đang xử lý", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "1", fromDate, toDate, chotKiemSoatID, null).Count, navigate = "BC_DSTKScreen", trangThaiId = "1" });
                        _widgetdata.Add(new Datum() { text = "Chờ xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "2", fromDate, toDate, chotKiemSoatID, null).Count, navigate = "BC_DSTKScreen", trangThaiId = "2" });
                        _widgetdata.Add(new Datum() { text = "Đã xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "3", fromDate, toDate, chotKiemSoatID, null).Count, navigate = "BC_DSTKScreen", trangThaiId = "3" });
                        _widgetdata.Add(new Datum() { text = "Sai thông tin", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "4", fromDate, toDate, chotKiemSoatID, null).Count, navigate = "BC_DSTKScreen", trangThaiId = "4" });
                        _widgetdata.Add(new Datum() { text = "Đến từ vùng dịch", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "", fromDate, toDate, chotKiemSoatID, "1").Count, navigate = "BC_DSTKScreen", trangThaiId = "", denTuVungDich="1" });
                        _widgetdata.Add(new Datum() { text = "Số người khai báo trong ngày", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "","").Select(q => q.ChotKiemSoatID == chotKiemSoatIdint).Count(), navigate = "BC_DSNKBScreen", trangThaiId = "" });
                    }
                    else 
                    if (areaCode == null || String.IsNullOrEmpty(areaCode))
                    {
                        _widgetdata.Add(new Datum() { text = "Tổng số tờ khai", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "", fromDate, toDate).Count , navigate = "BC_DSTKScreen", trangThaiId ="" });
                        //_widgetdata.Add(new Datum() { text = "Đang xử lý", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "1", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "1" });
                        _widgetdata.Add(new Datum() { text = "Chờ xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "2", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "2" });
                        _widgetdata.Add(new Datum() { text = "Đã xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "3", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "3" });
                        _widgetdata.Add(new Datum() { text = "Sai thông tin", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "4", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "4" });
                        _widgetdata.Add(new Datum() { text = "Đến từ vùng dịch", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", "", "", "", fromDate, toDate, "1").Count, navigate = "BC_DSTKScreen", trangThaiId = "", denTuVungDich = "1" });

                        _widgetdata.Add(new Datum() { text = "Số người khai báo trong ngày", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "",areaCode).Count, navigate = "BC_DSNKBScreen", trangThaiId = "" });
                    }
                    else
                    {
                        switch (areaCode.Length)
                        {
                            case 2:
                                _widgetdata.Add(new Datum() { text = "Tổng số tờ khai", value = _toKhaiRepository.GetTuNgayDenngay("", areaCode, "", "", "", "", "", "", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "" });
                                _widgetdata.Add(new Datum() { text = "Chờ xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", areaCode, "", "", "", "", "", "2", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "2" });
                                _widgetdata.Add(new Datum() { text = "Đã xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", areaCode, "", "", "", "", "", "3", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "3" });
                                _widgetdata.Add(new Datum() { text = "Sai thông tin", value = _toKhaiRepository.GetTuNgayDenngay("", areaCode, "", "", "", "", "", "4", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "4" });
                                _widgetdata.Add(new Datum() { text = "Đến từ vùng dịch", value = _toKhaiRepository.GetTuNgayDenngay("", areaCode, "", "", "", "", "", "", fromDate, toDate, "1").Count, navigate = "BC_DSTKScreen", trangThaiId = "", denTuVungDich = "1" });

                                _widgetdata.Add(new Datum() { text = "Số người khai báo trong ngày", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "",areaCode).Count(), navigate = "BC_DSNKBScreen", trangThaiId = "" });
                                break;
                            case 3:
                            case 4:
                                _widgetdata.Add(new Datum() { text = "Tổng số tờ khai", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", areaCode, "", "", "", "", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "" });                                
                                _widgetdata.Add(new Datum() { text = "Chờ xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", areaCode, "", "", "", "2", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "2" });
                                _widgetdata.Add(new Datum() { text = "Đã xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", areaCode, "", "", "", "3", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "3" });
                                _widgetdata.Add(new Datum() { text = "Sai thông tin", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", areaCode, "", "", "", "4", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "4" });
                                _widgetdata.Add(new Datum() { text = "Đến từ vùng dịch", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", areaCode, "", "", "", "", fromDate, toDate, "1").Count, navigate = "BC_DSTKScreen", trangThaiId = "", denTuVungDich = "1" });
                                _widgetdata.Add(new Datum() { text = "Số người khai báo trong ngày", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "",areaCode).Count(), navigate = "BC_DSNKBScreen", trangThaiId = "" });
                                break;
                            case 5:
                                _widgetdata.Add(new Datum() { text = "Tổng số tờ khai", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", areaCode, "", "", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "" });
                                _widgetdata.Add(new Datum() { text = "Chờ xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", areaCode, "", "2", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "2" });
                                _widgetdata.Add(new Datum() { text = "Đã xác nhận", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", areaCode, "", "3", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "3" });
                                _widgetdata.Add(new Datum() { text = "Sai thông tin", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", areaCode, "", "4", fromDate, toDate).Count, navigate = "BC_DSTKScreen", trangThaiId = "4" });
                                _widgetdata.Add(new Datum() { text = "Đến từ vùng dịch", value = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", areaCode, "", "", fromDate, toDate, "1").Count, navigate = "BC_DSTKScreen", trangThaiId = "", denTuVungDich = "1" });
                                _widgetdata.Add(new Datum() { text = "Số người khai báo trong ngày", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "",areaCode).Count(), navigate = "BC_DSNKBScreen", trangThaiId = "" });
                                break;
                        }
                    }
                    
                    _widget.data = _widgetdata;

                    Piechart _piechart = new Piechart();
                    _piechart.title = "Số người khai báo trong ngày";
                    List<Datum> _piedata = new List<Datum>();
                    _piedata.Add(new Datum() { text = "Chờ xác nhận", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "Chờ xác nhận ", areaCode).Count });
                    _piedata.Add(new Datum() { text = "Đã xác nhận", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "Đã xác nhận", areaCode).Count });
                    _piedata.Add(new Datum() { text = "Sai thông tin", value = _toKhaiRepository.GetByCreatedAt(DateTime.Today, "Sai thông tin", areaCode).Count });
                    _piechart.data = _piedata;

                    Chart _chart = new Chart();
                    _chart.title = "Số lượng tờ khai trong tuần";
                    List<Datum> _chartdata = new List<Datum>();
                    for(int i=6; i >= 0; i--)
                    {
                        DateTime dtTemp = DateTime.Today.AddDays(-i);
                        _chartdata.Add(new Datum() { text = dtTemp.ToString("dd/MM"), value = _toKhaiRepository.GetByCreatedAt(dtTemp, "", areaCode).Count });
                    }
                    _chart.data = _chartdata;

                    Dashboard db = new Dashboard();
                    db.widget = _widget;
                    db.chart = _chart;
                    db.piechart = _piechart;

                    result.data = db;
                }
                else
                {
                    result.error = new ErrorResult()
                    {
                        code = 400,
                        internalMessage = "Tài khoản hoặc mật khẩu không đúng",
                        userMessage = "Tài khoản hoặc mật khẩu không đúng"
                    };
                }
            }
            catch (Exception ex)
            {
                result.error = new ErrorResult()
                {
                    code = 500,
                    internalMessage = ex.ToString(),
                    userMessage = "Có lỗi xảy ra!"
                };
            }
            return result;
        }
        public APIResult GetToKhaiTheoDiaBan(string token, string areaCode,string fromDate, string toDate)
        {
            APIResult result = new APIResult();
            try
            {
                string urlRoot = SPContext.Current.Site.RootWeb.Url;
                string payload = APICommon.ValidateJWT(token);
                PayloadJWT payloadJWT = JsonConvert.DeserializeObject<PayloadJWT>(payload);
                string user = payloadJWT.context.user.userName;
                string pass = APICommon.doDecryptAES(payloadJWT.hashpwd);
                bool login = SPClaimsUtility.AuthenticateFormsUser(new Uri(urlRoot), user, pass);
                if (login)
                {
                    string groups = "Khai báo y tế##Khai báo cư trú##Kiểm duyệt y tế##Xem báo cáo cấp xã phường##Xem báo cáo cấp quận huyện##Xem báo cáo cấp tỉnh thành##Quản trị danh mục";// tên trong groups
                    var checkuser = CheckUserInGroups(groups);
                    var qr = checkuser.Split('#');
                    var qrKBYT = qr[0];
                    var qrKBCT = qr[1];
                    var qrKDYT = qr[2];
                    var qrXP = qr[3];
                    var qrQH = qr[4];
                    var qrTT = qr[5];
                    var qrQT = qr[6];

                    List<DiaBan> db = new List<DiaBan>();
                    if (areaCode == null || String.IsNullOrEmpty(areaCode))
                    {
                        areaCode = System.Configuration.ConfigurationManager.AppSettings["ProvinceCode"] + "";
                    }
                    var areas = _areaRepository.GetByParentCode(areaCode);
                    foreach (var area in areas)
                    {
                        switch (area.Code.Length)
                        {
                            case 2:
                                db.Add(new DiaBan
                                {
                                    text = area.Name,
                                    choxacnhan = _toKhaiRepository.GetTuNgayDenngay("", area.Code, "", "", "", "", "", "2", fromDate, toDate).Count,
                                    dangxuly = _toKhaiRepository.GetTuNgayDenngay("", area.Code, "", "", "", "", "", "1", fromDate, toDate).Count,
                                    daxacnhan = _toKhaiRepository.GetTuNgayDenngay("", area.Code, "", "", "", "", "", "3", fromDate, toDate).Count,
                                    saithongtin = _toKhaiRepository.GetTuNgayDenngay("", area.Code, "", "", "", "", "", "4", fromDate, toDate).Count
                                });
                                break;
                            case 3:
                            case 4:
                                db.Add(new DiaBan
                                {
                                    text = area.Name,
                                    choxacnhan = _toKhaiRepository.GetTuNgayDenngay("", "", "", area.Code, "", "", "", "2", fromDate, toDate).Count,
                                    dangxuly = _toKhaiRepository.GetTuNgayDenngay("", "", "", area.Code, "", "", "", "1", fromDate, toDate).Count,
                                    daxacnhan = _toKhaiRepository.GetTuNgayDenngay("", "", "", area.Code, "", "", "", "3", fromDate, toDate).Count,
                                    saithongtin = _toKhaiRepository.GetTuNgayDenngay("", "", "", area.Code, "", "", "", "4", fromDate, toDate).Count
                                });
                                break;
                            case 5:
                                db.Add(new DiaBan
                                {
                                    text = area.Name,
                                    choxacnhan = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", area.Code, "", "2", fromDate, toDate).Count,
                                    dangxuly = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", area.Code, "", "1", fromDate, toDate).Count,
                                    daxacnhan = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", area.Code, "", "3", fromDate, toDate).Count,
                                    saithongtin = _toKhaiRepository.GetTuNgayDenngay("", "", "", "", "", area.Code, "", "4", fromDate, toDate).Count
                                });
                                break;
                        }
                    }
                    result.data = db;
                }
                else
                {
                    result.error = new ErrorResult()
                    {
                        code = 400,
                        internalMessage = "Tài khoản hoặc mật khẩu không đúng",
                        userMessage = "Tài khoản hoặc mật khẩu không đúng"
                    };
                }
            }
            catch (Exception ex)
            {
                result.error = new ErrorResult()
                {
                    code = 500,
                    internalMessage = ex.ToString(),
                    userMessage = "Có lỗi xảy ra!"
                };
            }
            return result;
        }

        public APIResult GetMenu(string token)
        {
            APIResult result = new APIResult();
            try
            {
                string urlRoot = SPContext.Current.Site.Url;
                string payload = APICommon.ValidateJWT(token);
                PayloadJWT payloadJWT = JsonConvert.DeserializeObject<PayloadJWT>(payload);
                string user = payloadJWT.context.user.userName;
                string pass = APICommon.doDecryptAES(payloadJWT.hashpwd);
                bool login = SPClaimsUtility.AuthenticateFormsUser(new Uri(urlRoot), user, pass);
                if (login)
                {
                    string groups = "Khai báo y tế##Khai báo cư trú##Kiểm duyệt y tế##Xem báo cáo cấp xã phường##Xem báo cáo cấp quận huyện##Xem báo cáo cấp tỉnh thành##Quản trị danh mục";// tên trong groups
                    var checkuser = CheckUserInGroups(groups);
                    var qr = checkuser.Split('#');
                    var qrKBYT = qr[0];
                    var qrKBCT = qr[1];
                    var qrKDYT = qr[2];
                    var qrXP = qr[3];
                    var qrQH = qr[4];
                    var qrTT = qr[5];
                    var qrQT = qr[6];
                    var lstMenu = new List<Menu>();
                    if (qrQT == "1" || qrKBYT == "1")
                    {
                        lstMenu.Add(new Menu
                        {
                            appid = 1,
                            name = "Khai báo",
                            navigate = "DeclarationEditScreen",
                            icon = "clipboard-list-check",
                            color = "#0E2D7D"
                        });
                    }
                    if (qrQT == "1" || qrKBCT == "1")
                    {

                    }
                    if (qrQT == "1" || qrKDYT == "1")
                    {
                        lstMenu.Add(new Menu
                        {
                            appid = 2,
                            name = "Kiểm duyệt",
                            navigate = "KD_MainScreen",
                            icon = "clipboard-check",
                            color = "#AF1A16"
                        });
                        lstMenu.Add(new Menu
                        {
                            appid = 3,
                            name = "Người khai báo",
                            navigate = "NKB_MainScreen",
                            icon = "users",
                            color = "#5B63EC"
                        });
                    }
                    if (qrQT == "1" || qrXP == "1" || qrQH == "1" || qrTT == "1")
                    {
                        lstMenu.Add(new Menu
                        {
                            appid = 4,
                            name = "Thống kê",
                            navigate = "BC_MainScreen",
                            icon = "chart-pie",
                            color = "#00796b"
                        });
                    }
                    result.data = lstMenu;
                }
                else
                {
                    result.error = new ErrorResult()
                    {
                        code = 400,
                        internalMessage = "Tài khoản hoặc mật khẩu không đúng",
                        userMessage = "Tài khoản hoặc mật khẩu không đúng"
                    };
                }
            }
            catch (Exception ex)
            {
                result.error = new ErrorResult()
                {
                    code = 500,
                    internalMessage = ex.ToString(),
                    userMessage = "Có lỗi xảy ra!"
                };
            }
            return result;
            //return APICommon.ObjectToJson(result);
        }

        public APIResult GetContentByNavigate(string token, string navigate)
        {
            APIResult result = new APIResult();
            try
            {
                string urlRoot = SPContext.Current.Site.RootWeb.Url;
                string payload = APICommon.ValidateJWT(token);
                PayloadJWT payloadJWT = JsonConvert.DeserializeObject<PayloadJWT>(payload);
                string user = payloadJWT.context.user.userName;
                string pass = APICommon.doDecryptAES(payloadJWT.hashpwd);
                bool login = SPClaimsUtility.AuthenticateFormsUser(new Uri(urlRoot), user, pass);
                if (login)
                {
                    string groups = "Khai báo y tế##Khai báo cư trú##Kiểm duyệt y tế##Xem báo cáo cấp xã phường##Xem báo cáo cấp quận huyện##Xem báo cáo cấp tỉnh thành##Quản trị danh mục";// tên trong groups
                    var checkuser = CheckUserInGroups(groups);
                    var qr = checkuser.Split('#');
                    var qrKBYT = qr[0];
                    var qrKBCT = qr[1];
                    var qrKDYT = qr[2];
                    var qrXP = qr[3];
                    var qrQH = qr[4];
                    var qrTT = qr[5];
                    var qrQT = qr[6];
                    switch (navigate)
                    {
                        case "ListNguoiKhaiBao":

                            break;
                        case "BC_MainScreen":

                            break;
                    }
                }
                else
                {
                    result.error = new ErrorResult()
                    {
                        code = 400,
                        internalMessage = "Tài khoản hoặc mật khẩu không đúng",
                        userMessage = "Tài khoản hoặc mật khẩu không đúng"
                    };
                }
            }
            catch (Exception ex)
            {
                result.error = new ErrorResult()
                {
                    code = 500,
                    internalMessage = ex.ToString(),
                    userMessage = "Có lỗi xảy ra!"
                };
            }
            return result;
        }

        public Stream XacNhan(int toKhaiId, int trangThaiToKhaiId)
        {
            var toKhai = _toKhaiRepository.GetById(toKhaiId);
            toKhai.TrangThaiToKhaiID = trangThaiToKhaiId == 2 || trangThaiToKhaiId == 3 || trangThaiToKhaiId == 4 ? trangThaiToKhaiId : 1;
            var tmp = _toKhaiRepository.Update(toKhai);

            var quyTrinhToKhai = new QuyTrinhToKhai
            {
                ToKhaiID = tmp.ID,
                //Name = trangThaiToKhaiId == 2 ? "Chờ xác nhận" : trangThaiToKhaiId == 3 ? "Đã xác nhận" : trangThaiToKhaiId == 4 ? "Sai thông tin" : ""
            };

            _quyTrinhToKhaiRepository.Add(quyTrinhToKhai);

            var apiResult = new APIResult
            {
                data = tmp
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream XacNhanHangLoat(List<int> toKhaiIds, int trangThaiToKhaiId, string comment, string nguoiKiemSoatName)
        {
            foreach (var toKhaiId in toKhaiIds)
            {
                var toKhai = _toKhaiRepository.GetById(toKhaiId);
                if (toKhai != null)
                {
                    toKhai.TrangThaiToKhaiID = trangThaiToKhaiId == 2 || trangThaiToKhaiId == 3 || trangThaiToKhaiId == 4 ? trangThaiToKhaiId : 1;
                    toKhai.GhiChuDiChuyen = comment;
                }
                var tmp = _toKhaiRepository.Update(toKhai);

                var quyTrinhToKhai = new QuyTrinhToKhai
                {
                    ToKhaiID = tmp.ID,
                    
                    //Name = trangThaiToKhaiId == 2 ? "Chờ xác nhận" : trangThaiToKhaiId == 3 ? "Đã xác nhận" : trangThaiToKhaiId == 4 ? "Sai thông tin" : ""
                };

                _quyTrinhToKhaiRepository.Add(quyTrinhToKhai);


                var checkExistPeople = _peopleRepository.GetByIdentificationID(toKhai.IdentificationID);

                var lichSuKiemSoat = new LichSuKiemSoat(toKhai, checkExistPeople, comment, trangThaiToKhaiId, nguoiKiemSoatName);
                _lichSuKiemSoatRepository.Add(lichSuKiemSoat);
            }

            var toKhaiTmp = _toKhaiRepository.GetById(toKhaiIds.First());

            var provinceTo = _areaRepository.GetByCode(toKhaiTmp.ProvinceCodeTo);
            var districtTo = _areaRepository.GetByCode(toKhaiTmp.DistrictCodeTo);
            var wardTo = _areaRepository.GetByCode(toKhaiTmp.WardCodeTo);

            var proviceToName = provinceTo == null ? "" : provinceTo.Name;
            var districtToName = districtTo == null ? "" : districtTo.Name;
            var wardToName = wardTo == null ? "" : wardTo.Name;

            var notiService = new NotificationService();
            notiService.SendNotification(toKhaiIds, toKhaiTmp , proviceToName, districtToName, wardToName, trangThaiToKhaiId);

            var apiResult = new APIResult
            {
                data = toKhaiIds
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream AddNguoiDungToChotKiemSoat(int chotKiemSoatID, List<string> nguoiDungs)
        {
            //var data = _chotKiemSoatRepository.AddDanhSachNguoiDung(chotKiemSoatID, nguoiDungs);

            var apiResult = new APIResult
            {
                //data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetNguoiDungChotKiemSoat(string idStr)
        {
            var checkId = int.TryParse(idStr, out int id);
            if (!checkId) throw new ApiBadArgumentsException();

            //var data = _chotKiemSoatRepository.GetDanhSachNguoiDung(id);
            var apiResult = new APIResult
            {
                //data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetQuocTichs()
        {
            var data = _quocTichRepository.GetAll();
            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetDanTocs()
        {
            var data = _danTocRepository.GetAll();
            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetChildrenArea(string code)
        {
            var data = _areaRepository.GetByParentCode(code);

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }

        public Stream GetAreasRoot()
        {
            var data = _areaRepository.GetAreaLevel2();

            var apiResult = new APIResult
            {
                data = data
            };

            var result = GetResultStream(apiResult);
            return result;
        }
        
    }
}
