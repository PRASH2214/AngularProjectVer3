using System;
using System.Collections.Generic;
using System.IO;
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
    /// This class used for Get, insert,  update, delete the Doctor
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    // [ServiceFilter(typeof(AdminTokenFilter))]
    public class DoctorController : BaseController
    {

        private readonly IDoctor _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Doctor Controller Comstructor
        /// </summary>
        public DoctorController(IDoctor user, IWebHostEnvironment hostingEnvironment)
        {
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// This Method Get the Doctor List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the Doctor Record by Passing Doctor Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the Doctor record
        /// Pass DoctorReg as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]DoctorReg oDoctorReg)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();


            if (oDoctorReg.ImagePath != null && oDoctorReg.ImagePath != "")
            {
                //oResultModel.Message = Constants.NOFILEPROVIDED_MESSAGE;
                //oResultModel.Status = Constants.NOFILEPROVIDED;
                //return oResultModel;

                //check file validation and upload a file on the server
                oResultModel = FileUpload(Me.LoginId, oDoctorReg.ImagePath, oDoctorReg.FileName, oDoctorReg.FileFlag, "Doctor/DoctorLicence", _hostingEnvironment);
                if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                    return oResultModel;

                oDoctorReg.MedicalLicenseImage = oResultModel.Message;// Pass the image path for save in the database
            }
            return await _srv.Insert(Me, oDoctorReg);
        }



        /// <summary>
        /// This Method used to Update the Doctor record
        /// Pass DoctorReg as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]DoctorReg oDoctorReg)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();


            if (oDoctorReg.ImagePath != null && oDoctorReg.ImagePath != "")// If Upload image request in Update Doctor 
            {

                //check file validation and upload a file on the server
                oResultModel = FileUpload(Me.LoginId, oDoctorReg.ImagePath, oDoctorReg.FileName, oDoctorReg.FileFlag, "Doctor/DoctorLicence", _hostingEnvironment);
                if (oResultModel.Status > Constants.SUCCESS)//If file validation failed
                    return oResultModel;

                oDoctorReg.MedicalLicenseImage = oResultModel.Message;// Pass the image path for save in the database
            }
            return await _srv.Update(Me, oDoctorReg);
        }

        /// <summary>
        /// This Method Delete the Doctor Record by Passing Doctor Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }


        [HttpGet("GetPath")]
        public string GetPath()
        {
            return _hostingEnvironment.WebRootPath;
        }


    }
}
