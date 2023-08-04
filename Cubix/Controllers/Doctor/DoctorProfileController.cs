﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces.Doctor;
using Cubix.Filters;
using Cubix.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.Doctor
{
    /// <summary>
    /// This class used for Get and update the Doctor Profile
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(DoctorTokenFilter))]
    public class DoctorProfileController : BaseController
    {
        private readonly IDoctorProfile _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;// use to get the web root path
        private readonly IDistributedCache _distributedCache;
        /// <summary>
        /// Doctor Profile Controller Comstructor
        /// </summary>
        public DoctorProfileController(IDoctorProfile user, IWebHostEnvironment hostingEnvironment, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// This method used for Get the Profile
        /// </summary>
        /// <returns>Doctor Model</returns>
        [HttpGet("getprofile")]
        public async Task<ResultModel<object>> GetProfile()
        {
            return await _srv.GetDoctor(Me);
        }
        /// <summary>
        /// This method used for Update the Profile
        /// </summary>
        /// <returns>ResultModel with int as Model</returns>
        /// 
        [HttpPost("updateprofile")]
        public async Task<ResultModel<object>> UpdateProfile(DoctorReg oDoctorReg)
        {
            return await _srv.UpdateDoctor(oDoctorReg, Me);
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
            oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "Doctor/Profile", _hostingEnvironment);
            if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                return oResultModel;

            oFileUpload.ImagePath = oResultModel.Message;// Pass the profile image path for save in the database

            return await _srv.UpdateProfileImage(Me, oFileUpload);
        }


        /// <summary>
        /// This Method used to Get Doctor Slots DayWise
        /// Pass DoctorSlotRequest as Parameter
        /// </summary>
        [HttpPost("GetSlots")]
        public async Task<ResultModel<object>> GetSlots([FromBody]DoctorSlotRequest oDoctorSlotRequest)
        {
            return await _srv.GetSlots(Me, oDoctorSlotRequest);
        }

        /// <summary>
        /// This Method used to Insert Doctor Slots DayWise
        /// Pass DoctorSlotRequest as Parameter
        /// </summary>
        [HttpPost("InsertSlots")]
        public async Task<ResultModel<object>> InsertSlots([FromBody]List<DoctorSlotTime> oDoctorSlotTime)
        {
            return await _srv.InsertDoctorSlotTime(Me, oDoctorSlotTime);
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

        /// <summary>
        /// This method used for Get the Dashboard Counters
        /// </summary>
        /// <returns>Doctor Model</returns>
        [HttpGet("getdashboardcounters")]
        public async Task<ResultModel<object>> GetDashBoardCounters()
        {
            return await _srv.GetDashBoardCounters(Me);
        }


    }
}
