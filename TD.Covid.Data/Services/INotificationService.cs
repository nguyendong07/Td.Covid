using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Covid.Data.Model.ToKhaiYTe;

namespace TD.Covid.Data.Services
{
    public interface INotificationService
    {
        void SendNotification(ToKhai toKhai, string province, string district, string ward);
        void SendNotification(ToKhai toKhai, string province, string district, string ward, int trangThaiToKhaiID);
        void SendNotification(List<int> toKhaiIDs, ToKhai toKhaiTmp, string province, string district, string ward, int trangThaiToKhaiID);

    }
}
