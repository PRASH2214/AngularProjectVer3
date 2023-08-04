using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Filters;
using Cubix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Cubix.Controllers.SuperAdmin
{

    /// <summary>
    /// This class used for Get and update the SuperAdmin Profile
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(SuperAdminTokenFilter))]
    public class SuperAdminController : BaseController
    {

        private readonly ISuperAdminProfile _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;// use to get the web root path
        private readonly IDistributedCache _distributedCache;
        /// <summary>
        /// SuperAdmin Controller Comstructor
        /// </summary>
        public SuperAdminController(ISuperAdminProfile user, IWebHostEnvironment hostingEnvironment, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
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
            oResultModel = FileUpload(Me.LoginId, oFileUpload.ImagePath, oFileUpload.FileName, oFileUpload.FileFlag, "SuperAdmin/Profile", _hostingEnvironment);
            if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                return oResultModel;

            oFileUpload.ImagePath = oResultModel.Message;// Pass the profile image path for save in the database

            return await _srv.UpdateProfileImage(Me, oFileUpload);
        }

        /// <summary>
        /// This Method used to Logout
        /// </summary>
        [HttpGet("logout")]
        public async Task<bool> Logout()
        {
            try
            {
                await _distributedCache.RemoveAsync(Constants.TOKEN_PREFIX_SUPERADMIN_USER + Me.LoginId);
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