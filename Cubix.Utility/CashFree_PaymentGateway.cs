using Cubix.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.Utility
{
    public static class CashFree_PaymentGateway
    {
        public static async Task<ResponseModel> CreateConsultationPayment(RequestModel oRequestModel)
        {
            ResponseModel oResponseModel = new ResponseModel();
            try
            {
                //Latest Generated Secure Key
                Stream dataStream;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AppSetting.PaymentGatewayURL + "/api/v1/order/create");
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;

                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

                request.Method = "POST";

                string query = string.Format("appId={0}&secretKey={1}&orderId={2}&orderAmount={3}&orderNote={4}&customerPhone={5}&" +
                    "customerName={6}&customerEmail={7}&notifyUrl={8}&returnUrl={9}",
                    oRequestModel.appId, oRequestModel.secretKey,
                    oRequestModel.orderId, oRequestModel.orderAmount, oRequestModel.orderNote,
                     oRequestModel.customerPhone, oRequestModel.customerName, oRequestModel.customerEmail,
                     oRequestModel.notifyUrl, oRequestModel.returnUrl);

                byte[] byteArray = Encoding.ASCII.GetBytes(query);

                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;

                dataStream = request.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);

                dataStream.Close();

                WebResponse response = request.GetResponse();

                String Status = ((HttpWebResponse)response).StatusDescription;

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                String responseFromServer = reader.ReadToEnd();

                reader.Close();

                dataStream.Close();

                response.Close();
                return JsonConvert.DeserializeObject<ResponseModel>(responseFromServer);
            }
            catch (Exception ex)
            {
                oResponseModel.status = "ERROR";
                Log.LogError(ex);
            }
            return oResponseModel;
        }



        public static async Task<ResponseModel> GetConsultationPaymentStatus(RequestModel oRequestModel)
        {
            ResponseModel oResponseModel = new ResponseModel();
            try
            {
                //Latest Generated Secure Key
                Stream dataStream;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AppSetting.PaymentGatewayURL + "/api/v1/order/info/status");
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;

                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

                request.Method = "POST";

                string query = string.Format("appId={0}&secretKey={1}&orderId={2}",
                    oRequestModel.appId, oRequestModel.secretKey,
                    oRequestModel.orderId);

                byte[] byteArray = Encoding.ASCII.GetBytes(query);

                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;

                dataStream = request.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);

                dataStream.Close();

                WebResponse response = request.GetResponse();

                String Status = ((HttpWebResponse)response).StatusDescription;

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                String responseFromServer = reader.ReadToEnd();

                reader.Close();

                dataStream.Close();

                response.Close();
                return JsonConvert.DeserializeObject<ResponseModel>(responseFromServer);
            }
            catch (Exception ex)
            {
                oResponseModel.status = "ERROR";
                Log.LogError(ex);
            }
            return oResponseModel;
        }



        public static async Task<ResponseModel> RefundRequest(RequestModel oRequestModel)
        {
            ResponseModel oResponseModel = new ResponseModel();
            try
            {
                //Latest Generated Secure Key
                Stream dataStream;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AppSetting.PaymentGatewayURL + "/api/v1/order/refund");
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;

                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

                request.Method = "POST";

                string query = string.Format("appId={0}&secretKey={1}&referenceId={2}&refundAmount={3}&refundNote={4}&refundType={5}",
                    oRequestModel.appId,
                    oRequestModel.secretKey,
                    oRequestModel.referenceId,
                    oRequestModel.refundAmount,
                    oRequestModel.refundNote,
                    oRequestModel.refundType);

                byte[] byteArray = Encoding.ASCII.GetBytes(query);

                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;

                dataStream = request.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);

                dataStream.Close();

                WebResponse response = request.GetResponse();

                String Status = ((HttpWebResponse)response).StatusDescription;

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                String responseFromServer = reader.ReadToEnd();

                reader.Close();

                dataStream.Close();

                response.Close();
                return JsonConvert.DeserializeObject<ResponseModel>(responseFromServer);
            }
            catch (Exception ex)
            {
                oResponseModel.status = "ERROR";
                Log.LogError(ex);
            }
            return oResponseModel;
        }
    }
}
