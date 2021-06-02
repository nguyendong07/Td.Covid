using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Builder;
using JWT.Exceptions;
using TD.Core.Api.Common.Http;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Microsoft.SharePoint.IdentityModel;
using Microsoft.SharePoint;
using Newtonsoft.Json;
using System.Linq;
using TD.Core.Areas.Models;
using TD.Core.UserProfiles.Controllers;
using TD.Core.UserProfiles.Models;

namespace TD.Covid.Services
{
    [DataContract]
    [KnownType(typeof(List<string>))]
    [KnownType(typeof(API_DataLogin))]
    [KnownType(typeof(List<Menu>))]
    [KnownType(typeof(Chart))]
    [KnownType(typeof(Piechart))]
    [KnownType(typeof(Widget))]
    [KnownType(typeof(Dashboard))]
    [KnownType(typeof(List<Datum>))]
    [KnownType(typeof(List<DiaBan>))]
    [KnownType(typeof(Area))]
    [KnownType(typeof(JsonResult))]
    [Serializable]
    public class APIResult
    {
        [DataMember]
        public object data { get; set; }
        [DataMember]
        public ErrorResult error { get; set; }
        [DataMember]
        public int total { get; set; }

        public APIResult()
        {
            this.data = null;
            this.error = new ErrorResult();
        }
    }

    [DataContract]
    public class ErrorResult
    {
        [DataMember]
        public string userMessage { get; set; }
        [DataMember]
        public string internalMessage { get; set; }
        [DataMember]
        public int code { get; set; }
        public ErrorResult()
        {
            this.userMessage = string.Empty;
            this.internalMessage = string.Empty;
            this.code = 200;
        }
    }

    public class Menu
    {
        public int appid { get; set; }
        public string name { get; set; }
        public string navigate { get; set; }
        public string icon { get; set; }
        public string color { get; set; }
        public Menu()
        {
            this.appid = 0;
            this.name = string.Empty;
            this.navigate = string.Empty;
            this.icon = string.Empty;
            this.color = string.Empty;
        }
    }
    public class API_DataLogin
    {
        public string token { get; set; }
        public string identifier { get; set; }
        public string soHoKhau { get; set; }
        public string avartar { get; set; }
        public string fullName { get; set; }
        public string birthday { get; set; }
        public string address { get; set; }
        public string sex { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string areacode { get; set; }
        public string permission { get; set; }
        public int chotkiemsoatid { get; set; }
        public int cosoluutruid { get; set; }
        public API_DataLogin()
        {
            this.token = string.Empty;
            this.soHoKhau = string.Empty;
            this.avartar = string.Empty;
            this.fullName = string.Empty;
            this.birthday = string.Empty;
            this.address = string.Empty;
            this.sex = string.Empty;
            this.phoneNumber = string.Empty;
            this.email = string.Empty;
            this.areacode = string.Empty;
            this.permission = string.Empty;
            this.chotkiemsoatid = 0;
            this.cosoluutruid = 0;
        }
    }

    public class Account
    {
        public string user { get; set; }
        public string pass { get; set; }
    }
    public class DiaBan
    {
        public string text { get; set; }
        public int choxacnhan { get; set; }
        public int dangxuly { get; set; }
        public int daxacnhan { get; set; }
        public int saithongtin { get; set; }
    }
    public class Datum
    {
        public string text { get; set; }
        public int value { get; set; }
        public string navigate { get; set; }
        public string trangThaiId { get; set; }
        public string denTuVungDich { get; set; }
    }

    public class Widget
    {
        public string title { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Piechart
    {
        public string title { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Chart
    {
        public string title { get; set; }
        public List<Datum> data { get; set; }
    }
    public class Dashboard
    {
        public Widget widget { get; set; }
        public Chart chart { get; set; }
        public Piechart piechart { get; set; }
    }
    public partial class CovidService : ICovidService, IApiWcfJsonService
    {
        /// <summary>
        /// Lay toke dang nhap của cán bộ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public APIResult GetUserTokenKey(string user, string pass, string tokenDevice)
        {
            APIResult result = new APIResult();
            API_DataLogin _user = new API_DataLogin();
            try
            {
                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
                {
                    string urlRoot = SPContext.Current.Site.RootWeb.Url;
                    bool login = SPClaimsUtility.AuthenticateFormsUser(new Uri(urlRoot), user, pass);
                    
                    if (login)
                    {
                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {
                            using (SPSite oSite = new SPSite(urlRoot))
                            {
                                //var webApp = oSite.WebApplication;
                                //var zone = oSite.Zone;

                                //UserProfileController userProfileCtrlr = new UserProfileController(webApp, zone);
                                //UserProfile obj = userProfileCtrlr.GetByCurrentUser();
                                SPWeb rootWeb = oSite.RootWeb;
                                SPList lstUser = rootWeb.Lists["UserProfiles"];
                                SPListItem obj = GetByUser(lstUser, user);
                                if (obj != null)
                                {
                                    _user.fullName = obj["FullName"] + "";
                                    _user.birthday = obj["Birthday"] != null ? Convert.ToDateTime(obj["Birthday"]).ToString("dd /MM/yyyy") : "";
                                    _user.sex = obj["Sex"] + "";
                                    _user.phoneNumber = obj["Phone"] + "";
                                    _user.email = obj["Email"] + "";
                                    _user.avartar = obj["Avatar"] + "";
                                    _user.address = obj["Address"] + "";
                                    _user.areacode = obj["AreaCode"] + "";
                                }
                                else
                                {
                                    _user.fullName = user;
                                }
                            }
                            using (SPSite oSite = new SPSite(urlRoot+"/sites/covid"))
                            {
                                SPWeb rootWeb = oSite.RootWeb;
                                SPList lstUser = rootWeb.Lists["Users"];
                                //var webApp = oSite.WebApplication;
                                //var zone = oSite.Zone;

                                //SPUser currUser = SPContext.Current.Site.RootWeb.Users[user];
                                var query = new SPQuery()
                                {
                                    Query = "<Where><Eq><FieldRef Name='UserAccount' /><Value Type='Lookup'>i:0#.f|provider|" + user + "</Value></Eq></Where>",
                                    RowLimit = 1
                                };
                                var spItems = lstUser.GetItems(query);
                                if (spItems != null && spItems.Count > 0)
                                {
                                    _user.permission = spItems[0]["Permissions"].ToString();
                                }
                            }
                        });
                        //_user.chotkiemsoatid = _chotKiemSoatRepository.GetChotKiemSoatTheoNguoiDung(user);
                        //_user.cosoluutruid = _coSoLuuTruRepository.GetCoSoLuuTruTheoNguoiDung(user);
                        PayloadJWT token = new PayloadJWT()
                        {
                            iat = (int)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                            exp = (int)DateTimeOffset.UtcNow.AddYears(3).Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                            sub = user,
                            hashpwd = APICommon.doEncryptAES(pass),
                            context = new UserContext()
                            {
                                user = new UserAPI()
                                {
                                    displayName = _user.fullName,
                                    userName = user
                                }
                            }
                        };
                        _user.token = APICommon.CreateJWT(token);
                        result.data = _user;

                        //Add user token
                        //AddUserToken(user, tokenDevice);
                    }
                    else
                    {
                        result.error = new ErrorResult()
                        {
                            code = 401,
                            userMessage = "Không xác thực thành công!"
                        };
                    }
                }
                else
                {
                    result.error = new ErrorResult()
                    {
                        code = 405,
                        userMessage = "Tài khoản hoặc mật khẩu không được để trống!"
                    };
                }
            }
            catch (Exception ex)
            {
                result.error = new ErrorResult()
                {
                    code = 500,
                    userMessage = "Có lỗi xảy ra!",
                    internalMessage = ex.ToString()
                };
            }
            //return APICommon.ObjectToJson(result);
            return result;
        }
        public SPListItem GetByUser(SPList lstUserProfile, string account)
        {
            try
            {
                var query = new SPQuery()
                {
                    Query = "<Where><Eq><FieldRef Name='Account'/><Value Type='Text'>" + account + "</Value></Eq></Where>",
                    RowLimit = 1
                };
                var spItems = lstUserProfile.GetItems(query);
                if (spItems != null && spItems.Count > 0)
                {
                    return spItems[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public APIResult LogoutUser(string token, string tokenDevice)
        {
            APIResult result = new APIResult();
            try
            {
                string validate = APICommon.ValidateJWT(token);
                try
                {
                    PayloadJWT payloadJWT = JsonConvert.DeserializeObject<PayloadJWT>(validate);
                    string pass = APICommon.doDecryptAES(payloadJWT.hashpwd);
                    string user = payloadJWT.context.user.userName;
                    result.data = "success";
                }
                catch (Exception)
                {
                    result.error = new ErrorResult()
                    {
                        code = 500,
                        internalMessage = validate,
                        userMessage = validate
                    };
                }

            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public string CheckUserInGroups(string _groups)
        {
            string result = "";
            string urlRoot = SPContext.Current.Site.Url;
            SPUser curUser = SPContext.Current.Web.CurrentUser;
            try
            {
                using (SPSite osite = new SPSite(urlRoot+"/sites/covid"))
                {
                    using (SPWeb oweb = osite.OpenWeb())
                    {
                        curUser = oweb.CurrentUser;
                        string[] groups = _groups.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string group in groups)
                        {
                            if (IsUserMemberOfGroup(curUser, group) == true)
                                result += "1#";
                            else
                                result += "0#";
                        }
                    }
                }
            }
            catch (Exception ex)
            { result += ex.ToString(); }
            return result;
        }
        public string GetAreaCodeByCurrentUser()
        {
            string areaCode = string.Empty;
            string urlRoot = SPContext.Current.Site.RootWeb.Url;
            using (SPSite oSite = new SPSite(urlRoot))
            {
                var webApp = oSite.WebApplication;
                var zone = oSite.Zone;

                UserProfileController userProfileCtrlr = new UserProfileController(webApp, zone);
                UserProfile obj = userProfileCtrlr.GetByCurrentUser();
                areaCode = obj.AreaCode;
            }
            return areaCode;
        }
        public static bool IsUserMemberOfGroup(SPUser user, string groupName)
        {
            if (String.IsNullOrEmpty(groupName) || user == null) return false;
            return user.Groups.Cast<SPGroup>().Any(@group => @group.Name == groupName);
        }

        public Stream GetToKhais(string q, string provinceCode, string districtCode, string wardCode, string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }
    }
}
