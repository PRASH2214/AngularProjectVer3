using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Filters;
using Cubix.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static Cubix.Controllers.BaseController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.Admin
{
    /// <summary>
    /// This class used for Bulk Entries in 
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class BulkController : BaseController
    {


        private readonly IHospital _srvHospital;
        private readonly IMedicineMaster _srvMedicineMaster;
        private readonly IBranch _srvBranchMaster;
        private readonly IDepartment _srvDepartmentMaster;
        private readonly IDoctor _srvDoctorMaster;
        private readonly IMR _srvMRMaster;
        private readonly ICompany _srvCompanyMaster;
        private readonly ICommon _srvCommon;
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Company Controller Comstructor
        /// </summary>
        public BulkController(IHospital srvHospital, IMedicineMaster srvMedicineMaster, IBranch srvBranchMaster, IDepartment srvDepartmentMaster, IDoctor srvDoctorMaster, IMR srvMRMaster, ICompany srvCompanyMaster, ICommon srvCommon, IWebHostEnvironment hostingEnvironment)
        {
            _srvHospital = srvHospital;
            _hostingEnvironment = hostingEnvironment;
            _srvMedicineMaster = srvMedicineMaster;
            _srvBranchMaster = srvBranchMaster;
            _srvDepartmentMaster = srvDepartmentMaster;
            _srvDoctorMaster = srvDoctorMaster;
            _srvCompanyMaster = srvCompanyMaster;
            _srvMRMaster = srvMRMaster;
            _srvCommon = srvCommon;
        }

        /// <summary>
        /// This Method Validated the Upload bulk hospital entries 
        /// Pass List of HospitalRegUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatehospitalbulkupload")]
        public ResultModel<HospitalRegUpload> ValidateHospitalBulkUpload([FromBody]List<HospitalRegUpload> oFileUpload)
        {

            ResultModel<HospitalRegUpload> oResultModel = new ResultModel<HospitalRegUpload>();
            List<HospitalRegUpload> lstHospitalRegUpload = new List<HospitalRegUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.HospitalName, AllowedChars))
                    {
                        item.Reason = " Hospital Name invalid enrty. | ";
                    }
                    else if (item.HospitalName.Length > 250)
                    {
                        item.Reason = item.Reason + " Hospital Name cannot be greater than 250. | ";
                    }
                    item.StateId = _srvCommon.CheckStateName(item.StateName).Result;
                    if (item.StateId == 0)
                    {
                        item.Reason = item.Reason + " State Name is invalid. | ";
                    }

                    item.DistrictId = _srvCommon.CheckDistrictName(item.DistrictName).Result;
                    if (item.DistrictId == 0)
                    {
                        item.Reason = item.Reason + " District Name is invalid. |";
                    }

                    item.CityId = _srvCommon.CheckCityName(item.CityName).Result;
                    if (item.CityId == 0)
                    {
                        item.Reason = item.Reason + " City Name is invalid. | ";
                    }

                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.HospitalAddress, AllowedChars))
                    {
                        item.Reason = item.Reason + " Hospital Address invalid enrty. | ";
                    }
                    else if (item.HospitalAddress.Length > 250)
                    {
                        item.Reason = item.Reason + " Hospital Address cannot be greater than 250. | ";
                    }

                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.HospitalLicenseNumber, AllowedChars))
                    {
                        item.Reason = item.Reason + " License Number invalid enrty. | ";
                    }
                    else if (item.HospitalLicenseNumber.Length > 70)
                    {
                        item.Reason = item.Reason + " License Number cannot be greater than 70. | ";
                    }


                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.OwnerName, AllowedChars))
                    {
                        item.Reason = item.Reason + " Owner Name invalid enrty. | ";
                    }
                    else if (item.OwnerName.Length > 99)
                    {
                        item.Reason = item.Reason + " Owner Name cannot be greater than 99. | ";
                    }

                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.OwnerMobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Owner Mobile invalid enrty. | ";
                    }
                    else if (item.OwnerMobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Owner Mobile must be equal to 10 digits. | ";
                    }

                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.ContactName, AllowedChars))
                    {
                        item.Reason = item.Reason + " Contact Name invalid enrty. | ";
                    }
                    else if (item.ContactName.Length > 99)
                    {
                        item.Reason = item.Reason + " Contact Name cannot be greater than 99. | ";
                    }

                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.ContactMobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Contact Mobile invalid enrty. | ";
                    }
                    else if (item.ContactMobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Contact Mobile must be equal to 10 digits. | ";
                    }

                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.PinCode, AllowedChars))
                    {
                        item.Reason = item.Reason + " PinCode invalid enrty. | ";
                    }
                    else if (item.PinCode.Length != 6)
                    {
                        item.Reason = item.Reason + " PinCode must be equal to 6 digits. | ";
                    }


                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.HospitalLink, AllowedChars))
                    {
                        item.Reason = item.Reason + " Hospital Link invalid enrty. | ";
                    }
                    else if (item.HospitalLink.Length > 500)
                    {
                        item.Reason = item.Reason + " Hospital Link cannot be greater than 500. | ";
                    }

                    item.Reason = item.Reason + _srvCommon.CheckHospitalValidations(item.HospitalLicenseNumber, item.OwnerMobile, item.ContactMobile, item.HospitalName).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstHospitalRegUpload.Add(item);
                }

                oResultModel.LstModel = lstHospitalRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk hospital entries 
        /// Pass List of HospitalReg as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savehospitalbulkupload")]
        public ResultModel<object> SaveHospitalBulkUpload([FromBody]List<HospitalReg> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstHospitalRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvHospital.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstHospitalRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstHospitalRegUpload;
            }
            catch (Exception ex)
            {


            }

            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }



        /// <summary>
        /// This Method Validated the Upload bulk Branch entries 
        /// Pass List of BranchRegUploadzBranchRegUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatebranchbulkupload")]
        public ResultModel<BranchRegUpload> ValidateBranchBulkUpload([FromBody]List<BranchRegUpload> oFileUpload)
        {

            ResultModel<BranchRegUpload> oResultModel = new ResultModel<BranchRegUpload>();
            List<BranchRegUpload> lstHospitalRegUpload = new List<BranchRegUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    item.HospitalId = _srvCommon.CheckHospitalName(item.HospitalName, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.HospitalId == 0)
                    {
                        item.Reason = item.Reason + " Hospital Name is invalid. | ";
                    }

                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.BranchName, AllowedChars))
                    {
                        item.Reason = " Branch Name invalid enrty. | ";
                    }
                    else if (item.BranchName.Length > 250)
                    {
                        item.Reason = item.Reason + " Branch Name cannot be greater than 250. | ";
                    }

                    item.StateId = _srvCommon.CheckStateName(item.StateName).Result;
                    if (item.StateId == 0)
                    {
                        item.Reason = item.Reason + " State Name is invalid. | ";
                    }

                    item.DistrictId = _srvCommon.CheckDistrictName(item.DistrictName).Result;
                    if (item.DistrictId == 0)
                    {
                        item.Reason = item.Reason + " District Name is invalid. |";
                    }

                    item.CityId = _srvCommon.CheckCityName(item.CityName).Result;
                    if (item.CityId == 0)
                    {
                        item.Reason = item.Reason + " City Name is invalid. | ";
                    }

                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.BranchHospitalAddress, AllowedChars))
                    {
                        item.Reason = item.Reason + " Branch Address invalid enrty. | ";
                    }
                    else if (item.BranchHospitalAddress.Length > 250)
                    {
                        item.Reason = item.Reason + " Branch Address cannot be greater than 250. | ";
                    }

                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.BranchHospitalLicenseNumber, AllowedChars))
                    {
                        item.Reason = item.Reason + " License Number invalid enrty. | ";
                    }
                    else if (item.BranchHospitalLicenseNumber.Length > 70)
                    {
                        item.Reason = item.Reason + " License Number cannot be greater than 70. | ";
                    }


                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.ContactName, AllowedChars))
                    {
                        item.Reason = item.Reason + " Contact Name invalid enrty. | ";
                    }
                    else if (item.ContactName.Length > 99)
                    {
                        item.Reason = item.Reason + " Contact Name cannot be greater than 99. | ";
                    }

                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.ContactMobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Contact Mobile invalid enrty. | ";
                    }
                    else if (item.ContactMobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Contact Mobile must be equal to 10 digits. | ";
                    }




                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.PinCode, AllowedChars))
                    {
                        item.Reason = item.Reason + " PinCode invalid enrty. | ";
                    }
                    else if (item.PinCode.Length != 6)
                    {
                        item.Reason = item.Reason + " PinCode must be equal to 6 digits. | ";
                    }


                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.BranchHospitalLink, AllowedChars))
                    {
                        item.Reason = item.Reason + " Hospital Link invalid enrty. | ";
                    }
                    else if (item.BranchHospitalLink.Length > 500)
                    {
                        item.Reason = item.Reason + " Hospital Link cannot be greater than 500. | ";
                    }

                    item.Reason = item.Reason + _srvCommon.CheckBranchValidations(item.BranchHospitalLicenseNumber, item.ContactMobile, item.BranchName).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstHospitalRegUpload.Add(item);
                }

                oResultModel.LstModel = lstHospitalRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk Branch entries 
        /// Pass List of BranchReg as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savebranchbulkupload")]
        public ResultModel<object> SaveBranchBulkUpload([FromBody]List<BranchReg> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstHospitalRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvBranchMaster.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstHospitalRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstHospitalRegUpload;
            }
            catch (Exception ex)
            {


            }
            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Validated the Upload bulk Department entries 
        /// Pass List of DepartmentRegUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatedepartmentbulkupload")]
        public ResultModel<DepartmentRegUpload> ValidateDepartmentBulkUpload([FromBody]List<DepartmentRegUpload> oFileUpload)
        {

            ResultModel<DepartmentRegUpload> oResultModel = new ResultModel<DepartmentRegUpload>();
            List<DepartmentRegUpload> lstHospitalRegUpload = new List<DepartmentRegUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    item.HospitalId = _srvCommon.CheckHospitalName(item.HospitalName, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.HospitalId == 0)
                    {
                        item.Reason = item.Reason + " Hospital Name is invalid. | ";
                    }

                    item.BranchId = _srvCommon.CheckBranchName(item.BranchName, item.HospitalId, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.BranchId == 0)
                    {
                        item.Reason = item.Reason + " Branch Name is invalid. | ";
                    }


                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.DepartmentName, AllowedChars))
                    {
                        item.Reason = " Department Name invalid enrty. | ";
                    }
                    else if (item.DepartmentName.Length > 250)
                    {
                        item.Reason = item.Reason + " Department Name cannot be greater than 250. | ";
                    }



                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.DepartmentContactMobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Department Mobile invalid enrty. | ";
                    }
                    else if (item.DepartmentContactMobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Department Mobile must be equal to 10 digits. | ";
                    }


                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.HospitalDepartmentLink, AllowedChars))
                    {
                        item.Reason = item.Reason + " Hospital Link invalid enrty. | ";
                    }
                    else if (item.HospitalDepartmentLink.Length > 500)
                    {
                        item.Reason = item.Reason + " Hospital Link cannot be greater than 500. | ";
                    }

                    item.Reason = item.Reason + _srvCommon.CheckDepartmentValidations(item.DepartmentContactMobile, item.DepartmentName).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstHospitalRegUpload.Add(item);
                }

                oResultModel.LstModel = lstHospitalRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk Department entries 
        /// Pass List of DepartmentReg as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savedepartmentbulkupload")]
        public ResultModel<object> SaveDepartmentBulkUpload([FromBody]List<DepartmentReg> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstDepartmentRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvDepartmentMaster.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstDepartmentRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstDepartmentRegUpload;
            }
            catch (Exception ex)
            {


            }
            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Validated the Upload bulk Doctor entries 
        /// Pass List of DoctorRegUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatedoctorbulkupload")]
        public ResultModel<DoctorRegUpload> ValidateDoctorBulkUpload([FromBody]List<DoctorRegUpload> oFileUpload)
        {


            ResultModel<DoctorRegUpload> oResultModel = new ResultModel<DoctorRegUpload>();
            List<DoctorRegUpload> lstDoctorRegUpload = new List<DoctorRegUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    if (item.strGender.ToLower() == "male")
                    {
                        item.GenderId = 1;
                    }
                    else if (item.strGender.ToLower() == "female")
                    {
                        item.GenderId = 2;
                    }
                    else if (item.strGender.ToLower() == "transgender")
                    {
                        item.GenderId = 3;
                    }
                    else
                    {
                        item.Reason = item.Reason + " Gender not provided. | ";
                    }

                    item.HospitalId = _srvCommon.CheckHospitalName(item.HospitalName, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.HospitalId == 0)
                    {
                        item.Reason = item.Reason + " Hospital Name is invalid. | ";
                    }

                    item.BranchId = _srvCommon.CheckBranchName(item.BranchName, item.HospitalId, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.BranchId == 0)
                    {
                        item.Reason = item.Reason + " Branch Name is invalid. | ";
                    }



                    item.DepartmentId = _srvCommon.CheckDepartmentName(item.DepartmentName, item.BranchId, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.DepartmentId == 0)
                    {
                        item.Reason = item.Reason + " Department Name is invalid. | ";
                    }

                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.FirstName, AllowedChars))
                    {
                        item.Reason = " First Name invalid enrty. | ";
                    }
                    else if (item.FirstName.Length > 45)
                    {
                        item.Reason = item.Reason + " First Name cannot be greater than 45. | ";
                    }

                    if (!Regex.IsMatch(item.MiddleName, AllowedChars))
                    {
                        item.Reason = " Middle Name invalid enrty. | ";
                    }
                    else if (item.MiddleName.Length > 45)
                    {
                        item.Reason = item.Reason + " Middle Name cannot be greater than 45. | ";
                    }

                    if (!Regex.IsMatch(item.LastName, AllowedChars))
                    {
                        item.Reason = " Last Name invalid enrty. | ";
                    }
                    else if (item.LastName.Length > 45)
                    {
                        item.Reason = item.Reason + " Last Name cannot be greater than 45. | ";
                    }

                    item.StateId = _srvCommon.CheckStateName(item.StateName).Result;
                    if (item.StateId == 0)
                    {
                        item.Reason = item.Reason + " State Name is invalid. | ";
                    }

                    item.DistrictId = _srvCommon.CheckDistrictName(item.DistrictName).Result;
                    if (item.DistrictId == 0)
                    {
                        item.Reason = item.Reason + " District Name is invalid. |";
                    }

                    item.CityId = _srvCommon.CheckCityName(item.CityName).Result;
                    if (item.CityId == 0)
                    {
                        item.Reason = item.Reason + " City Name is invalid. | ";
                    }

                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.DoctorAddress, AllowedChars))
                    {
                        item.Reason = item.Reason + " Doctor Address invalid enrty. | ";
                    }
                    else if (item.DoctorAddress.Length > 250)
                    {
                        item.Reason = item.Reason + " Doctor Address cannot be greater than 250. | ";
                    }

                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.DoctorLicenseNumber, AllowedChars))
                    {
                        item.Reason = item.Reason + " License Number invalid enrty. | ";
                    }
                    else if (item.DoctorLicenseNumber.Length > 70)
                    {
                        item.Reason = item.Reason + " License Number cannot be greater than 70. | ";
                    }



                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.Mobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Mobile invalid enrty. | ";
                    }
                    else if (item.Mobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Mobile must be equal to 10 digits. | ";
                    }
                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.PinCode, AllowedChars))
                    {
                        item.Reason = item.Reason + " PinCode invalid enrty. | ";
                    }
                    else if (item.PinCode.Length != 6)
                    {
                        item.Reason = item.Reason + " PinCode must be equal to 6 digits. | ";
                    }

                    item.SpecialityId = _srvCommon.CheckSpecialityName(item.SpecialityName, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.SpecialityId == 0)
                    {
                        item.Reason = item.Reason + " Speciality Name is invalid. | ";
                    }

                    try
                    {
                        item.DOB = DateTime.ParseExact(item.strDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        item.Reason = item.Reason + " DOB is invalid. | ";
                    }




                    item.Reason = item.Reason + _srvCommon.CheckDoctorValidations(item.DoctorLicenseNumber, item.Mobile).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstDoctorRegUpload.Add(item);
                }

                oResultModel.LstModel = lstDoctorRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk Doctor entries 
        /// Pass List of DoctorReg as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savedoctorbulkupload")]
        public ResultModel<object> SaveDoctorBulkUpload([FromBody]List<DoctorReg> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstHospitalRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvDoctorMaster.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstHospitalRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstHospitalRegUpload;
            }
            catch (Exception ex)
            {


            }
            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }




        /// <summary>
        /// This Method Validated the Upload bulk Doctor entries 
        /// Pass List of DoctorRegUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatemrbulkupload")]
        public ResultModel<MRRegUpload> ValidateMRBulkUpload([FromBody]List<MRRegUpload> oFileUpload)
        {


            ResultModel<MRRegUpload> oResultModel = new ResultModel<MRRegUpload>();
            List<MRRegUpload> lstDoctorRegUpload = new List<MRRegUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    if (item.strGender.ToLower() == "male")
                    {
                        item.GenderId = 1;
                    }
                    else if (item.strGender.ToLower() == "female")
                    {
                        item.GenderId = 2;
                    }
                    else if (item.strGender.ToLower() == "transgender")
                    {
                        item.GenderId = 3;
                    }
                    else
                    {
                        item.Reason = item.Reason + " Gender not provided. | ";
                    }


                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.FirstName, AllowedChars))
                    {
                        item.Reason = " First Name invalid enrty. | ";
                    }
                    else if (item.FirstName.Length > 45)
                    {
                        item.Reason = item.Reason + " First Name cannot be greater than 45. | ";
                    }

                    if (!Regex.IsMatch(item.MiddleName, AllowedChars))
                    {
                        item.Reason = " Middle Name invalid enrty. | ";
                    }
                    else if (item.MiddleName.Length > 45)
                    {
                        item.Reason = item.Reason + " Middle Name cannot be greater than 45. | ";
                    }

                    if (!Regex.IsMatch(item.LastName, AllowedChars))
                    {
                        item.Reason = " Last Name invalid enrty. | ";
                    }
                    else if (item.LastName.Length > 45)
                    {
                        item.Reason = item.Reason + " Last Name cannot be greater than 45. | ";
                    }


                    item.CompanyId = _srvCommon.CheckCompanyName(item.CompanyName, Convert.ToInt64(Me.LoginId)).Result;
                    if (item.CompanyId == 0)
                    {
                        item.Reason = item.Reason + " Company Name is invalid. | ";
                    }

                    item.StateId = _srvCommon.CheckStateName(item.StateName).Result;
                    if (item.StateId == 0)
                    {
                        item.Reason = item.Reason + " State Name is invalid. | ";
                    }

                    item.DistrictId = _srvCommon.CheckDistrictName(item.DistrictName).Result;
                    if (item.DistrictId == 0)
                    {
                        item.Reason = item.Reason + " District Name is invalid. |";
                    }

                    item.CityId = _srvCommon.CheckCityName(item.CityName).Result;
                    if (item.CityId == 0)
                    {
                        item.Reason = item.Reason + " City Name is invalid. | ";
                    }

                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.MrAddress, AllowedChars))
                    {
                        item.Reason = item.Reason + " MR Address invalid enrty. | ";
                    }
                    else if (item.MrAddress.Length > 250)
                    {
                        item.Reason = item.Reason + " MR Address cannot be greater than 250. | ";
                    }

                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.MrLicenseNumber, AllowedChars))
                    {
                        item.Reason = item.Reason + " License Number invalid enrty. | ";
                    }
                    else if (item.MrLicenseNumber.Length > 70)
                    {
                        item.Reason = item.Reason + " License Number cannot be greater than 70. | ";
                    }



                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.Mobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Mobile invalid enrty. | ";
                    }
                    else if (item.Mobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Mobile must be equal to 10 digits. | ";
                    }
                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.PinCode, AllowedChars))
                    {
                        item.Reason = item.Reason + " PinCode invalid enrty. | ";
                    }
                    else if (item.PinCode.Length != 6)
                    {
                        item.Reason = item.Reason + " PinCode must be equal to 6 digits. | ";
                    }


                    try
                    {
                        item.DOB = DateTime.ParseExact(item.strDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        item.Reason = item.Reason + " DOB is invalid. | ";
                    }




                    item.Reason = item.Reason + _srvCommon.CheckMRValidations(item.MrLicenseNumber, item.Mobile).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstDoctorRegUpload.Add(item);
                }

                oResultModel.LstModel = lstDoctorRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk MR entries 
        /// Pass List of MRReg as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savemrbulkupload")]
        public ResultModel<object> SaveMRBulkUpload([FromBody]List<MRReg> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstHospitalRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvMRMaster.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstHospitalRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstHospitalRegUpload;
            }
            catch (Exception ex)
            {


            }
            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }



        /// <summary>
        /// This Method Validated the Upload bulk Medicine entries 
        /// Pass List of MasterMedicineUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatemedicinebulkupload")]
        public ResultModel<MasterMedicineUpload> ValidateMedicineBulkUpload([FromBody]List<MasterMedicineUpload> oFileUpload)
        {


            ResultModel<MasterMedicineUpload> oResultModel = new ResultModel<MasterMedicineUpload>();
            List<MasterMedicineUpload> lstDoctorRegUpload = new List<MasterMedicineUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    item.CompanyId = _srvCommon.CheckCompanyName(item.CompanyName, Convert.ToInt64(Me.LoginId)).Result;
                    item.DrugId = _srvCommon.CheckDrugName(item.DrugName, Convert.ToInt64(Me.LoginId)).Result;
                    item.DrugType = _srvCommon.CheckDrugType(item.DrugTypeName, Convert.ToInt64(Me.LoginId)).Result;


                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.MedicineName, AllowedChars))
                    {
                        item.Reason = " Medicine Name invalid enrty. | ";
                    }
                    else if (item.MedicineName.Length > 250)
                    {
                        item.Reason = item.Reason + " Medicine Name cannot be greater than 250. | ";
                    }

                    if (!Regex.IsMatch(item.Description, AllowedChars))
                    {
                        item.Reason = " Description invalid enrty. | ";
                    }
                    else if (item.Description.Length > 500)
                    {
                        item.Reason = item.Reason + " Description cannot be greater than 500. | ";
                    }

                    // item.Reason = item.Reason + _srvCommon.CheckDrugValidations(item.MedicineName, Convert.ToInt64(Me.LoginId)).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstDoctorRegUpload.Add(item);
                }

                oResultModel.LstModel = lstDoctorRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk Drug entries 
        /// Pass List of MasterDrug as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savemedicinebulkupload")]
        public ResultModel<object> SaveDrugBulkUpload([FromBody]List<MasterMedicine> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstHospitalRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvMedicineMaster.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstHospitalRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstHospitalRegUpload;
            }
            catch (Exception ex)
            {


            }
            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Validated the Upload bulk Company entries 
        /// Pass List of CompanyRegUpload as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("validatecompanybulkupload")]
        public ResultModel<CompanyRegUpload> ValidateCompanyBulkUpload([FromBody]List<CompanyRegUpload> oFileUpload)
        {

            ResultModel<CompanyRegUpload> oResultModel = new ResultModel<CompanyRegUpload>();
            List<CompanyRegUpload> lstCompanyRegUpload = new List<CompanyRegUpload>();
            try
            {
                foreach (var item in oFileUpload)
                {
                    item.Reason = "";

                    String AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.CompanyName, AllowedChars))
                    {
                        item.Reason = " Company Name invalid enrty. | ";
                    }
                    else if (item.CompanyName.Length > 250)
                    {
                        item.Reason = item.Reason + " Company Name cannot be greater than 250. | ";
                    }
                    item.StateId = _srvCommon.CheckStateName(item.StateName).Result;
                    if (item.StateId == 0)
                    {
                        item.Reason = item.Reason + " State Name is invalid. | ";
                    }

                    item.DistrictId = _srvCommon.CheckDistrictName(item.DistrictName).Result;
                    if (item.DistrictId == 0)
                    {
                        item.Reason = item.Reason + " District Name is invalid. |";
                    }

                    item.CityId = _srvCommon.CheckCityName(item.CityName).Result;
                    if (item.CityId == 0)
                    {
                        item.Reason = item.Reason + " City Name is invalid. | ";
                    }

                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.CompanyAddress, AllowedChars))
                    {
                        item.Reason = item.Reason + " CompanyzCompany Address invalid enrty. | ";
                    }
                    else if (item.CompanyAddress.Length > 250)
                    {
                        item.Reason = item.Reason + " Company Address cannot be greater than 250. | ";
                    }

                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.CompanyLicenseNumber, AllowedChars))
                    {
                        item.Reason = item.Reason + " License Number invalid enrty. | ";
                    }
                    else if (item.CompanyLicenseNumber.Length > 70)
                    {
                        item.Reason = item.Reason + " License Number cannot be greater than 70. | ";
                    }


                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.SpocName, AllowedChars))
                    {
                        item.Reason = item.Reason + " Spoc Name invalid enrty. | ";
                    }
                    else if (item.SpocName.Length > 99)
                    {
                        item.Reason = item.Reason + " Spoc Name cannot be greater than 99. | ";
                    }

                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.SpocMobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Spoc Mobile invalid enrty. | ";
                    }
                    else if (item.SpocMobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Spoc Mobile must be equal to 10 digits. | ";
                    }


                    AllowedChars = @"^[^<>.,?;:'()!~%\-_@#/*""]+$";
                    if (!Regex.IsMatch(item.AdminName, AllowedChars))
                    {
                        item.Reason = item.Reason + " Admin Name invalid enrty. | ";
                    }
                    else if (item.AdminName.Length > 99)
                    {
                        item.Reason = item.Reason + " Admin Name cannot be greater than 99. | ";
                    }

                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.AdminMobile, AllowedChars))
                    {
                        item.Reason = item.Reason + " Admin Mobile invalid enrty. | ";
                    }
                    else if (item.AdminMobile.Length != 10)
                    {
                        item.Reason = item.Reason + " Admin Mobile must be equal to 10 digits. | ";
                    }


                    AllowedChars = "^[0-9]*$";
                    if (!Regex.IsMatch(item.PinCode, AllowedChars))
                    {
                        item.Reason = item.Reason + " PinCode invalid enrty. | ";
                    }
                    else if (item.PinCode.Length != 6)
                    {
                        item.Reason = item.Reason + " PinCode must be equal to 6 digits. | ";
                    }


                    AllowedChars = @"[^<>]*";
                    if (!Regex.IsMatch(item.CompanyWebLink, AllowedChars))
                    {
                        item.Reason = item.Reason + " Company Link invalid enrty. | ";
                    }
                    else if (item.CompanyWebLink.Length > 500)
                    {
                        item.Reason = item.Reason + " Company Link cannot be greater than 500. | ";
                    }

                    item.Reason = item.Reason + _srvCommon.CheckCompanyValidations(item.CompanyLicenseNumber, item.AdminMobile, item.SpocMobile, item.CompanyName).Result;
                    item.Status = 1;
                    item.IsValid = true;
                    if (item.Reason != "")
                    {
                        item.IsValid = false;
                    }
                    lstCompanyRegUpload.Add(item);
                }

                oResultModel.LstModel = lstCompanyRegUpload;

                if (oResultModel.LstModel.Where(s => s.IsValid == false).Count() > 0)
                {
                    oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                    oResultModel.Status = Constants.INVALID_EXCEL;

                }
            }
            catch (Exception ex)
            {


            }
            return oResultModel;
        }


        /// <summary>
        /// This Method Save the Upload bulk hospital entries 
        /// Pass List of HospitalReg as Parameter
        /// </summary>
        /// 
        [HttpPost]
        [Route("savecompanybulkupload")]
        public ResultModel<object> SaveCompanyBulkUpload([FromBody]List<CompanyReg> oFileUpload)
        {

            ResultModel<object> oResultModel = new ResultModel<object>();
            try
            {
                int count = 0;
                List<object> lstHospitalRegUpload = new List<object>();
                foreach (var item in oFileUpload)
                {
                    oResultModel = _srvCompanyMaster.Insert(Me, item).Result;
                    if (oResultModel.Status > Constants.SUCCESS)
                    {
                        int c = count - 1;
                        oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
                        item.Reason = oResultModel.Message;
                        lstHospitalRegUpload.Add(item);
                    }
                }
                oResultModel.LstModel = lstHospitalRegUpload;
            }
            catch (Exception ex)
            {


            }

            if (oResultModel.LstModel.Count() > 0)
            {
                oResultModel.Message = Constants.INVALID_EXCCEL_MESSAGE;
                oResultModel.Status = Constants.INVALID_EXCEL;

            }
            return oResultModel;
        }




        ///// <summary>
        ///// This Method Upload the bulk hospital entries 
        ///// Pass SearchModel as Parameter
        ///// </summary>
        ///// 
        //[HttpPost]
        //[Route("hospitalbulkupload")]
        //public ResultModel<object> HospitalBulkUpload([FromBody] FileUpload oFileUpload)
        //{

        //    ResultModel<object> oResultModel = new ResultModel<object>();
        //    try
        //    {

        //        if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
        //        {
        //            oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
        //            oResultModel.Status = Constants.NOFILEPROVIDED;
        //            return oResultModel;
        //        }
        //        //check file validation and upload a file on the server
        //        oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Bulk/Hospital", _hostingEnvironment);
        //        if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
        //            return oResultModel;

        //        List<FileUpload> users = new List<FileUpload>();

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = System.IO.File.Open("wwwroot/" + oResultModel.Message, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                int count = 0;
        //                while (reader.Read()) //Each row of the file
        //                {
        //                    if (count > 0)
        //                    {
        //                        HospitalReg oHospitalReg = new HospitalReg();

        //                        oHospitalReg.Status = 1;

        //                        oHospitalReg.HospitalName = reader.GetValue(0) == null ? "" : reader.GetValue(0).ToString();
        //                        oHospitalReg.StateId = reader.GetValue(1) == null ? 0 : Convert.ToInt32(reader.GetValue(1));
        //                        oHospitalReg.DistrictId = reader.GetValue(2) == null ? 0 : Convert.ToInt32(reader.GetValue(2));
        //                        oHospitalReg.CityId = reader.GetValue(3) == null ? 0 : Convert.ToInt32(reader.GetValue(3));
        //                        oHospitalReg.HospitalAddress = reader.GetValue(4) == null ? "" : reader.GetValue(4).ToString();
        //                        oHospitalReg.HospitalLicenseNumber = reader.GetValue(5) == null ? "" : reader.GetValue(5).ToString();
        //                        oHospitalReg.OwnerName = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString();
        //                        oHospitalReg.OwnerMobile = reader.GetValue(7) == null ? "" : reader.GetValue(7).ToString();
        //                        oHospitalReg.ContactName = reader.GetValue(8) == null ? "" : reader.GetValue(8).ToString();
        //                        oHospitalReg.ContactMobile = reader.GetValue(9) == null ? "" : reader.GetValue(9).ToString();
        //                        oHospitalReg.PinCode = reader.GetValue(10) == null ? "" : reader.GetValue(10).ToString();
        //                        oHospitalReg.HospitalLink = reader.GetValue(11) == null ? "" : reader.GetValue(11).ToString();
        //                        oHospitalReg.Status = reader.GetValue(12) == null ? 2 : Convert.ToInt32(reader.GetValue(12));

        //                        oResultModel = _srvHospital.Insert(Me, oHospitalReg).Result;
        //                        if (oResultModel.Status > Constants.SUCCESS)
        //                        {
        //                            int c = count - 1;
        //                            oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
        //                            break;

        //                        }
        //                    }

        //                    count++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return oResultModel;
        //}




        ///// <summary>
        ///// This Method Upload the bulk Branch entries 
        ///// Pass SearchModel as Parameter
        ///// </summary>
        ///// 
        //[HttpPost]
        //[Route("branchbulkupload")]
        //public ResultModel<object> BranchBulkUpload([FromBody] BranchFileUpload oFileUpload)
        //{

        //    ResultModel<object> oResultModel = new ResultModel<object>();
        //    try
        //    {

        //        if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
        //        {
        //            oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
        //            oResultModel.Status = Constants.NOFILEPROVIDED;
        //            return oResultModel;
        //        }
        //        //check file validation and upload a file on the server
        //        oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Bulk/Drug", _hostingEnvironment);
        //        if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
        //            return oResultModel;

        //        List<FileUpload> users = new List<FileUpload>();

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = System.IO.File.Open("wwwroot/" + oResultModel.Message, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                int count = 0;
        //                while (reader.Read()) //Each row of the file
        //                {
        //                    if (count > 0)
        //                    {
        //                        BranchReg oBranchReg = new BranchReg();

        //                        oBranchReg.HospitalId = oFileUpload.HospitalId;
        //                        oBranchReg.BranchName = reader.GetValue(0) == null ? "" : reader.GetValue(0).ToString();
        //                        oBranchReg.StateId = reader.GetValue(1) == null ? 0 : Convert.ToInt32(reader.GetValue(1));
        //                        oBranchReg.DistrictId = reader.GetValue(2) == null ? 0 : Convert.ToInt32(reader.GetValue(2));
        //                        oBranchReg.CityId = reader.GetValue(3) == null ? 0 : Convert.ToInt32(reader.GetValue(3));
        //                        oBranchReg.BranchHospitalAddress = reader.GetValue(4) == null ? "" : reader.GetValue(4).ToString();
        //                        oBranchReg.BranchHospitalLicenseNumber = reader.GetValue(5) == null ? "" : reader.GetValue(5).ToString();
        //                        oBranchReg.ContactName = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString();
        //                        oBranchReg.ContactMobile = reader.GetValue(7) == null ? "" : reader.GetValue(7).ToString();
        //                        oBranchReg.PinCode = reader.GetValue(8) == null ? "" : reader.GetValue(8).ToString();
        //                        oBranchReg.BranchHospitalLink = reader.GetValue(9) == null ? "" : reader.GetValue(9).ToString();
        //                        oBranchReg.Status = reader.GetValue(10) == null ? 2 : Convert.ToInt32(reader.GetValue(10));
        //                        oResultModel = _srvBranchMaster.Insert(Me, oBranchReg).Result;
        //                        if (oResultModel.Status > Constants.SUCCESS)
        //                        {
        //                            int c = count - 1;
        //                            oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
        //                            break;

        //                        }
        //                    }

        //                    count++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return oResultModel;
        //}


        ///// <summary>
        ///// This Method Upload the bulk Department entries 
        ///// Pass SearchModel as Parameter
        ///// </summary>
        ///// 
        //[HttpPost]
        //[Route("departmentbulkupload")]
        //public ResultModel<object> DepartmentBulkUpload([FromBody] DepartmentFileUpload oFileUpload)
        //{

        //    ResultModel<object> oResultModel = new ResultModel<object>();
        //    try
        //    {

        //        if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
        //        {
        //            oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
        //            oResultModel.Status = Constants.NOFILEPROVIDED;
        //            return oResultModel;
        //        }
        //        //check file validation and upload a file on the server
        //        oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Bulk/Department", _hostingEnvironment);
        //        if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
        //            return oResultModel;

        //        List<FileUpload> users = new List<FileUpload>();

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = System.IO.File.Open("wwwroot/" + oResultModel.Message, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                int count = 0;
        //                while (reader.Read()) //Each row of the file
        //                {
        //                    if (count > 0)
        //                    {
        //                        DepartmentReg oDepartmentReg = new DepartmentReg();

        //                        oDepartmentReg.HospitalId = oFileUpload.HospitalId;
        //                        oDepartmentReg.BranchId = oFileUpload.BranchId;
        //                        oDepartmentReg.DepartmentName = reader.GetValue(0) == null ? "" : reader.GetValue(0).ToString();
        //                        oDepartmentReg.DepartmentContactMobile = reader.GetValue(1) == null ? "" : reader.GetValue(1).ToString();
        //                        oDepartmentReg.HospitalDepartmentLink = reader.GetValue(2) == null ? "" : reader.GetValue(2).ToString();
        //                        oDepartmentReg.Amount = reader.GetValue(3) == null ? 0 : Convert.ToDecimal(reader.GetValue(3));
        //                        oDepartmentReg.IsRefundAllowed = Convert.ToInt32(reader.GetValue(4));
        //                        oDepartmentReg.Status = reader.GetValue(5) == null ? 2 : Convert.ToInt32(reader.GetValue(5));
        //                        oResultModel = _srvDepartmentMaster.Insert(Me, oDepartmentReg).Result;
        //                        if (oResultModel.Status > Constants.SUCCESS)
        //                        {
        //                            int c = count - 1;
        //                            oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
        //                            break;

        //                        }
        //                    }

        //                    count++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return oResultModel;
        //}



        ///// <summary>
        ///// This Method Upload the bulk Doctor entries 
        ///// Pass SearchModel as Parameter
        ///// </summary>
        ///// 
        //[HttpPost]
        //[Route("doctorbulkupload")]
        //public ResultModel<object> DoctorBulkUpload([FromBody] DoctorFileUpload oFileUpload)
        //{

        //    ResultModel<object> oResultModel = new ResultModel<object>();
        //    try
        //    {

        //        if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
        //        {
        //            oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
        //            oResultModel.Status = Constants.NOFILEPROVIDED;
        //            return oResultModel;
        //        }
        //        //check file validation and upload a file on the server
        //        oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Bulk/Doctor", _hostingEnvironment);
        //        if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
        //            return oResultModel;

        //        List<FileUpload> users = new List<FileUpload>();

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = System.IO.File.Open("wwwroot/" + oResultModel.Message, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                int count = 0;
        //                while (reader.Read()) //Each row of the file
        //                {
        //                    if (count > 0)
        //                    {
        //                        DoctorReg oDoctorReg = new DoctorReg();

        //                        oDoctorReg.HospitalId = oFileUpload.HospitalId;
        //                        oDoctorReg.BranchId = oFileUpload.BranchId;
        //                        oDoctorReg.DepartmentId = oFileUpload.DepartmentId;
        //                        oDoctorReg.FirstName = reader.GetValue(0) == null ? "" : reader.GetValue(0).ToString();
        //                        oDoctorReg.MiddleName = reader.GetValue(1) == null ? "" : reader.GetValue(1).ToString();
        //                        oDoctorReg.LastName = reader.GetValue(2) == null ? "" : reader.GetValue(2).ToString();
        //                        oDoctorReg.StateId = reader.GetValue(3) == null ? 0 : Convert.ToInt32(reader.GetValue(3));
        //                        oDoctorReg.DistrictId = reader.GetValue(4) == null ? 0 : Convert.ToInt32(reader.GetValue(4));
        //                        oDoctorReg.CityId = reader.GetValue(5) == null ? 0 : Convert.ToInt32(reader.GetValue(5));
        //                        oDoctorReg.DoctorAddress = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString();
        //                        oDoctorReg.DoctorLicenseNumber = reader.GetValue(7) == null ? "" : reader.GetValue(7).ToString();
        //                        oDoctorReg.Mobile = reader.GetValue(8) == null ? "" : reader.GetValue(8).ToString();
        //                        oDoctorReg.DOB = (DateTime)reader.GetValue(9);
        //                        //  oDoctorReg.Age = reader.GetValue(10) == null ? 0 : Convert.ToInt32(reader.GetValue(3));
        //                        oDoctorReg.PinCode = reader.GetValue(10) == null ? "" : reader.GetValue(10).ToString();
        //                        oDoctorReg.SpecialityId = reader.GetValue(11) == null ? 0 : Convert.ToInt32(reader.GetValue(11));
        //                        oDoctorReg.Status = reader.GetValue(12) == null ? 2 : Convert.ToInt32(reader.GetValue(12));
        //                        oResultModel = _srvDoctorMaster.Insert(Me, oDoctorReg).Result;
        //                        if (oResultModel.Status > Constants.SUCCESS)
        //                        {
        //                            int c = count - 1;
        //                            oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
        //                            break;

        //                        }
        //                    }

        //                    count++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return oResultModel;
        //}

        ///// <summary>
        ///// This Method Upload the bulk Drug entries 
        ///// Pass SearchModel as Parameter
        ///// </summary>
        ///// 
        //[HttpPost]
        //[Route("drugbulkupload")]
        //public ResultModel<object> DrugBulkUpload([FromBody] FileUpload oFileUpload)
        //{

        //    ResultModel<object> oResultModel = new ResultModel<object>();
        //    try
        //    {

        //        if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
        //        {
        //            oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
        //            oResultModel.Status = Constants.NOFILEPROVIDED;
        //            return oResultModel;
        //        }
        //        //check file validation and upload a file on the server
        //        oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Bulk/Drug", _hostingEnvironment);
        //        if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
        //            return oResultModel;

        //        List<FileUpload> users = new List<FileUpload>();

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = System.IO.File.Open("wwwroot/" + oResultModel.Message, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                int count = 0;
        //                while (reader.Read()) //Each row of the file
        //                {
        //                    if (count > 0)
        //                    {
        //                        MasterDrug oMasterDrug = new MasterDrug();

        //                        oMasterDrug.DrugName = reader.GetValue(0) == null ? "" : reader.GetValue(0).ToString();
        //                        oMasterDrug.Description = reader.GetValue(1) == null ? "" : reader.GetValue(1).ToString();
        //                        oMasterDrug.Status = reader.GetValue(2) == null ? 2 : Convert.ToInt32(reader.GetValue(2));

        //                        oResultModel = _srvDrugMaster.Insert(Me, oMasterDrug).Result;
        //                        if (oResultModel.Status > Constants.SUCCESS)
        //                        {
        //                            int c = count - 1;
        //                            oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
        //                            break;

        //                        }
        //                    }

        //                    count++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return oResultModel;
        //}


        ///// <summary>
        ///// This Method Upload the bulk company entries 
        ///// Pass SearchModel as Parameter
        ///// </summary>
        ///// 
        //[HttpPost]
        //[Route("companybulkupload")]
        //public ResultModel<object> CompanyBulkUpload([FromBody] FileUpload oFileUpload)
        //{

        //    ResultModel<object> oResultModel = new ResultModel<object>();
        //    try
        //    {

        //        if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
        //        {
        //            oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
        //            oResultModel.Status = Constants.NOFILEPROVIDED;
        //            return oResultModel;
        //        }
        //        //check file validation and upload a file on the server
        //        oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Bulk/Company", _hostingEnvironment);
        //        if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
        //            return oResultModel;

        //        List<FileUpload> users = new List<FileUpload>();

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = System.IO.File.Open("wwwroot/" + oResultModel.Message, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                int count = 0;
        //                while (reader.Read()) //Each row of the file
        //                {
        //                    if (count > 0)
        //                    {
        //                        CompanyReg oCompanyReg = new CompanyReg();

        //                        oCompanyReg.Status = 1;

        //                        oCompanyReg.CompanyName = reader.GetValue(0) == null ? "" : reader.GetValue(0).ToString();
        //                        oCompanyReg.StateId = reader.GetValue(1) == null ? 0 : Convert.ToInt32(reader.GetValue(1));
        //                        oCompanyReg.DistrictId = reader.GetValue(2) == null ? 0 : Convert.ToInt32(reader.GetValue(2));
        //                        oCompanyReg.CityId = reader.GetValue(3) == null ? 0 : Convert.ToInt32(reader.GetValue(3));
        //                        oCompanyReg.CompanyAddress = reader.GetValue(4) == null ? "" : reader.GetValue(4).ToString();
        //                        oCompanyReg.CompanyLicenseNumber = reader.GetValue(5) == null ? "" : reader.GetValue(5).ToString();
        //                        oCompanyReg.SpocName = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString();
        //                        oCompanyReg.SpocMobile = reader.GetValue(7) == null ? "" : reader.GetValue(7).ToString();
        //                        oCompanyReg.AdminName = reader.GetValue(8) == null ? "" : reader.GetValue(8).ToString();
        //                        oCompanyReg.AdminMobile = reader.GetValue(9) == null ? "" : reader.GetValue(9).ToString();
        //                        oCompanyReg.PinCode = reader.GetValue(10) == null ? "" : reader.GetValue(10).ToString();
        //                        oCompanyReg.CompanyWebLink = reader.GetValue(11) == null ? "" : reader.GetValue(11).ToString();
        //                        oCompanyReg.Status = reader.GetValue(12) == null ? 2 : Convert.ToInt32(reader.GetValue(12));

        //                        oResultModel = _srvCompanyMaster.Insert(Me, oCompanyReg).Result;
        //                        if (oResultModel.Status > Constants.SUCCESS)
        //                        {
        //                            int c = count - 1;
        //                            oResultModel.Message = "Only " + c + " Records processed due to error at Record " + count + " (" + oResultModel.Message + ")";
        //                            break;

        //                        }
        //                    }

        //                    count++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return oResultModel;
        //}



    }
}
