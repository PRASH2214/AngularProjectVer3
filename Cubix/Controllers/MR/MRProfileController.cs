using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.BAL.Interfaces.MR;
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.MR
{
    /// <summary>
    /// This class used for Get and update the MR Profile
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(MRTokenFilter))]
    public class MRProfileController : BaseController
    {
        private readonly IMRProfile _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;// use to get the web root path
        private readonly IDistributedCache _distributedCache;
        private readonly IAuth _Authsrv;
        /// <summary>
        /// MR Profile Controller Comstructor
        /// </summary>
        public MRProfileController(IMRProfile user, IWebHostEnvironment hostingEnvironment, IDistributedCache distributedCache, IAuth Authsrv)
        {
            _distributedCache = distributedCache;
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
            _Authsrv = Authsrv;
        }


        /// <summary>
        /// This method used for Get the Profile
        /// </summary>
        /// <returns>MR Model</returns>
        [HttpGet("getprofile")]
        public async Task<ResultModel<object>> GetProfile()
        {
            return await _srv.GetMR(Me);
        }
        /// <summary>
        /// This method used for Update the Profile
        /// </summary>
        /// <returns>ResultModel with int as Model</returns>
        /// 
        [HttpPost("updateprofile")]
        public async Task<ResultModel<object>> UpdateProfile(MRReg oMRReg)
        {
            return await _srv.UpdateMR(oMRReg, Me);
        }

        /// <summary>
        /// This method used for Update the Image Profile
        /// </summary>
        /// <returns>ResultModel with int as Model</returns>
        /// 
        [HttpPost("updateprofileimage")]
        public async Task<ResultModel<object>> UpdateProfileImage(FileUpload oFileUpload)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();


            if (oFileUpload.ImagePath == null || oFileUpload.ImagePath == "")
            {
                oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
                oResultModel.Status = Constants.NOFILEPROVIDED;
                return oResultModel;
            }
            //check file validation and upload a file on the server
            oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "MR/Profile", _hostingEnvironment);
            if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                return oResultModel;

            oFileUpload.ImagePath = oResultModel.Message;// Pass the profile image path for save in the database

            return await _srv.UpdateProfileImage(Me, oFileUpload);
        }


        /// <summary>
        /// This Method Get the Doctor Slots 
        /// Pass DoctorSlotRequest(Slot Date and Doctor Id) as Parameter
        /// </summary>
        [HttpPost("GetMRSlots")]
        public async Task<ResultModel<object>> GetMRSlots([FromBody] DoctorSlotRequest oDoctorSlotRequest)
        {
            return await _Authsrv.GetDoctorSlots(oDoctorSlotRequest, Constants.MR_USER);
        }
        /// <summary>
        /// This Method Get the Doctor Slots 
        /// Pass DoctorSlotRequest(Slot Date and Doctor Id) as Parameter
        /// </summary>
        [HttpPost("InsertMRTeleConsultationReg")]
        public async Task<ResultModel<ResultMRReg>> InsertMRTeleConsultationReg([FromBody] MRTeleConsultationReg oMRTeleConsultationReg)
        {
            oMRTeleConsultationReg.MRId = Convert.ToInt64(Me.LoginId);
            return await _srv.InsertMRTeleConsultationReg(oMRTeleConsultationReg);
        }


        /// <summary>
        /// This Method Get the Today Consultations List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("gettodayappointments")]
        public async Task<ResultModel<object>> GetTodayAppointments([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetTodayAppointments(Me, oSearchModel);
        }



        /// <summary>
        /// This Method Get the Past Consultations List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("getpastconsultations")]
        public async Task<ResultModel<object>> GetPastConsultations([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetPastConsultations(Me, oSearchModel);
        }



        /// <summary>
        /// This Method used to Logout
        /// </summary>
        [HttpGet("logout")]
        public async Task<bool> Logout()
        {
            try
            {
                await _distributedCache.RemoveAsync(Constants.TOKEN_PREFIX_PATIENT_USER + Me.LoginId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
