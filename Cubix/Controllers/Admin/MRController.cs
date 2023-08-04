using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.Admin
{

    /// <summary>
    /// This class used for Get, insert,  update, delete the MR
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class MRController : BaseController
    {

        private readonly IMR _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// MR Controller Comstructor
        /// </summary>
        public MRController(IMR user, IWebHostEnvironment hostingEnvironment)
        {
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// This Method Get the MR List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the MR Record by Passing MR Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the MR record
        /// Pass MRReg as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]MRReg oMRReg)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();


            if (oMRReg.ImagePath != null && oMRReg.ImagePath != "")
            {
                //oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
                //oResultModel.Status = Constants.NOFILEPROVIDED;
                //return oResultModel;

                //check file validation and upload a file on the server
                oResultModel = FileUpload(Me.LoginId, oMRReg.ImagePath, oMRReg.FileName, oMRReg.FileFlag, "MR/MRLicence", _hostingEnvironment);
                if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                    return oResultModel;

                oMRReg.MrLicenseImage = oResultModel.Message;// Pass the image path for save in the database
            }
            return await _srv.Insert(Me, oMRReg);
        }

        /// <summary>
        /// This Method used to Update the MR record
        /// Pass MRReg as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]MRReg oMRReg)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();


            if (oMRReg.ImagePath != null && oMRReg.ImagePath != "")// If Upload image request in Update MR 
            {

                //check file validation and upload a file on the server
                oResultModel = FileUpload(Me.LoginId, oMRReg.ImagePath, oMRReg.FileName, oMRReg.FileFlag, "MR/MRLicence", _hostingEnvironment);
                if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                    return oResultModel;

                oMRReg.MrLicenseImage = oResultModel.Message;// Pass the image path for save in the database
            }
            return await _srv.Update(Me, oMRReg);
        }

        /// <summary>
        /// This Method Delete the MR Record by Passing MR Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
