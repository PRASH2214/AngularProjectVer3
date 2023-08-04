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
    public class SpecialityMasterService : ISpecialityMaster
    {

        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
               
                Pars.Add("@SpecialityId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_DoctorReg_Count_By_SpecialityId", Pars) == 0)
                {
                    Pars.Add("@CreatedById", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from MasterSpecialityData Where  SpecialityId=@SpecialityId and CreatedById=@CreatedById", Pars) == 1 ? 1 : 2;
                    if (Result.Status == 1)
                        Result.Message = Constants.DELETE_MESSAGE;
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
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@SpecialityId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterSpecialityData Where  SpecialityId=@SpecialityId and CreatedById=@CreatedById", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MasterSpecialityData_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, MasterSpecialityData oMasterSpecialityData)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SpecialityName", oMasterSpecialityData.SpecialityName);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterSpecialityData Where  SpecialityName=@SpecialityName and CreatedById=@CreatedById", Pars);
                if (Result.Model != null)
                {
                    Result.Model = null;
                    Result.Message = Constants.ALREADY_EXISTS_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                    return Result;
                }

                Pars.Add("@Description", oMasterSpecialityData.Description);            
                Pars.Add("@Status", oMasterSpecialityData.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_MasterSpecialityData_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                    Result.Message = Constants.CREATED_MESSAGE;
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, MasterSpecialityData oMasterSpecialityData)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SpecialityId", oMasterSpecialityData.SpecialityId);
                Pars.Add("@Description", oMasterSpecialityData.Description);
                Pars.Add("@SpecialityName", oMasterSpecialityData.SpecialityName);
                Pars.Add("@Status", oMasterSpecialityData.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                var res = await DBQuery.ExeSPScaler<int>("SP_MasterSpecialityData_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
                    Result.Status = Constants.NOTUPDATED;
                }
                else
                    Result.Message = Constants.UPDATED_MESSAGE;
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
