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
    /// This class used for Get, insert,  update, delete the SpecialityMaster
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class SpecialityMasterController : BaseController
    {

        private readonly ISpecialityMaster _srv;


        /// <summary>
        /// SpecialityMaster Controller Comstructor
        /// </summary>
        public SpecialityMasterController(ISpecialityMaster user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the SpecialityMaster List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the SpecialityMaster Record by Passing SpecialityMaster Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the SpecialityMaster record
        /// Pass MasterSpecialityData as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]MasterSpecialityData oMasterSpecialityData)
        {
            return await _srv.Insert(Me, oMasterSpecialityData);
        }

        /// <summary>
        /// This Method used to Update the SpecialityMaster record
        /// Pass MasterSpecialityData as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]MasterSpecialityData oMasterSpecialityData)
        {
            return await _srv.Update(Me, oMasterSpecialityData);
        }

        /// <summary>
        /// This Method Delete the SpecialityMaster Record by Passing SpecialityMaster Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
