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
    /// This class used for Get, insert,  update, delete the DrugMaster
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class DrugMasterController : BaseController
    {

        private readonly IDrugMaster _srv;


        /// <summary>
        /// DrugMaster Controller Comstructor
        /// </summary>
        public DrugMasterController(IDrugMaster user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the DrugMaster List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the DrugMaster Record by Passing DrugId
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the DrugMaster record
        /// Pass MasterDrug as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]MasterDrug oMasterDrug)
        {
            return await _srv.Insert(Me, oMasterDrug);
        }

        /// <summary>
        /// This Method used to Update the DrugMaster record
        /// Pass MasterDrug as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]MasterDrug oMasterDrug)
        {
            return await _srv.Update(Me, oMasterDrug);
        }

        /// <summary>
        /// This Method Delete the DrugMaster Record by Passing DrugId
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
