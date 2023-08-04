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
    public class CompanyService : ICompany
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
              
                Pars.Add("@CompanyId", Id);
                if (await DBQuery.ExeSPScaler<int>("SP_MrReg_Count_By_CompanyId", Pars) == 0 && await DBQuery.ExeSPScaler<int>("SP_MasterMedicine_Count_By_CompanyId", Pars) == 0)
                {
                    Pars.Add("@AdminId", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from CompanyReg Where  CompanyId=@CompanyId", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@CompanyId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from CompanyReg Where CompanyId=@CompanyId", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_CompanyReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, CompanyReg oCompanyReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CompanyName", oCompanyReg.CompanyName);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oCompanyReg.StateId);
                Pars.Add("@DistrictId", oCompanyReg.DistrictId);
                Pars.Add("@CityId", oCompanyReg.CityId);
                Pars.Add("@CompanyAddress", oCompanyReg.CompanyAddress);
                Pars.Add("@CompanyLicenseNumber", oCompanyReg.CompanyLicenseNumber);
                Pars.Add("@SpocName", oCompanyReg.SpocName);
                Pars.Add("@SpocMobile", oCompanyReg.SpocMobile);
                Pars.Add("@AdminName", oCompanyReg.AdminName);
                Pars.Add("@AdminMobile", oCompanyReg.AdminMobile);
                Pars.Add("@PinCode", oCompanyReg.PinCode);
                Pars.Add("@CompanyWebLink", oCompanyReg.CompanyWebLink);
                Pars.Add("@Status", oCompanyReg.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);


                var res = await DBQuery.ExeSPScaler<int>("SP_CompanyReg_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, CompanyReg oCompanyReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CompanyId", oCompanyReg.CompanyId);
                Pars.Add("@CompanyName", oCompanyReg.CompanyName);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oCompanyReg.StateId);
                Pars.Add("@DistrictId", oCompanyReg.DistrictId);
                Pars.Add("@CityId", oCompanyReg.CityId);
                Pars.Add("@CompanyAddress", oCompanyReg.CompanyAddress);
                Pars.Add("@CompanyLicenseNumber", oCompanyReg.CompanyLicenseNumber);
                Pars.Add("@SpocName", oCompanyReg.SpocName);
                Pars.Add("@SpocMobile", oCompanyReg.SpocMobile);
                Pars.Add("@AdminName", oCompanyReg.AdminName);
                Pars.Add("@AdminMobile", oCompanyReg.AdminMobile);
                Pars.Add("@PinCode", oCompanyReg.PinCode);
                Pars.Add("@CompanyWebLink", oCompanyReg.CompanyWebLink);
                Pars.Add("@Status", oCompanyReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                var res = await DBQuery.ExeSPScaler<int>("SP_CompanyReg_Update", Pars);
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
