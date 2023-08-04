using Cubix.BAL.Interfaces.Doctor;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.Doctor
{
    public class DoctorConsultationsService : IDoctorConsultations
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
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_PatientTeleConsultationReg_TodayDoctorAppointment_Select", Pars);
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
        public async Task<ResultModel<object>> GetMRTodayAppointments(TokenModel oTokenModel, SearchModel oSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SearchValue", oSearchModel.SearchValue);
                Pars.Add("@Skip", oSearchModel.Skip);
                Pars.Add("@Take", oSearchModel.ItemsPerPage);
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MRTeleConsultationReg_TodayDoctorAppointment_Select", Pars);
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
        public async Task<ResultModel<object>> GetMRPastConsultations(TokenModel oTokenModel, SearchModel oSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SearchValue", oSearchModel.SearchValue);
                Pars.Add("@Skip", oSearchModel.Skip);
                Pars.Add("@Take", oSearchModel.ItemsPerPage);
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MRTeleConsultationReg_PastConsultations_Select", Pars);
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
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_PatientTeleConsultationReg_PastConsultations_Select", Pars);
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

        public async Task<ResultModel<PatientTeleConsultationReg>> RefundResponse(TokenModel oTokenModel, RefundRequest oRefundRequest)
        {
            ResultModel<PatientTeleConsultationReg> Result = new ResultModel<PatientTeleConsultationReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationReferenceNumber", oRefundRequest.ConsultationReferenceNumber);
                Pars.Add("@Status", oRefundRequest.Status);
                Pars.Add("@PatientId", oRefundRequest.PatientId);
                Pars.Add("@RefundResponseReason", oRefundRequest.RefundResponseReason);
                var res = await DBQuery.ExeQuery("update PatientTeleConsultationReg set status=@Status,RefundResponseReason=@RefundResponseReason where  ConsultationReferenceNumber=@ConsultationReferenceNumber and PatientId=@PatientId and Status =" + Constants.REFUND_REQUEST, Pars);
                Result.Model = await DBQuery.ExeScalarQuery<PatientTeleConsultationReg>("Select  * from PatientTeleConsultationReg Where   ConsultationReferenceNumber=@ConsultationReferenceNumber and PatientId=@PatientId", Pars);
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
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.Model = new SuperPatientTeleConsultation();
                Result.Model.PatientTeleConsultationDetail = await DBQuery.ExeSPScaler<PatientTeleConsultationDetail>("SP_Get_Consultation_Detail_By_Doctor", Pars);

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
