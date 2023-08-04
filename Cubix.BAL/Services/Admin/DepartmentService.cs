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
    public class DepartmentService : IDepartment
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
             
                Pars.Add("@DepartmentId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_DoctorReg_Count_By_DepartmentId", Pars) == 0)
                {
                    Pars.Add("@AdminId", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from DepartmentReg Where  DepartmentId=@DepartmentId", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@AdminId", oTokenModel.LoginId);
                Pars.Add("@DepartmentId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from DepartmentReg Where  DepartmentId=@DepartmentId", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_DepartmentReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, DepartmentReg oDepartmentReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oDepartmentReg.HospitalId);
                Pars.Add("@BranchId", oDepartmentReg.BranchId);
                Pars.Add("@DepartmentName", oDepartmentReg.DepartmentName);
                Pars.Add("@DepartmentContactMobile", oDepartmentReg.DepartmentContactMobile);
                Pars.Add("@HospitalDepartmentLink", oDepartmentReg.HospitalDepartmentLink);
                Pars.Add("@Status", oDepartmentReg.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@Amount", oDepartmentReg.Amount);
                Pars.Add("@IsRefundAllowed", oDepartmentReg.IsRefundAllowed);


                var res = await DBQuery.ExeSPScaler<int>("SP_DepartmentReg_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, DepartmentReg oDepartmentReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DepartmentId", oDepartmentReg.DepartmentId);
                Pars.Add("@HospitalId", oDepartmentReg.HospitalId);
                Pars.Add("@BranchId", oDepartmentReg.BranchId);
                Pars.Add("@DepartmentName", oDepartmentReg.DepartmentName);
                Pars.Add("@DepartmentContactMobile", oDepartmentReg.DepartmentContactMobile);
                Pars.Add("@HospitalDepartmentLink", oDepartmentReg.HospitalDepartmentLink);
                Pars.Add("@Status", oDepartmentReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@Amount", oDepartmentReg.Amount);
                Pars.Add("@IsRefundAllowed", oDepartmentReg.IsRefundAllowed);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                var res = await DBQuery.ExeSPScaler<int>("SP_DepartmentReg_Update", Pars);
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

        public async Task<ResultModel<MasterDepartmentSlotTime>> GetSlots(TokenModel oTokenModel, DepartmentSlotRequest oDepartmentSlotRequest)
        {
            ResultModel<MasterDepartmentSlotTime> Result = new ResultModel<MasterDepartmentSlotTime>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DepartmentId", oDepartmentSlotRequest.DepartmentId);
                Pars.Add("@DayId", oDepartmentSlotRequest.DayId);
                Pars.Add("@DayName", oDepartmentSlotRequest.DayName);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Result.LstModel = await DBQuery.ExeSPList<MasterDepartmentSlotTime>("SP_MasterDepartmentSlotTime_Select", Pars);

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

        public async Task<ResultModel<object>> InsertSlots(TokenModel oTokenModel, List<MasterDepartmentSlotTime> oMasterDepartmentSlotTime)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DayId", oMasterDepartmentSlotTime[0].DayId);
                Pars.Add("@DepartmentId", oMasterDepartmentSlotTime[0].DepartmentId);
                Result.Status = await DBQuery.ExeQuery("Delete from MasterDepartmentSlotTime Where  DepartmentId=@DepartmentId and DayId=@DayId", Pars) == 1 ? 1 : 2;


                foreach (var item in oMasterDepartmentSlotTime)
                {

                    Pars.Add("@SlotFromTime", item.SlotFromTime);
                    Pars.Add("@SlotEndTime", item.SlotEndTime);
                    Pars.Add("@Status", 1);
                    Pars.Add("@CreatedDate", DateTime.Now);
                    Pars.Add("@CreatedById", oTokenModel.LoginId);
                    Pars.Add("@ModifiedDate", DateTime.Now);
                    Pars.Add("@MasterSlotId", item.MasterSlotId);
                    Pars.Add("@DayName", item.DayName);

                    var res = await DBQuery.ExeSPScaler<int>("SP_MasterDepartmentSlotTime_Insert", Pars);
                    if (res == 0)
                    {
                        Result.Message = Constants.NOTCREATED_MESSAGE;
                        Result.Status = Constants.NOTCREATED;
                    }
                    else
                    {
                        Result.Status = Constants.SUCCESS;
                        Result.Message = Constants.CREATED_MESSAGE;
                    }
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
