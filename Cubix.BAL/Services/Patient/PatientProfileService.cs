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
    public class PatientProfileService : IPatientProfile
    {

        public async Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeSPScaler<object>("SP_Get_Patient_DashBoard_Count", Pars);
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

        public async Task<ResultModel<object>> GetPatient(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from PatientReg Where status=1  and  PatientId=@PatientId ", Pars);
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

        public async Task<ResultModel<object>> UpdatePatient(PatientReg oPatientReg, TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();

                Pars.Add("@PatientId", oPatientReg.PatientId);
                Pars.Add("@PatientReferenceNumber", oPatientReg.PatientReferenceNumber);
                Pars.Add("@FirstName", oPatientReg.FirstName);
                Pars.Add("@LastName", oPatientReg.LastName);
                Pars.Add("@CountryId", oPatientReg.CountryId);
                Pars.Add("@StateId", oPatientReg.StateId);
                Pars.Add("@DistrictId", oPatientReg.DistrictId);
                Pars.Add("@CityId", oPatientReg.CityId);
                Pars.Add("@PatientAddress", oPatientReg.PatientAddress);
                Pars.Add("@DOB", oPatientReg.DOB);
                Pars.Add("@Age", oPatientReg.Age);
                Pars.Add("@PinCode", oPatientReg.PinCode);
                Pars.Add("@Status", oPatientReg.Status);
                Pars.Add("@GenderId", oPatientReg.GenderId);
                Pars.Add("@EmailAddress", oPatientReg.EmailAddress);
                Pars.Add("@RelationId", oPatientReg.RelationId);
                Pars.Add("@RelationName", oPatientReg.RelationName);
                Pars.Add("@ModifiedDate", DateTime.Now);
                var res = await DBQuery.ExeSPScaler<int>("SP_PatientReg_Update", Pars);
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
                Pars.Add("@PatientId", oTokenModel.LoginId);
                Result.Status = await DBQuery.ExeQuery("update  PatientReg set ProfileImagePath=@ImagePath Where status=1  and  PatientId=@PatientId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;
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

    }
}
