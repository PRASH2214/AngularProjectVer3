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
    /// This class used for Get, insert,  update, delete the Hospital
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class HospitalController : BaseController
    {

        private readonly IHospital _srv;


        /// <summary>
        /// Hospital Controller Comstructor
        /// </summary>
        public HospitalController(IHospital user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the Hospital List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the Hospital Record by Passing Hospital Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the Hospital record
        /// Pass HospitalReg as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]HospitalReg oHospitalReg)
        {
            return await _srv.Insert(Me, oHospitalReg);
        }

        /// <summary>
        /// This Method used to Update the Hospital record
        /// Pass HospitalReg as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]HospitalReg oHospitalReg)
        {
            return await _srv.Update(Me, oHospitalReg);
        }

        /// <summary>
        /// This Method Delete the Hospital Record by Passing Hospital Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
