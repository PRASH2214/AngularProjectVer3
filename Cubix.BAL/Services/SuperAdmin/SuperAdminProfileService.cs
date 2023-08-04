using Cubix.BAL.Interfaces;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.SuperAdmin
{
    public class SuperAdminProfileService : ISuperAdminProfile
    {
        public async Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", 0);
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
        public async Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ImagePath", oFileUpload.ImagePath);
                Pars.Add("@SuperAdminId", oTokenModel.LoginId);
                Result.Status = await DBQuery.ExeQuery("update  SuperAdminReg set ProfileImagePath=@ImagePath Where status=1  and  SuperAdminId=@SuperAdminId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;
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
