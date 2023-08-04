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
    public class SlotTimeMasterService : ISlotTimeMaster
    {

        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MasterSlotId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_MasterDepartmentSlotTime_Count_By_MasterSlotId", Pars) == 0)
                {
                    Pars.Add("@CreatedById", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from MasterSlots Where MasterSlotId=@MasterSlotId and CreatedById=@CreatedById", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@MasterSlotId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterSlots Where  MasterSlotId=@MasterSlotId and CreatedById=@CreatedById", Pars);
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

        public async Task<ResultModel<object>> GetAll(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                //  Pars.Add("@SearchValue", oSearchModel.SearchValue);
                //    Pars.Add("@Skip", oSearchModel.Skip);
                //    Pars.Add("@Take", oSearchModel.ItemsPerPage);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  * from MasterSlots Where CreatedById=@CreatedById order by SlotFromTime", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, MasterSlots oMasterSlots)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SlotFromTime", oMasterSlots.SlotFromTime);
                Pars.Add("@SlotEndTime", oMasterSlots.SlotEndTime);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from MasterSlots Where  SlotFromTime=@SlotFromTime and SlotEndTime=@SlotEndTime and CreatedById=@CreatedById", Pars);
                if (Result.Model != null)
                {
                    Result.Model = null;
                    Result.Message = Constants.ALREADY_EXISTS_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                    return Result;
                }

                Pars.Add("@Status", oMasterSlots.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                var res = await DBQuery.ExeSPScaler<int>("SP_MasterSlots_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, MasterSlots oMasterSlots)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MasterSlotId", oMasterSlots.MasterSlotId);
                Pars.Add("@SlotFromTime", oMasterSlots.SlotFromTime);
                Pars.Add("@SlotEndTime", oMasterSlots.SlotEndTime);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@Status", oMasterSlots.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                var res = await DBQuery.ExeSPScaler<int>("SP_MasterSlots_Update", Pars);
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
