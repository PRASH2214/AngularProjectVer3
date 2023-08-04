using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers
{
    public class ResponseController : Controller
    {
        private readonly IAuth _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Auth Controller Comstructor
        /// </summary>
        public ResponseController(IAuth user, IWebHostEnvironment hostingEnvironment)
        {
            _srv = user;
            _hostingEnvironment= hostingEnvironment;
        }


        [HttpPost]
        // GET: /<controller>/
        public IActionResult Index(IFormCollection form)
        {
            string secretKey = AppSetting.PaymentSecretKey;
            string orderId = Request.Form["orderId"];
            string orderAmount = Request.Form["orderAmount"];
            string referenceId = Request.Form["referenceId"];
            string txStatus = Request.Form["txStatus"];
            string paymentMode = Request.Form["paymentMode"];
            string txMsg = Request.Form["txMsg"];
            string txTime = Request.Form["txTime"];
            string signature = Request.Form["signature"];

            string signatureData = orderId + orderAmount + referenceId + txStatus + paymentMode + txMsg + txTime;

            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string computedsignature = Convert.ToBase64String(gensignature);
            if (signature == computedsignature)
            {
                int paymentStatus = Constants.PAYMENT_SUCCESS;
                if (txStatus=="CANCELLED")
                {
                    paymentStatus = Constants.PAYMENT_CANCELLED;
                }
                ResultModel<PatientTeleConsultationReg> oPatientTeleConsultationReg = _srv.UpdateTeleConsultationAfterPayment(orderId, paymentStatus, referenceId, paymentMode).Result;
                if (oPatientTeleConsultationReg != null && oPatientTeleConsultationReg.Model != null && paymentStatus == Constants.PAYMENT_SUCCESS)
                {
                    Message.SendOTP(oPatientTeleConsultationReg.Model.Mobile, "Dear User, Your Slot (" + oPatientTeleConsultationReg.Model.SlotFromTime + "-" + oPatientTeleConsultationReg.Model.SlotFromTime + ") is fixed with  ReferenceNumber: " + orderId + " on " + oPatientTeleConsultationReg.Model.AppointmentDate.ToString("dd/MM/yyyy"));

                }
                ViewData["panel"] = "panel panel-success";
                ViewData["heading"] = "Signature Verification Successful";
            }
            else
            {

                ViewData["panel"] = "panel panel-danger";
                ViewData["heading"] = "Signature Verification Failed";
            }
            ViewData["orderId"] = orderId;
            ViewData["orderAmount"] = orderAmount;
            ViewData["referenceId"] = referenceId;
            ViewData["txStatus"] = txStatus;
            ViewData["txMsg"] = txMsg;
            ViewData["txTime"] = txTime;
            ViewData["paymentMode"] = paymentMode;
            return View();
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetBytes(text);
        }

        [HttpPost]
        public string PatientProfileFileUpload(string file, string fileName, string folderName, int FolderId)
        {
            string jsonOutString = "";
            DataSet dsResult = new DataSet();
            DataTable dtStatus = new DataTable();

            DataColumn dcColumn = new DataColumn("isValid", Type.GetType("System.Boolean"));
            dcColumn.DefaultValue = true;
            dtStatus.Columns.Add(dcColumn);

            dcColumn = new DataColumn("Message", Type.GetType("System.String"));
            dcColumn.DefaultValue = "";
            dtStatus.Columns.Add(dcColumn);

            DataRow dr = dtStatus.NewRow();
            dtStatus.Rows.Add(dr);
            dtStatus.TableName = "Status";

            try
            {
                file = file.Replace(" ", "+");
                byte[] encodedData = System.Convert.FromBase64String(file);
                string webRootPath = _hostingEnvironment.WebRootPath;//Get Root Path
                string fileDestFolder = AppSetting.DocPath + folderName + "\\" + FolderId;
                webRootPath = webRootPath + "/" + fileDestFolder;

                if (!Directory.Exists(webRootPath))
                {
                    Directory.CreateDirectory(webRootPath);
                }
                string docPath = webRootPath + "\\" + fileName;
                FileStream objfilestream = new FileStream(docPath, FileMode.Create, FileAccess.ReadWrite);
                objfilestream.Write(encodedData, 0, encodedData.Length);
                objfilestream.Flush();
                objfilestream.Close();
                objfilestream.Dispose();

                dr["isValid"] = true;
                dr["Message"] = "";
            }
            catch (Exception e)
            {
                dr["isValid"] = false;
                dr["Message"] = e.Message;
            }
            dsResult.Tables.Add(dtStatus);
            jsonOutString = JsonConvert.SerializeObject(dsResult);
            return jsonOutString;
        }
    }
}
