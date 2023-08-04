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
    public class MedicineMasterService : IMedicineMaster
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@MedicineId", Id);
                Result.Status = await DBQuery.ExeQuery("Delete from MasterMedicine Where  MedicineId=@MedicineId and CreatedById=@CreatedById", Pars) == 1 ? 1 : 2;
                if (Result.Status == 1)
                    Result.Message = Constants.DELETE_MESSAGE;
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
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@MedicineId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterMedicine Where  MedicineId=@MedicineId and CreatedById=@CreatedById", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_MasterMedicine_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, MasterMedicine oMasterMedicine)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MedicineName", oMasterMedicine.MedicineName);
                Pars.Add("@Status", oMasterMedicine.Status);
                Pars.Add("@DrugId", oMasterMedicine.DrugId);
                Pars.Add("@DrugType", oMasterMedicine.DrugType);
                Pars.Add("@Description", oMasterMedicine.Description);
                Pars.Add("@CompanyName", oMasterMedicine.CompanyName);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CompanyId", oMasterMedicine.CompanyId);
                var res = await DBQuery.ExeSPScaler<int>("SP_MasterMedicine_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, MasterMedicine oMasterMedicine)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MedicineId", oMasterMedicine.MedicineId);
                Pars.Add("@MedicineName", oMasterMedicine.MedicineName);
                Pars.Add("@Status", oMasterMedicine.Status);
                Pars.Add("@DrugId", oMasterMedicine.DrugId);
                Pars.Add("@DrugType", oMasterMedicine.DrugType);
                Pars.Add("@Description", oMasterMedicine.Description);
                Pars.Add("@CompanyName", oMasterMedicine.CompanyName);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CompanyId", oMasterMedicine.CompanyId);
                var res = await DBQuery.ExeSPScaler<int>("SP_MasterMedicine_Update", Pars);
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
