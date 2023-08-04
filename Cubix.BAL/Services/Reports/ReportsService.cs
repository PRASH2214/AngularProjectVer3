using Cubix.BAL.Interfaces.Reports;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.Reports
{
    public class ReportsService : IReports
    {
        public async Task<ResultModel<object>> GetMRAppointmentDetailByAdmin(TokenModel oTokenModel, ReportSearchModel oReportSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                long LoginId = Convert.ToInt64(oTokenModel.LoginId);

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oReportSearchModel.HospitalId);
                Pars.Add("@DoctorId", 0);
                Pars.Add("@CreatedById", LoginId);
                Pars.Add("@FromDate", oReportSearchModel.FromDate);
                Pars.Add("@ToDate", oReportSearchModel.ToDate);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_Report_MR_Appointment_Detail", Pars);

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

        public async Task<ResultModel<object>> GetMRAppointmentDetailByDoctor(TokenModel oTokenModel, ReportSearchModel oReportSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                long LoginId = Convert.ToInt64(oTokenModel.LoginId);
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oReportSearchModel.HospitalId);
                Pars.Add("@DoctorId", LoginId);
                Pars.Add("@CreatedById", 0);
                Pars.Add("@FromDate", oReportSearchModel.FromDate);
                Pars.Add("@ToDate", oReportSearchModel.ToDate);

                Result.LstModel = await DBQuery.ExeSPList<object>("SP_Report_MR_Appointment_Detail", Pars);

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

        public async Task<ResultModel<ReportMRResultModel>> GetMRAppointmentSummary(TokenModel oTokenModel, ReportSearchModel oReportSearchModel)
        {
            ResultModel<ReportMRResultModel> Result = new ResultModel<ReportMRResultModel>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oReportSearchModel.HospitalId);
                Pars.Add("@FromDate", oReportSearchModel.FromDate);
                Pars.Add("@ToDate", oReportSearchModel.ToDate);

                Result.LstModel = await DBQuery.ExeSPList<ReportMRResultModel>("SP_Report_MR_Appointment_Summary", Pars);
                if (Result.LstModel != null && Result.LstModel.Count > 0)
                {
                    long LoginId = Convert.ToInt64(oTokenModel.LoginId);
                    Result.LstModel = Result.LstModel.Where(s => s.CreatedBy == LoginId).ToList();
                }
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

        public async Task<ResultModel<object>> GetPatientAppointmentDetailByAdmin(TokenModel oTokenModel, ReportSearchModel oReportSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                long LoginId = Convert.ToInt64(oTokenModel.LoginId);

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oReportSearchModel.HospitalId);
                Pars.Add("@DoctorId", 0);
                Pars.Add("@CreatedById", LoginId);
                Pars.Add("@FromDate", oReportSearchModel.FromDate);
                Pars.Add("@ToDate", oReportSearchModel.ToDate);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_Report_Patient_Appointment_Detail", Pars);

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

        public async Task<ResultModel<object>> GetPatientAppointmentDetailByDoctor(TokenModel oTokenModel, ReportSearchModel oReportSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                long LoginId = Convert.ToInt64(oTokenModel.LoginId);
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oReportSearchModel.HospitalId);
                Pars.Add("@DoctorId", LoginId);
                Pars.Add("@CreatedById", 0);
                Pars.Add("@FromDate", oReportSearchModel.FromDate);
                Pars.Add("@ToDate", oReportSearchModel.ToDate);

                Result.LstModel = await DBQuery.ExeSPList<object>("SP_Report_Patient_Appointment_Detail", Pars);

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

        public async Task<ResultModel<ReportPatientResultModel>> GetPatientAppointmentSummary(TokenModel oTokenModel, ReportSearchModel oReportSearchModel)
        {
            ResultModel<ReportPatientResultModel> Result = new ResultModel<ReportPatientResultModel>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oReportSearchModel.HospitalId);
                Pars.Add("@FromDate", oReportSearchModel.FromDate);
                Pars.Add("@ToDate", oReportSearchModel.ToDate);

                Result.LstModel = await DBQuery.ExeSPList<ReportPatientResultModel>("SP_Report_Patient_Appointment_Summary", Pars);
                if (Result.LstModel != null && Result.LstModel.Count > 0)
                {
                    long LoginId = Convert.ToInt64(oTokenModel.LoginId);
                    Result.LstModel = Result.LstModel.Where(s => s.CreatedBy == LoginId).ToList();
                }
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
    }
}
