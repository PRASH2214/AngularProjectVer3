using Cubix.BAL.Interfaces.MR;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.MR
{
    public class MRProfileService : IMRProfile
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
                Pars.Add("@MRId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MRTeleConsultationReg_TodayMRAppointment_Select", Pars);
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
                Pars.Add("@MRId", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MRTeleConsultationReg_PastMRConsultations_Select", Pars);
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



        public async Task<ResultModel<object>> GetMR(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MRId", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MRReg Where status=1  and  MRId=@MRId ", Pars);
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

        public async Task<ResultModel<object>> UpdateMR(MRReg oMRReg, TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();

                Pars.Add("@MRId", oTokenModel.LoginId);
                Pars.Add("@FirstName", oMRReg.FirstName);
                Pars.Add("@MiddleName", oMRReg.MiddleName);
                Pars.Add("@LastName", oMRReg.LastName);
                Pars.Add("@CompanyId", oMRReg.CompanyId);
                Pars.Add("@CountryId", oMRReg.CountryId);
                Pars.Add("@StateId", oMRReg.StateId);
                Pars.Add("@DistrictId", oMRReg.DistrictId);
                Pars.Add("@CityId", oMRReg.CityId);
                Pars.Add("@MrAddress", oMRReg.MrAddress);
                Pars.Add("@MrLicenseNumber", oMRReg.MrLicenseNumber);
                Pars.Add("@GenderId", oMRReg.GenderId);
                Pars.Add("@Mobile", oMRReg.Mobile);
                Pars.Add("@MrLicenseImage", oMRReg.MrLicenseImage);
                Pars.Add("@DOB", oMRReg.DOB);
                Pars.Add("@Age", oMRReg.Age);
                Pars.Add("@PinCode", oMRReg.PinCode);
                Pars.Add("@Status", oMRReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.CreatedById);
                var res = await DBQuery.ExeSPScaler<int>("SP_MrReg_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
                    Result.Status = Constants.NOTUPDATED;
                }
                else
                {
                    Result.Message = Constants.UPDATED_MESSAGE;
                    Result.Status = Constants.SUCCESS;
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

        public async Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ImagePath", oFileUpload.ImagePath);
                Pars.Add("@MRId", oTokenModel.LoginId);
                Result.Status = await DBQuery.ExeQuery("update  MRReg set ProfileImagePath=@ImagePath Where status=1  and  MRId=@MRId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;
                if (Result.Status == Constants.SUCCESS)
                    Result.Message = oFileUpload.ImagePath;
                else
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
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


        public async Task<ResultModel<ResultMRReg>> InsertMRTeleConsultationReg(MRTeleConsultationReg oMRTeleConsultationReg)
        {
            ResultModel<ResultMRReg> Result = new ResultModel<ResultMRReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                MRTeleConsultationReg ooMRTeleConsultationReg = await DBQuery.ExeScalarQuery<MRTeleConsultationReg>("Select  * from MRTeleConsultationReg  where DoctorId =" + oMRTeleConsultationReg.DoctorId + " and   MRId=" + oMRTeleConsultationReg.MRId + " and CAST( AppointmentDate AS Date ) = CAST( GETDATE() AS Date )", Pars);
                if (ooMRTeleConsultationReg != null && ooMRTeleConsultationReg.MRTeleConsultationId > 0)
                {
                    Result.Status = Constants.CONSULTATION_ALREADY_EXIST;
                    Result.Message = Constants.MR_APPOINTMENT_ALREADY_EXIST_MESSAGE;
                    return Result;
                }
                Pars.Add("@Mobile", oMRTeleConsultationReg.Mobile);
                Pars.Add("@MRId", oMRTeleConsultationReg.MRId);
                Pars.Add("@MRReferenceNumber", oMRTeleConsultationReg.MRReferenceNumber);
                Pars.Add("@ConsultationReferenceNumber", oMRTeleConsultationReg.ConsultationReferenceNumber);
                Pars.Add("@HospitalId", oMRTeleConsultationReg.HospitalId);
                Pars.Add("@BranchId", oMRTeleConsultationReg.BranchId);
                Pars.Add("@DepartmentId", oMRTeleConsultationReg.DepartmentId);
                Pars.Add("@DoctorId", oMRTeleConsultationReg.DoctorId);
                Pars.Add("@DoctorSlotTimeId", oMRTeleConsultationReg.DoctorSlotTimeId);
                Pars.Add("@AppointmentDate", oMRTeleConsultationReg.AppointmentDate);
                Pars.Add("@SlotFromTime", oMRTeleConsultationReg.SlotFromTime);
                Pars.Add("@SlotEndTime", oMRTeleConsultationReg.SlotEndTime);
                Pars.Add("@Subject", oMRTeleConsultationReg.Subject);
                Pars.Add("@Type", oMRTeleConsultationReg.Type);
                Pars.Add("@Status", Constants.CONSULTATION_PENDING);
                Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_PENDING);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_MRTeleConsultationReg_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    string ConsultationReferenceNumber = Helper.GenerateMRTeleConsultationNumber(res);
                    // update Reference Number
                    await DBQuery.ExeQuery("update MRTeleConsultationReg set ConsultationReferenceNumber=" + ConsultationReferenceNumber + " where MRTeleConsultationId=" + res, Pars);

                    //update the Booked Slots Number 
                    await DBQuery.ExeQuery("update DoctorSlotTime set NoOfPatientsBooked=NoOfPatientsBooked+1 where ForUserTypeId=" + Constants.MR_USER + " and  DoctorSlotTimeId=" + oMRTeleConsultationReg.DoctorSlotTimeId, Pars);
                    Result.Model = new ResultMRReg();
                    Result.Model.MRTeleConsultationId = res;
                    Result.Message = Constants.CREATED_MESSAGE;
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
