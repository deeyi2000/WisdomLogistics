/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using System.Configuration;
using System.Web;

namespace WisdomLogistics.Code
{
    public sealed class Licence
    {
        public static bool IsLicence(string key)
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            if (host.Equals("localhost"))
                return true;
            string licence = ConfigurationManager.AppSettings["LicenceKey"];
            if (licence != null && licence == Md5.md5(key, 32))
                return true;

            return false;
        }
        public static string GetLicence()
        {
            var licence = Configs.GetValue("LicenceKey");
            if (string.IsNullOrEmpty(licence))
            {
                licence = Common.GuId();
                Configs.SetValue("LicenceKey", licence);
            }
            return Md5.md5(licence, 32);
        }
    }
}
