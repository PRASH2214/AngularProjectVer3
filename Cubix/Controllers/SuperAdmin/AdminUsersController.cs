using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.BAL.Interfaces.SuperAdmin;
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
    public class AdminUsersController : BaseController
    {

        private readonly IAdminUsers _srv;
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Doctor Controller Comstructor
        /// </summary>
        public AdminUsersController(IAdminUsers user, IWebHostEnvironment hostingEnvironment)
        {
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// This Method Get the Admin List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the Admin Record by Passing Admin Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the Admin record
        /// Pass AdminReg as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]AdminReg oAdminReg)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();
            return await _srv.Insert(Me, oAdminReg);
        }



        /// <summary>
        /// This Method used to Update the Admin record
        /// Pass AdminReg as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]AdminReg oAdminReg)
        {
            ResultModel<object> oResultModel = new ResultModel<object>();

            return await _srv.Update(Me, oAdminReg);
        }

        /// <summary>
        /// This Method Delete the Admin Record by Passing Doctor Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }



    }
}
