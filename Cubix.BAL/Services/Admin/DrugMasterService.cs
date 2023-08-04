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
    public class DrugMasterService : IDrugMaster
    {

        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();

                Pars.Add("@DrugId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_MasterMedicine_Count_By_DrugId", Pars) == 0)
                {
                    Pars.Add("@CreatedById", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from MasterDrug Where  DrugId=@DrugId and CreatedById=@CreatedById", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@DrugId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterDrug Where  DrugId=@DrugId and CreatedById=@CreatedById", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MasterDrug_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, MasterDrug oMasterDrug)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DrugName", oMasterDrug.DrugName);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterDrug Where  DrugName=@DrugName and CreatedById=@CreatedById", Pars);
                if (Result.Model != null)
                {
                    Result.Model = null;
                    Result.Message = Constants.ALREADY_EXISTS_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                    return Result;
                }
                Pars.Add("@Description", oMasterDrug.Description);
                Pars.Add("@Status", oMasterDrug.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_MasterDrug_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, MasterDrug oMasterDrug)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DrugId", oMasterDrug.DrugId);
                Pars.Add("@DrugName", oMasterDrug.DrugName);
                Pars.Add("@Description", oMasterDrug.Description);
                Pars.Add("@Status", oMasterDrug.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                var res = await DBQuery.ExeSPScaler<int>("SP_MasterDrug_Update", Pars);
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
