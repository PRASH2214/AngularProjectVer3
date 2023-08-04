using Cubix.BAL.Interfaces.SuperAdmin;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.SuperAdmin
{
    public class AdminUsersService : IAdminUsers
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_HospitalReg_Count_By_AdminId", Pars) == 0)
                {

                    Result.Status = await DBQuery.ExeQuery("Delete from AdminReg Where  AdminId=@AdminId", Pars) == 1 ? 1 : 2;
                    if (Result.Status == 1)
                    {
                        Pars = new Dapper.DynamicParameters();
                        Pars.Add("@UserTypeId", Constants.ADMIN_USER);
                        Pars.Add("@AdminId", Id);
                        await DBQuery.ExeQuery("Delete from UserLogin Where  ReferenceId=@AdminId and UserTypeId=@UserTypeId", Pars);
                        Result.Message = Constants.DELETE_MESSAGE;
                    }
                    else
                    {
                        Result.Status = Constants.NOTDELETED;
                        Result.Message = Constants.NOTDELETE_MESSAGE;
                    }
                }
                else
                {
                    Result.Status = Constants.NOTDELETED;
                    Result.Message = Constants.ALREADY_ASSOCIATED_MESSAGE;
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
                Pars.Add("@AdminId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from AdminReg Where  AdminId=@AdminId", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_AdminReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, AdminReg oAdminReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminName", oAdminReg.AdminName);
                Pars.Add("@StateId", oAdminReg.StateId);
                Pars.Add("@DistrictId", oAdminReg.DistrictId);
                Pars.Add("@CityId", oAdminReg.CityId);
                Pars.Add("@Address", oAdminReg.Address);
                Pars.Add("@AdminMobile", oAdminReg.AdminMobile);
                Pars.Add("@Status", 1);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@ClientName", oAdminReg.ClientName);
                Pars.Add("@WebUrl", oAdminReg.WebUrl);
                Pars.Add("@MrUrl ", oAdminReg.MrUrl);
                Pars.Add("@ProfileImagePath ", oAdminReg.ProfileImagePath);
                var res = await DBQuery.ExeSPScaler<int>("SP_AdminReg_Insert", Pars);
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
                    oAdminReg.AdminId = res;
                    await InsertUserLogin(oAdminReg);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, AdminReg oAdminReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminId", oAdminReg.AdminId);
                Pars.Add("@AdminName", oAdminReg.AdminName);
                Pars.Add("@StateId", oAdminReg.StateId);
                Pars.Add("@DistrictId", oAdminReg.DistrictId);
                Pars.Add("@CityId", oAdminReg.CityId);
                Pars.Add("@Address", oAdminReg.Address);
                Pars.Add("@AdminMobile", oAdminReg.AdminMobile);
                Pars.Add("@Status", oAdminReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@ClientName", oAdminReg.ClientName);
                Pars.Add("@WebUrl", oAdminReg.WebUrl);
                Pars.Add("@MrUrl ", oAdminReg.MrUrl);

                var res = await DBQuery.ExeSPScaler<int>("SP_AdminReg_Update", Pars);
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
                    await UpdateUserLogin(oAdminReg);
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



        public async Task<ResultModel<object>> InsertUserLogin(AdminReg oAdminReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserTypeId", Constants.ADMIN_USER);
                Pars.Add("@UserMobile", oAdminReg.AdminMobile);
                Pars.Add("@Status", 1);
                Pars.Add("@ReferenceId", oAdminReg.AdminId);
                //   Pars.Add("@UserEmail", oDoctorReg.ma);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@OTP", 1234);
                Pars.Add("@UserEmail", "");

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

        public async Task<ResultModel<object>> UpdateUserLogin(AdminReg oAdminReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Status", oAdminReg.Status);
                Pars.Add("@UserMobile", oAdminReg.AdminMobile);
                Pars.Add("@ReferenceId", oAdminReg.AdminId);
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
