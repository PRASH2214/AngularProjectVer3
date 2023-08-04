using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.BAL.Interfaces.Doctor;
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers.Doctor
{
    /// <summary>
    /// This class used for Get and update the Doctor Profile
    /// Only Authorised User can access the mehtods of this Api Controller
    /// </summary>
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(DoctorTokenFilter))]
    public class DoctorConsultationsController : BaseController
    {
        private readonly IDoctorConsultations _srv;
        private readonly ICommon _srvCommon;

        /// <summary>
        /// Doctor Profile Controller Comstructor
        /// </summary>
        public DoctorConsultationsController(IDoctorConsultations user, ICommon srvCommon)
        {
            _srv = user;
            _srvCommon = srvCommon;
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
        /// This Method Get the Today Consultations for MR List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("getmrtodayappointments")]
        public async Task<ResultModel<object>> GetMRTodayAppointments([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetMRTodayAppointments(Me, oSearchModel);
        }



        /// <summary>
        /// This Method Get the MR Past Consultations List by Search
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("getmrpastconsultations")]
        public async Task<ResultModel<object>> GetMRPastConsultations([FromBody] SearchModel oSearchModel)
        {
            oSearchModel.Skip = Helper.GetSkipCount(oSearchModel.CurrentPage, oSearchModel.ItemsPerPage);
            return await _srv.GetMRPastConsultations(Me, oSearchModel);
        }

        /// <summary>
        /// This method used for Get the Patient Profile
        /// </summary>
        [HttpGet("getpatientprofile/{Id}")]
        public async Task<ResultModel<object>> GetPatientProfile(long Id)
        {
            return await _srvCommon.GetPatientProfile(Id);
        }




        /// <summary>
        /// This Method used to refund reponse
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("refundresponse")]
        public async Task<ResultModel<PatientTeleConsultationReg>> RefundResponse([FromBody] RefundRequest oRefundRequest)
        {

            var result = _srv.RefundResponse(Me, oRefundRequest).Result;
            // if successfullt approved refund request
            if (result.Status == Constants.SUCCESS && oRefundRequest.Status == Constants.REFUND_APPROVED)
            {
                ResponseModel oResponseModel = await CashFree_PaymentGateway.RefundRequest(new RequestModel
                {
                    appId = AppSetting.PaymentAPIKey,
                    secretKey = AppSetting.PaymentSecretKey,
                    referenceId = result.Model.PaymentReferenceId,
                    refundAmount = result.Model.PaymentAmmount.ToString(),
                    refundNote = oRefundRequest.RefundResponseReason,
                    refundType = "INSTANT"
                });
                if (oResponseModel.status == "OK")
                {
                    //save merchantRefundId
                }
            }
            return result;
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



        /// This Method used to insert reponse
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("insertconsultationresponse")]
        public async Task<ResultModel<object>> InsertConsultationResponse([FromBody] SuperPatientTeleConsultation oSuperPatientTeleConsultation)
        {

            var result = _srvCommon.InsertConsultationResponse(Me, oSuperPatientTeleConsultation.PatientTeleConsultationDetail).Result;
            // if successfullt approved refund request
            if (result.Status == Constants.SUCCESS)
            {
                foreach (var item in oSuperPatientTeleConsultation.PatientTeleConsultationAllergy)
                    await _srvCommon.InsertPatientTeleConsultationAllergy(Me, item);

                foreach (var item in oSuperPatientTeleConsultation.PatientTeleConsultationDiagnosis)
                    await _srvCommon.InsertPatientTeleConsultationDiagnosis(Me, item);

                await _srvCommon.InsertPatientTeleConsultationExamination(Me, oSuperPatientTeleConsultation.PatientTeleConsultationExamination);

                foreach (var item in oSuperPatientTeleConsultation.PatientTeleConsultationMedicine)
                    await _srvCommon.InsertPatientTeleConsultationMedicine(Me, item);
            }
            return result;
        }
        /// This Method used to insert mr reponse
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("insertmrconsultationresponse")]
        public async Task<ResultModel<object>> InsertMRConsultationResponse([FromBody] MRResponseTeleConsultationReg oMRResponseTeleConsultationReg)
        {

            var result = await _srvCommon.InsertMRConsultationResponse(Me, oMRResponseTeleConsultationReg);

            return result;
        }


        /// <summary>
        /// This method used for Get the grug type
        /// </summary>

        [HttpGet("getdrugtype")]
        public async Task<ResultModel<object>> GetDrugType()
        {
            return await _srvCommon.GetDrugType(Convert.ToInt64(Me.CreatedById));
        }

        /// <summary>
        /// This method used for Get the grug type
        /// </summary>

        [HttpGet("getmedicinemaster")]
        public async Task<ResultModel<object>> GetMedicineMaster()
        {
            return await _srvCommon.GetMedicineMaster(Convert.ToInt64(Me.CreatedById));
        }

        /// <summary>
        /// This method used for Get the grug type
        /// </summary>

        [HttpGet("getmasterdosevalue")]
        public async Task<ResultModel<object>> GetMasterDosevalue()
        {
            return await _srvCommon.GetMasterDosevalue(Convert.ToInt64(Me.CreatedById));
        }
    }
}
