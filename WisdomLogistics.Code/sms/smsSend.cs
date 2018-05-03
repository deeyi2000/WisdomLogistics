/*******************************************************************************
 * Copyright © 2018 德州蓝湖网络科技有限公司 版权所有
 * Author: 张艳军
 * Description: 智慧物流管理平台
 * Website：http://www.wxopens.com
*********************************************************************************/
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WisdomLogistics.Code.sms
{
    public class smsSend
    {
        public static string PostUrl = ConfigurationManager.AppSettings["WebReference.Service.PostUrl"];

        public static string userName = ConfigurationManager.AppSettings["UserName"];
        public static string password = ConfigurationManager.AppSettings["Password"];
        public static void SendSMSMesage(string phone, string contents)
        {
            try
            {
                string[] sArrayPhone = phone.Split(',');
                foreach (string sPhone in sArrayPhone)
                {

                    string content = contents.Trim() + "【智慧物流】";
                    string postStrTpl = "un={0}&pw={1}&phone={2}&msg={3}&rd=1";
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] postData = encoding.GetBytes(string.Format(postStrTpl, userName, password, sPhone, content));
                    System.GC.Collect();
                    HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(PostUrl);
                    myRequest.KeepAlive = false;
                    myRequest.Method = "POST";
                    myRequest.ContentType = "application/x-www-form-urlencoded";
                    myRequest.ContentLength = postData.Length;
                    myRequest.Timeout = 5000;
                    Stream newStream = myRequest.GetRequestStream();
                    // Send the data.
                    newStream.Write(postData, 0, postData.Length);
                    newStream.Flush();
                    newStream.Close();

                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        //LabelRetMsg.Text = reader.ReadToEnd();
                        string result = reader.ReadToEnd();
                        myResponse.Close();
                        myRequest.Abort();
                    }
                    else
                    {
                        myRequest.Abort();
                        myResponse.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
