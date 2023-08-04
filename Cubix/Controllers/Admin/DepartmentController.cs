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
    /// This class used for Get, insert,  update, delete the Department
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
 //   [ServiceFilter(typeof(AdminTokenFilter))]
    public class DepartmentController : BaseController
    {

        private readonly IDepartment _srv;


        /// <summary>
        /// Department Controller Comstructor
        /// </summary>
        public DepartmentController(IDepartment user)
        {
            _srv = user;
        }

        /// <summary>
        /// This Method Get the Department List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("GetAll")]
        public async Task<ResultModel<object>> GetAll([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetAll(Me, oSearchModel);
        }

        /// <summary>
        /// This Method Get the Department Record by Passing Department Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ResultModel<object>> Get(int id)
        {
            return await _srv.Get(Me, id);
        }

        /// <summary>
        /// This Method used to Insert the Department record
        /// Pass DepartmentReg as Parameter
        /// </summary>
        [HttpPost]
        public async Task<ResultModel<object>> Post([FromBody]DepartmentReg oDepartmentReg)
        {
            return await _srv.Insert(Me, oDepartmentReg);
        }

        /// <summary>
        /// This Method used to Update the Department record
        /// Pass DepartmentReg as Parameter
        /// </summary>
        [HttpPut]
        public async Task<ResultModel<object>> Put([FromBody]DepartmentReg oDepartmentReg)
        {
            return await _srv.Update(Me, oDepartmentReg);
        }

        /// <summary>
        /// This Method Delete the Department Record by Passing Department Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ResultModel<object>> Delete(int id)
        {
            return await _srv.Delete(Me, id);
        }



        /// <summary>
        /// This Method used to Get Department Slots DayWise
        /// Pass DepartmentSlotRequest as Parameter
        /// </summary>
        [HttpPost("GetSlots")]
        public async Task<ResultModel<MasterDepartmentSlotTime>> GetSlots([FromBody]DepartmentSlotRequest oDepartmentSlotRequest)
        {
            return await _srv.GetSlots(Me, oDepartmentSlotRequest);
        }

        /// <summary>
        /// This Method used to Insert Department Slots DayWise
        /// Pass DepartmentSlotRequest as Parameter
        /// </summary>
        [HttpPost("InsertSlots")]
        public async Task<ResultModel<object>> InsertSlots([FromBody]List<MasterDepartmentSlotTime> oMasterDepartmentSlotTime)
        {
            return await _srv.InsertSlots(Me, oMasterDepartmentSlotTime);
        }


    }
}
