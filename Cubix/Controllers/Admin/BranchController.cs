using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.Admin
{

    /// <summary>
    /// This class used for Get, insert,  update, delete the Branch
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class BranchController : BaseController
    {

        private readonly IBranch _srv;


        /// <summary>
        /// Branch Controller Comstructor
        /// </summary>
        public BranchController(IBranch user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the Branch List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the Branch Record by Passing Branch Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the Branch record
        /// Pass BranchReg as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]BranchReg oBranchReg)
        {
            return await _srv.Insert(Me, oBranchReg);
        }

        /// <summary>
        /// This Method used to Update the Branch record
        /// Pass BranchReg as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]BranchReg oBranchReg)
        {
            return await _srv.Update(Me, oBranchReg);
        }

        /// <summary>
        /// This Method Delete the Branch Record by Passing Branch Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
