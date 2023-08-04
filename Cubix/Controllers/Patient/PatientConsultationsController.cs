using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.BAL.Interfaces.Patient;
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
    /// This class used for Get and update the Patient Profile
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(PatientProfileTokenFilter))]
    public class PatientConsultationsController : BaseController
    {
        private readonly IPatientConsultations _srv;
        private readonly ICommon _srvCommon;
        private readonly IDistributedCache _distributedCache;
        private readonly IAuth _srvAuth;
        /// <summary>
        /// Patient Profile Controller Comstructor
        /// </summary>
        public PatientConsultationsController(IPatientConsultations user, ICommon srvCommon, IDistributedCache distributedCache, IAuth srvAuth)
        {
            _srv = user;
            _srvCommon = srvCommon;
            _distributedCache = distributedCache;
            _srvAuth = srvAuth;
        }


        /// <summary>
        /// This Method Get the Today Consultations List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("gettodayappointments")]
        public async Task<ResultModel<object>> GetTodayAppointments([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetTodayAppointments(Me, oSearchModel);
        }



        /// <summary>
        /// This Method Get the Past Consultations List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("getpastconsultations")]
        public async Task<ResultModel<object>> GetPastConsultations([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetPastConsultations(Me, oSearchModel);
        }



        /// <summary>
        /// This Method Verify the patient tele-consultation appointment
        /// </summary>
        [HttpPost("waitingroom")]
        public async Task<ResultModel<PatientTeleConsultationReg>> patientlogin(PatientLoginModel oPatientLoginModel)
        {
            ResultModel<PatientTeleConsultationReg> result = new ResultModel<PatientTeleConsultationReg>();
            var user = await _srvAuth.PatientLogin(oPatientLoginModel);
            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.PatientId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.Mobile.ToString()),
                new Claim("LoginId", user.Model.PatientId.ToString()),
                new Claim("UserTypeId",Constants.PATIENT_USER.ToString()),
                new Claim("CreatedById",0.ToString()),
                new Claim("DepartmentId",user.Model.DepartmentId.ToString()),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                                SecurityAlgorithms.HmacSha256)
                    );
                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_PATIENT_USER + user.Model.PatientId.ToString(), new TokenCacheModel { LoginId = user.Model.PatientId, UserTypeId = Constants.PATIENT_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }


        /// <summary>
        /// This Method used to refund request
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("refundrequest")]
        public async Task<ResultModel<object>> RefundRequest([FromBody] RefundRequest oRefundRequest)
        {
            return await _srv.RefundRequest(Me, oRefundRequest);
        }


        /// <summary>
        /// This Method used to get patient consultation detail
        /// Pass ConsultationId
        /// </summary>
        [HttpGet("getconsultationpatientdetail/{Id}")]
        public async Task<ResultModel<SuperPatientTeleConsultation>> GetConsultationPatientDetail(long Id)
        {
            var Result = await _srv.GetConsultationPatientDetail(Me, Id);
            if (Result.Model.PatientTeleConsultationDetail != null && Result.Model.PatientTeleConsultationDetail.PatientTeleConsultationId > 0)
                Result.Model.PatientDocumentReg = _srvCommon.GetPatientDocumentRegByConsultationId(Id).Result;

            return Result;
        }


        /// <summary>
        /// This Method used to get patient consultation completeconsultation
        /// Pass ConsultationId
        /// </summary>
        [HttpGet("getcompleteconsultation/{Id}")]
        public async Task<ResultModel<SuperPatientTeleConsultation>> GetCompleteConsultation(long Id)
        {
            var Result = await _srv.GetConsultationPatientDetail(Me, Id);
            if (Result.Model.PatientTeleConsultationDetail != null && Result.Model.PatientTeleConsultationDetail.PatientTeleConsultationId > 0)
            {
                Result.Model.PatientDocumentReg = _srvCommon.GetPatientDocumentRegByConsultationId(Id).Result;
                Result.Model.PatientTeleConsultationAllergy = _srvCommon.GetPatientTeleConsultationAllergyByConsultationId(Id).Result;
                Result.Model.PatientTeleConsultationDiagnosis = _srvCommon.GetPatientTeleConsultationDiagnosisByConsultationId(Id).Result;
                Result.Model.PatientTeleConsultationExamination = _srvCommon.GetPatientTeleConsultationExaminationByConsultationId(Id).Result;
                Result.Model.PatientTeleConsultationMedicine = _srvCommon.GetPatientTeleConsultationMedicineByConsultationId(Id).Result;
            }

            return Result;
        }
    }
}
