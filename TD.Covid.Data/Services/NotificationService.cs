using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Services
{
    public class NotificationService : INotificationService
    {
        public void SendNotification(ToKhai toKhai, string province, string district, string ward)
        {
            var client = new RestClient("https://api.dienbien.gov.vn/NOTIFI/v1/notifi/sendtoTopic")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer d8d917e6-7b5f-373b-ad46-808fee2798ed");
            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                topics = new List<string>
                {
                    toKhai.WardCodeTo,
                    toKhai.DistrictCodeTo
                },
                registrationTokens = new List<string>(),
                notification = new
                {
                    title = "Khai báo y tế mới",
                    body = $"{toKhai.Name} khai báo y tế tại {toKhai.StationTo}, {ward}, {district}, {province}"
                },
                appType = "COVID_Drawer",
                data = new
                {
                    ID = toKhai.ID.ToString(),
                    appType = "COVID_Drawer"
                }
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            client.Execute(request);
        }

        public void SendNotification(ToKhai toKhai, string province, string district, string ward, int trangThaiToKhaiID)
        {
            var client = new RestClient("https://api.dienbien.gov.vn/NOTIFI/v1/notifi/sendtoTopic")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer d8d917e6-7b5f-373b-ad46-808fee2798ed");
            request.AddHeader("Content-Type", "application/json");

            var title = "";
            var body = "";
            if (trangThaiToKhaiID == 2)
            {
                title = "Khai báo y tế mới";
                body = $"{toKhai.Name} khai báo y tế tại {toKhai.StationTo}, {ward}, {district}, {province}";
            } else if (trangThaiToKhaiID == 4)
            {
                title = "Xác nhận sai thông tin";
                body = $"{toKhai.Name} khai báo sai";
            }

            var body1 = new
            {
                topics = new List<string>
                {
                    toKhai.WardCodeTo,
                    toKhai.DistrictCodeTo
                },
                registrationTokens = new List<string>(),
                notification = new
                {
                    title = title,
                    body = body
                },
                appType = "COVID_Drawer",
                data = new
                {
                    ID = toKhai.ID.ToString(),
                    appType = "COVID_Drawer"
                }
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(body1), ParameterType.RequestBody);
            client.Execute(request);
        }

        public void SendNotification(List<int> toKhaiIDs, ToKhai toKhaiTmp, string province, string district, string ward, int trangThaiToKhaiID)
        {
            var client = new RestClient("https://api.dienbien.gov.vn/NOTIFI/v1/notifi/sendtoTopic");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer d8d917e6-7b5f-373b-ad46-808fee2798ed");
            request.AddHeader("Content-Type", "application/json");

            var count = toKhaiIDs.Count;

            if (trangThaiToKhaiID != 2 || trangThaiToKhaiID != 4) return;

            var body = new
            {
                topics = new List<string>
                {
                    toKhaiTmp.WardCodeTo,
                    toKhaiTmp.DistrictCodeTo
                },
                registrationTokens = new List<string>(),
                notification = new
                {
                    title = trangThaiToKhaiID == 2 ? $"{count} tờ khai mới!" : $"{count} tờ khai sai thông tin",
                    body = trangThaiToKhaiID == 2 ? $"Có {count} tờ khai mới tại {toKhaiTmp.StationTo}, {ward}, {district}, {province}"
                    : $"Có {count} tờ khai sai thông tin tại {toKhaiTmp.StationTo}, {ward}, {district}, {province}"
                },
                appType = "COVID_Drawer",
                data = new
                {
                    ID = toKhaiTmp.ID.ToString(),
                    appType = "COVID_Drawer"
                }
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            client.Execute(request);
        }
    }
}
