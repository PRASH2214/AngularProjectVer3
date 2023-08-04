using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.BAL.Interfaces.Patient;
using Cubix.BAL.Interfaces.Reports;
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.Patient
{
    /// <summary>
    /// This class used for Reports
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ReportsController : BaseController
    {
        private readonly IReports _srv;
        private readonly IDistributedCache _distributedCache;
        private readonly IAuth _srvAuth;
        /// <summary>
        /// Admin Reports Controller Comstructor
        /// </summary>
        public ReportsController(IReports user, IDistributedCache distributedCache, IAuth srvAuth)
        {
            _srv = user;
            _distributedCache = distributedCache;
            _srvAuth = srvAuth;
        }


        /// <summary>
        /// This Method Get the MR Summary Report
        /// Pass SearchModel as Parameter
        /// </summary>
        [ServiceFilter(typeof(AdminTokenFilter))]
        [HttpPost("getmrappointmentsummary")]
        public async Task<ResultModel<ReportMRResultModel>> GetMRAppointmentSummary([FromBody] ReportSearchModel oReportSearchModel)
        {
            return await _srv.GetMRAppointmentSummary(Me, oReportSearchModel);
        }



        /// <summary>
        /// This Method Get the Patient Summary Report
        /// Pass SearchModel as Parameter
        /// </summary>
        [ServiceFilter(typeof(AdminTokenFilter))]
        [HttpPost("getpatientappointmentsummary")]
        public async Task<ResultModel<ReportPatientResultModel>> GetPatientAppointmentSummary([FromBody] ReportSearchModel oReportSearchModel)
        {
            return await _srv.GetPatientAppointmentSummary(Me, oReportSearchModel);
        }


        /// <summary>
        /// This Method Get the MR Appointment Detail by Admin
        /// Pass SearchModel as Parameter
        /// </summary>
        [ServiceFilter(typeof(AdminTokenFilter))]
        [HttpPost("getmrappointmentdetailbyadmin")]
        public async Task<ResultModel<object>> GetMRAppointmentDetailByAdmin([FromBody] ReportSearchModel oReportSearchModel)
        {
            return await _srv.GetMRAppointmentDetailByAdmin(Me, oReportSearchModel);
        }

        /// <summary>
        /// This Method Get the Patient Appointment Detail by Admin
        /// Pass SearchModel as Parameter
        /// </summary>
        [ServiceFilter(typeof(AdminTokenFilter))]
        [HttpPost("getpatientappointmentdetailbyadmin")]
        public async Task<ResultModel<object>> GetPatientAppointmentDetailByAdmin([FromBody] ReportSearchModel oReportSearchModel)
        {
            return await _srv.GetPatientAppointmentDetailByAdmin(Me, oReportSearchModel);
        }


        /// <summary>
        /// This Method Get the MR Appointment Detail by Doctor
        /// Pass SearchModel as Parameter
        /// </summary>
        [ServiceFilter(typeof(DoctorTokenFilter))]
        [HttpPost("getmrappointmentdetailbydoctor")]
        public async Task<ResultModel<object>> GetMRAppointmentDetailByDoctor([FromBody] ReportSearchModel oReportSearchModel)
        {
            return await _srv.GetMRAppointmentDetailByDoctor(Me, oReportSearchModel);
        }

        /// <summary>
        /// This Method Get the Patient Appointment Detail by Doctor
        /// Pass SearchModel as Parameter
        /// </summary>
        [ServiceFilter(typeof(DoctorTokenFilter))]
        [HttpPost("getpatientappointmentdetailbydoctor")]
        public async Task<ResultModel<object>> GetPatientAppointmentDetailByDoctor([FromBody] ReportSearchModel oReportSearchModel)
        {
            return await _srv.GetPatientAppointmentDetailByDoctor(Me, oReportSearchModel);
        }

    }
}
