using Cubix.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Cubix.Utility
{
    public static class Helper
    {

        public static int GetSkipCount(int Page, int PageSize)
        {
            return ((Page - 1) * PageSize);
        }

        //Create Patient CRNumber using luhn mod algorithm
        public static string GenerateCRNumber(PatientReg oPatientReg)
        {
            string cscode = oPatientReg.StateId.ToString("D2");
            string cccode = oPatientReg.CityId.ToString("D5");
            string cdate = DateTime.Now.ToString("yy");
            Random random = new Random();
            string crandom = random.Next(0, 999999).ToString("D6").ToString();
            string CRNumber = "";
            CRNumber = cscode + cccode + cdate + crandom;
            CRNumber = CRNumber + CheckSum(CRNumber).ToString();
            return CRNumber;
        }

        public static string GenerateTeleConsultationNumber(long PatientId, int StateId, int DistrictId, int CityId)
        {
            string tscode = StateId.ToString("D2");
            string tdcode = DistrictId.ToString("D5");
            string tccode = CityId.ToString("D5");
            string tdate = DateTime.Now.ToString("yy");
            string tpat = PatientId.ToString("D6");
            return tscode + tdcode + tccode + tdate + tpat;
        }
        public static string GenerateMRTeleConsultationNumber(long PatientId)
        {

            string tdate = DateTime.Now.ToString("yy");
            string tpat = PatientId.ToString("D6");
            return tdate + tpat;
        }

        public static int CheckSum(string number)
        {

            // this will be a running total
            int sum = 0;

            // loop through digits from right to left
            for (int i = number.Length - 1; i >= 0; i--)
            {

                //set ch to "current" character to be processed
                int digit = Int32.Parse(number[i].ToString());


                // weight will be the current digit's contribution to
                // the running total
                int weight;
                if (i % 2 == 0)
                {

                    // for alternating digits starting with the rightmost, we
                    // use our formula this is the same as multiplying x 2 and
                    // adding digits together for values 0 to 9.  Using the
                    // following formula allows us to gracefully calculate a
                    // weight for non-numeric "digits" as well (from their
                    // ASCII value - 48).
                    weight = (2 * digit);
                    if (weight > 9)
                        weight = weight - 9;
                }
                else
                {
                    // even-positioned digits just contribute their ascii
                    // value minus 48
                    weight = digit;
                }

                // keep a running total of weights
                sum += weight;

            }

            // avoid sum less than 10 (if characters below "0" allowed,
            // this could happen)
            //sum = Math.Abs(sum) + 10;

            // check digit is amount needed to reach next number
            // divisible by ten
            if ((sum % 10) == 0)
            {
                sum = 0;
            }
            else
            {
                sum = 10 - (sum % 10);
            }
            return sum;

        }

        public static bool IsAllowedMimeType(string base64string)
        {
            if (string.IsNullOrWhiteSpace(base64string))
            {
                return false;
            }
            string data = base64string.Substring(0, 5);
            switch (data.ToUpper())
            {
                case "IVBOR":
                    //png
                    return true;

                case "/9J/4":
                    //jpg and jpeg
                    return true;
                case "JVBER":
                    //pdf
                    return true;
                case "R0lGO":
                    //gif
                    return true;
                case "/9j/4":
                    //gif
                    return true;
                case "UESDB":
                    //xlsx
                    return true;


                //case "AAAAA":
                //    //dicom
                //    return true;
                //case "UKLGR":
                //    //wav
                //    return true;
                default:
                    //other types
                    return false;
            }
        }

        public static bool CheckFileName(string FileName)
        {
            if (string.IsNullOrWhiteSpace(FileName))
            {
                return false;
            }
            if (FileName.Split(".").Length > 2)
            {
                return false;
            }

            switch (Path.GetExtension(FileName).ToLower())
            {
                case ".png":
                    return true;
                case ".jpeg":
                    return true;
                case ".jpg":
                    return true;
                case ".gif":
                    return true;
                case ".pdf":
                    return true;
                case ".jfif":
                    return true;
                case ".xlsx":
                    return true;

                //case ".dcm":
                //    return true;
                //case ".wav":
                //    return true;
                default:
                    //other types
                    return false;
            }
        }

        public static object CheckValidFileUplaod(object imagePath, object fileName, object fileFlag)
        {
            throw new NotImplementedException();
        }

        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }

        public static ResultModel<object> CheckValidFileUplaod(string base64String, string FileName, string FileType)
        {
            ResultModel<object> oErrorModel = new ResultModel<object>();
            if (!CheckFileName(FileName))
            {
                oErrorModel.Status = Constants.INVALIDFILE;
                oErrorModel.Message = Constants.INVALIDFILE_MESSAGE;
            }
            else if (!IsAllowedMimeType(base64String))
            {
                oErrorModel.Status = Constants.INVALIDIMAGE;
                oErrorModel.Message = Constants.INVALIDIMAGE_MESSAGE;
            }
            if (FileType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && FileType != "image/jpeg" && FileType != "image/png" && FileType != "image/gif" && FileType != "application/pdf" && FileType != "image/jpg")
            {
                oErrorModel.Status = Constants.INVALIDFILETYPE;
                oErrorModel.Message = Constants.INVALIDFILETYPE_MESSAGE;
            }

            return oErrorModel;
        }

        public static bool IsReCaptchValid(string CapchaValue)
        {
            var result = false;
            var captchaResponse = CapchaValue;
            var secretKey = "6Lf-o7sUAAAAACW2bASrZsowcCDXHYfoEOvUA4Mo";
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
        public static string ConvertDecimalToHours(decimal hours)
        {
            TimeSpan tt = TimeSpan.FromMinutes(Decimal.ToDouble(hours));
            return Math.Floor(tt.TotalHours).ToString("00") + "hrs " + tt.Minutes.ToString("00") + "mins";
        }
        public static string ConvertDecimalToMinutes(decimal hours)
        {
            TimeSpan tt = TimeSpan.FromMinutes(Decimal.ToDouble(hours));
            return Math.Floor(tt.TotalMinutes).ToString("00") + "mins " + tt.Seconds.ToString("00") + "secs";
        }


        public static string ConvertDecimalToHoursWithColon(decimal hours)
        {
            TimeSpan tt = TimeSpan.FromMinutes(Decimal.ToDouble(hours));
            return Math.Floor(tt.TotalHours).ToString("00") + ":" + tt.Minutes.ToString("00");
        }
        public static string ConvertDecimalToMinutesWithColon(decimal hours)
        {
            TimeSpan tt = TimeSpan.FromMinutes(Decimal.ToDouble(hours));
            return Math.Floor(tt.TotalMinutes).ToString("00") + ":" + tt.Seconds.ToString("00");
        }


        public static string GenerateOTP(int length = 6)
        {
            Random generator = new Random();

            return generator.Next((int)Math.Pow(10, (length - 1)), (int)Math.Pow(10, length) - 1).ToString();
        }

    }
}
