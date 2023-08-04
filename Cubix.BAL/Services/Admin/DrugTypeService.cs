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
    public class DrugTypeService : IDrugType
    {

        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DrugTypeId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_MasterMedicine_Count_By_DrugTypeId", Pars) == 0)
                {
                    Pars.Add("@CreatedById", oTokenModel.LoginId);

                    Result.Status = await DBQuery.ExeQuery("Delete from DrugType Where  DrugTypeId=@DrugTypeId and CreatedById=@CreatedById", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@DrugTypeId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from DrugType Where  DrugTypeId=@DrugTypeId and CreatedById=@CreatedById", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_DrugType_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, DrugType oDrugType)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Name", oDrugType.Name);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from DrugType Where  Name=@Name and CreatedById=@CreatedById", Pars);
                if (Result.Model != null)
                {
                    Result.Model = null;
                    Result.Message = Constants.ALREADY_EXISTS_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                    return Result;
                }

                Pars.Add("@Description", oDrugType.Description);
                Pars.Add("@Status", oDrugType.Status);
                var res = await DBQuery.ExeSPScaler<int>("SP_DrugType_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, DrugType oDrugType)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DrugTypeId", oDrugType.DrugTypeId);
                Pars.Add("@Name", oDrugType.Name);
                Pars.Add("@Description", oDrugType.Description);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@Status", oDrugType.Status);
                var res = await DBQuery.ExeSPScaler<int>("SP_DrugType_Update", Pars);
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
