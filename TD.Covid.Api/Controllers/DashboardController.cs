
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TD.Covid.Data.Repositories;
using TD.Covid.Data.Repositories.ToKhaiYTe;
using TD.Core.Api.Mvc;
using TD.Core.UserProfiles.Controllers;
using TD.Core.UserProfiles.Models;
using System.Configuration;
using System;

namespace TD.Covid.Api.Controllers.Dashboard
{
    public class DashboardController : TDApiController
    {
        private IToKhaiRepository _repository;
        private IAreaRepository _areaRepository;
        private ITrangThaiToKhaiRepository _trangThaiToKhaiRepository;
        public DashboardController(IToKhaiRepository repository, ITrangThaiToKhaiRepository trangThaiToKhaiRepository, IAreaRepository areaRepository)
        {
            _repository = repository;
            _trangThaiToKhaiRepository = trangThaiToKhaiRepository;
            _areaRepository = areaRepository;
        }

        private class Datum
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        [Route("~/covidapi/dashboardwidget")]
        [HttpGet]
        public IHttpActionResult GetDashboard()
        {
            var areaCode = _repository.GetAreaCodeByCurrentUser();
            var trangThaiToKhais = _trangThaiToKhaiRepository.GetAll();
            List<Datum> _widgetdata = new List<Datum>();
            _widgetdata.Add(new Datum() { text = "Tổng số tờ khai", value = _repository.GetByTrangThai("",areaCode).Count });
            foreach (var trangThaiToKhai in trangThaiToKhais)
            {
                _widgetdata.Add(new Datum() { text = trangThaiToKhai.Name, value = _repository.GetByTrangThai(trangThaiToKhai.Name,areaCode).Count });
            }
            _widgetdata.Add(new Datum() { text = "Đến từ vùng dịch", value = _repository.GetTuNgayDenngay("", areaCode, "", "", "", "", "", "", "", "", "1").Count  });
            _widgetdata.Add(new Datum() { text = "Số người khai báo trong ngày", value = _repository.GetByCreatedAt(DateTime.Today, "", areaCode).Count });
            var widget = new { title = "Thống kê tình hình", data = _widgetdata };

            List<Datum> _chartdata = new List<Datum>();
            for (int i = 6; i >= 0; i--)
            {
                DateTime dtTemp = DateTime.Today.AddDays(-i);
                _chartdata.Add(new Datum() { text = dtTemp.ToString("dd/MM"), value = _repository.GetByCreatedAt(dtTemp, "", areaCode).Count });
            }
            var chart = new { title = "Số lượng tờ khai trong tuần", data = _chartdata };

            List<Datum> _piedata = new List<Datum>();
            foreach (var trangThaiToKhai in trangThaiToKhais)
            {
                _piedata.Add(new Datum() { text = trangThaiToKhai.Name, value = _repository.GetByCreatedAt(DateTime.Today, trangThaiToKhai.Name, areaCode).Count });
            }
            var piechart = new { title = "Số người khai báo trong ngày", data = _piedata };

            var entities = new { widget = widget, chart = chart, piechart = piechart };
            return ApiOk(entities);
        }
        private class DiaBan
        {
            public string text { get; set; }
            public int choxacnhan { get; set; }
            public int dangxuly { get; set; }
            public int daxacnhan { get; set; }
            public int saithongtin { get; set; }
        }
        [Route("~/covidapi/dashboard/tokhaitheodiaban")]
        [HttpGet]
        public IHttpActionResult GetToKhaiTheoDiaBan(string areaCode,string frmDate,string toDate)
        {
            var trangThaiToKhais = _trangThaiToKhaiRepository.GetAll();
            if (String.IsNullOrEmpty(areaCode))
            {
                areaCode = _repository.GetAreaCodeByCurrentUser();
            }
            if (String.IsNullOrEmpty(areaCode))
            {
                areaCode = System.Configuration.ConfigurationManager.AppSettings["ProvinceCode"] + "";
            }
            var areas = _areaRepository.GetByParentCode(areaCode);
            List<DiaBan> _widgetdata = new List<DiaBan>();
            if (areas == null || areas.Count == 0)
            {
                _widgetdata.Add(new DiaBan
                {
                    text = _areaRepository.GetByCode(areaCode).Name,
                    choxacnhan = _repository.GetByTrangThai("Chờ xác nhận", areaCode).Count,
                    dangxuly = _repository.GetByTrangThai("Đang xử lý", areaCode).Count,
                    daxacnhan = _repository.GetByTrangThai("Đã xác nhận", areaCode).Count,
                    saithongtin = _repository.GetByTrangThai("Sai thông tin", areaCode).Count
                });
            }
            foreach (var area in areas)
            {
                _widgetdata.Add(new DiaBan
                {
                    text = area.Name,
                    choxacnhan = _repository.GetByTrangThai("Chờ xác nhận", area.Code).Count,
                    dangxuly = _repository.GetByTrangThai("Đang xử lý", area.Code).Count,
                    daxacnhan = _repository.GetByTrangThai("Đã xác nhận", area.Code).Count,
                    saithongtin = _repository.GetByTrangThai("Sai thông tin", area.Code).Count
                });
            }
            var entities = new { data = _widgetdata };
            return ApiOk(entities);
        }
    }
}
