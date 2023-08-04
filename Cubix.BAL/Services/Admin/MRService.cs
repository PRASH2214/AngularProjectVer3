using Cubix.BAL.Interfaces;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services
{
    public class MRService : IMR
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminId", oTokenModel.LoginId);
                Pars.Add("@MRId", Id);
                Result.Status = await DBQuery.ExeQuery("Delete from MRReg Where  MRId=@MRId", Pars) == 1 ? 1 : 2;
                if (Result.Status == 1)
                {
                    Result.Status = await DBQuery.ExeQuery("Delete from UserLogin Where  ReferenceId=@MRId", Pars) == 1 ? 1 : 2;
                    Result.Message = Constants.DELETE_MESSAGE;
                }
                else
                {
                    Result.Status = Constants.NOTDELETED;
                    Result.Message = Constants.NOTDELETE_MESSAGE;
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

        public async Task<ResultModel<object>> Get(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminId", oTokenModel.LoginId);
                Pars.Add("@MRId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MRReg Where MRId=@MRId", Pars);
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

        public async Task<ResultModel<object>> GetAll(TokenModel oTokenModel, SearchModel oSearchModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SearchValue", oSearchModel.SearchValue);
                Pars.Add("@Skip", oSearchModel.Skip);
                Pars.Add("@Take", oSearchModel.ItemsPerPage);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MRReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, MRReg oMRReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
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
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);


                var res = await DBQuery.ExeSPScaler<int>("SP_MrReg_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else if (res == -3)
                {
                    Result.Message = Constants.LICENCEALREADYEXISTS_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    oMRReg.MrId = res;
                    await InsertUserLogin(oMRReg);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, MRReg oMRReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MRId", oMRReg.MrId);
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
                Pars.Add("@CreatedById", oTokenModel.LoginId);

                var res = await DBQuery.ExeSPScaler<int>("SP_MrReg_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
                    Result.Status = Constants.NOTUPDATED;
                }
                else if (res == -3)
                {
                    Result.Message = Constants.LICENCEALREADYEXISTS_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    await UpdateUserLogin(oMRReg);
                    Result.Message = Constants.UPDATED_MESSAGE;
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



        public async Task<ResultModel<object>> InsertUserLogin(MRReg oMRReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserTypeId", Constants.MR_USER);
                Pars.Add("@UserMobile", oMRReg.Mobile);
                Pars.Add("@Status", oMRReg.Status);
                Pars.Add("@ReferenceId", oMRReg.MrId);
                //   Pars.Add("@UserEmail", oDoctorReg.ma);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@OTP", 1234);
                Pars.Add("@UserEmail", oMRReg.Mobile);

                var res = await DBQuery.ExeSPScaler<int>("SP_UserLogin_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    Result.Message = Constants.CREATED_MESSAGE;
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

        public async Task<ResultModel<object>> UpdateUserLogin(MRReg oMRReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Status", oMRReg.Status);
                Pars.Add("@UserMobile", oMRReg.Mobile);
                Pars.Add("@ReferenceId", oMRReg.MrId);
                Pars.Add("@Otp", 0);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_UserLogin_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    Result.Message = Constants.CREATED_MESSAGE;
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

    }
}
