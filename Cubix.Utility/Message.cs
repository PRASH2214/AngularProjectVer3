using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Cubix.Utility
{
    public class Message
    {
        static readonly HttpClient client = new HttpClient();
        public async static void SendOTP(string MobileNo, String message)
        {
            String query = "http://www.ecubixservices01.co.in/ECPClientSupport/Apps/Transaction/frmSendSMS.aspx?ClientName=eConsultation&UserName=eCubix1809&Password=eCubix$1521&MobileNo=" + MobileNo + "&MsgText=" + message;

            HttpResponseMessage response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // return responseBody;

            //String query = "http://www.ecubixservices01.co.in/ECPClientSupport/Apps/Transaction/frmSendSMS.aspx?ClientName=eConsultation&UserName=eCubix1809&Password=eCubix$1521&MobileNo=" + MobileNo + "&MsgText=" + message;
            //Uri MoodysWebAddress = new Uri(query);
            //HttpWebRequest request = WebRequest.Create(MoodysWebAddress) as HttpWebRequest;
            //request.Method = "GET";
            //request.ContentType = "text/xml";
            //string results = string.Empty;
            //HttpWebResponse response;
            //using (response = request.GetResponse() as HttpWebResponse)
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    results = reader.ReadToEnd();
            //    reader.Close();
            //    //  File.AppendAllText(@"/home/ubuntu/code/publish/wwwroot/SMS/BulkSMS/" + DateTime.Now.ToString("ddMMyy") + "SMSOTPLogs.txt", "Mobile: " + MobileNo + " Time: " + DateTime.Now.ToString() + " Response: " + response.StatusCode.ToString() + " Message :" + message + "\r\n\r");

            //}
            //response.Close();
        }

        //http://www.ecubixservices01.co.in/ECPClientSupport/Apps/Transaction/frmSendSMS.aspx?ClientName=eConsultation&UserName=eCubix1809&Password=eCubix$1521&MobileNo=9041423335&MsgText=Test Message
    }
}
