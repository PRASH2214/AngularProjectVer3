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
    /// This class used for Get, insert,  update, delete the SlotTimeMaster
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminTokenFilter))]
    public class SlotMasterController : BaseController
    {

        private readonly ISlotTimeMaster _srv;


        /// <summary>
        /// SlotTimeMaster Controller Comstructor
        /// </summary>
        public SlotMasterController(ISlotTimeMaster user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the SlotTimeMaster List 
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ResultModel<object>> GetAll()
        {
            //  oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me);
        }

        /// <summary>
        /// This Method Get the SlotTimeMaster Record by Passing MasterSlotId
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the SlotTimeMaster record
        /// Pass MasterSlots as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]MasterSlots oMasterSlots)
        {
            return await _srv.Insert(Me, oMasterSlots);
        }

        /// <summary>
        /// This Method used to Update the SlotTimeMaster record
        /// Pass MasterSlots as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]MasterSlots oMasterSlots)
        {
            return await _srv.Update(Me, oMasterSlots);
        }

        /// <summary>
        /// This Method Delete the SlotTimeMaster Record by Passing MasterSlotId
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }

    }
}
