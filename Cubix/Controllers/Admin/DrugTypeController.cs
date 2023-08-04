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
    /// This class used for Get, insert,  update, delete the DrugType
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class DrugTypeController : BaseController
    {

        private readonly IDrugType _srv;


        /// <summary>
        /// DrugType Controller Comstructor
        /// </summary>
        public DrugTypeController(IDrugType user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the DrugType List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the DrugType Record by Passing DrugTypeId
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the DrugType record
        /// Pass DrugType as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]DrugType oDrugType)
        {
            return await _srv.Insert(Me, oDrugType);
        }

        /// <summary>
        /// This Method used to Update the DrugType record
        /// Pass DrugType as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]DrugType oDrugType)
        {
            return await _srv.Update(Me, oDrugType);
        }

        /// <summary>
        /// This Method Delete the DrugType Record by Passing DrugTypeId
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
