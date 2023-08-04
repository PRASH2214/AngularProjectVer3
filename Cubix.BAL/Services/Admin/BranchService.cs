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
    public class BranchService : IBranch
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
               
                Pars.Add("@BranchId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_DepartmentReg_Count_By_BranchId", Pars) == 0)
                {
                    Pars.Add("@AdminId", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from BranchReg Where  BranchId=@BranchId", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@BranchId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from BranchReg Where BranchId=@BranchId", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_BranchReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, BranchReg oBranchReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oBranchReg.HospitalId);
                Pars.Add("@BranchName", oBranchReg.BranchName);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oBranchReg.StateId);
                Pars.Add("@DistrictId", oBranchReg.DistrictId);
                Pars.Add("@CityId", oBranchReg.CityId);
                Pars.Add("@BranchHospitalAddress", oBranchReg.BranchHospitalAddress);
                Pars.Add("@BranchHospitalLicenseNumber", oBranchReg.BranchHospitalLicenseNumber);
                Pars.Add("@ContactName", oBranchReg.ContactName);
                Pars.Add("@ContactMobile", oBranchReg.ContactMobile);
                Pars.Add("@PinCode", oBranchReg.PinCode);
                Pars.Add("@BranchHospitalLink", oBranchReg.BranchHospitalLink);
                Pars.Add("@Status", oBranchReg.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);


                var res = await DBQuery.ExeSPScaler<int>("SP_BranchReg_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, BranchReg oBranchReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@BranchId", oBranchReg.BranchId);
                Pars.Add("@BranchName", oBranchReg.BranchName);
                Pars.Add("@HospitalId", oBranchReg.HospitalId);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oBranchReg.StateId);
                Pars.Add("@DistrictId", oBranchReg.DistrictId);
                Pars.Add("@CityId", oBranchReg.CityId);
                Pars.Add("@BranchHospitalAddress", oBranchReg.BranchHospitalAddress);
                Pars.Add("@BranchHospitalLicenseNumber", oBranchReg.BranchHospitalLicenseNumber);
                Pars.Add("@ContactName", oBranchReg.ContactName);
                Pars.Add("@ContactMobile", oBranchReg.ContactMobile);
                Pars.Add("@PinCode", oBranchReg.PinCode);
                Pars.Add("@BranchHospitalLink", oBranchReg.BranchHospitalLink);
                Pars.Add("@Status", oBranchReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                var res = await DBQuery.ExeSPScaler<int>("SP_BranchReg_Update", Pars);
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
