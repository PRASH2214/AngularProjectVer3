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
    public class AdminService : IAdmin
    {

        public async Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeSPScaler<object>("SP_Get_Admin_DashBoard_Count", Pars);
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
        public async Task<ResultModel<object>> GetAdmin(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminId", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from AdminReg Where status=1  and  AdminId=@AdminId ", Pars);
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

        public async Task<ResultModel<object>> UpdateAdmin(AdminReg oAdminReg, TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();

                Pars.Add("@AdminId", oTokenModel.LoginId);
                Pars.Add("@AdminName", oAdminReg.AdminName);
                Pars.Add("@StateId", oAdminReg.StateId);
                Pars.Add("@DistrictId", oAdminReg.DistrictId);
                Pars.Add("@CityId", oAdminReg.CityId);
                Pars.Add("@Address", oAdminReg.Address);
                Pars.Add("@AdminMobile", oAdminReg.AdminMobile);
                Pars.Add("@Status", 1);// Set Status active in case self update
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@ClientName", oAdminReg.ClientName);
                Pars.Add("@WebUrl", oAdminReg.WebUrl);
                Pars.Add("@MrUrl", oAdminReg.MrUrl);
                Pars.Add("@CreatedById", oAdminReg.CreatedById);
                var res = await DBQuery.ExeSPScaler<int>("SP_AdminReg_Update", Pars);
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
                Pars.Add("@AdminId", oTokenModel.LoginId);
                Result.Status = await DBQuery.ExeQuery("update  AdminReg set ProfileImagePath=@ImagePath Where status=1  and  AdminId=@AdminId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;
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
