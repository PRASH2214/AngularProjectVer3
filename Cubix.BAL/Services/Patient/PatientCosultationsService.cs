using Cubix.BAL.Interfaces.Patient;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.Patient
{
    public class PatientConsultationsService : IPatientConsultations
    {

        public async Task<ResultModel<object>> GetTodayAppointments(TokenModel oTokenModel, SearchModel oSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SearchValue", oSearchModel.SearchValue);
                Pars.Add("@Skip", oSearchModel.Skip);
                Pars.Add("@Take", oSearchModel.ItemsPerPage);
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_PatientTeleConsultationReg_TodayPatientAppointment_Select", Pars);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                Result.Success = false;
                Result.Message = Constants.EXCEPTION_MESSAGE;
                Result.Status = Constants.EXCEPTION;
            }
            return Result;
        }
        public async Task<ResultModel<object>> GetPastConsultations(TokenModel oTokenModel, SearchModel oSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SearchValue", oSearchModel.SearchValue);
                Pars.Add("@Skip", oSearchModel.Skip);
                Pars.Add("@Take", oSearchModel.ItemsPerPage);
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_PatientTeleConsultationReg_PastPatientConsultations_Select", Pars);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                Result.Success = false;
                Result.Message = Constants.EXCEPTION_MESSAGE;
                Result.Status = Constants.EXCEPTION;
            }
            return Result;
        }


        public async Task<ResultModel<object>> RefundRequest(TokenModel oTokenModel, RefundRequest oRefundRequest)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationReferenceNumber", oRefundRequest.ConsultationReferenceNumber);
                Pars.Add("@Status", Constants.REFUND_REQUEST);
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Pars.Add("@RefundReason", oRefundRequest.RefundReason);
                var res = await DBQuery.ExeQuery("update PatientTeleConsultationReg set status=@Status,RefundReason=@RefundReason where  ConsultationReferenceNumber=@ConsultationReferenceNumber and PatientId=@PatientId", Pars);
                Result.Status = Constants.SUCCESS;
                Result.Message = Constants.SUCCESS_MESSAGE;
                return Result;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }


        public async Task<ResultModel<SuperPatientTeleConsultation>> GetConsultationPatientDetail(TokenModel oTokenModel, long ConsultationId)
        {
            ResultModel<SuperPatientTeleConsultation> Result = new ResultModel<SuperPatientTeleConsultation>();
            try
            {//SP_Get_Consultation_Detail_By_Doctor
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", ConsultationId);
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Result.Model = new SuperPatientTeleConsultation();
                Result.Model.PatientTeleConsultationDetail = await DBQuery.ExeSPScaler<PatientTeleConsultationDetail>("SP_Get_Consultation_Detail_By_Patient", Pars);

                Result.Status = Constants.SUCCESS;
                Result.Message = Constants.SUCCESS_MESSAGE;
                return Result;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }
    }
}
