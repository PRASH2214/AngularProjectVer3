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
    public class HospitalService : IHospital
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
              
                Pars.Add("@HospitalId", Id);
                if ( await DBQuery.ExeSPScaler<int>("SP_BranchReg_Count_By_HospitalId", Pars) == 0)
                {
                    Pars.Add("@AdminId", oTokenModel.LoginId);
                    Result.Status = await DBQuery.ExeQuery("Delete from HospitalReg Where HospitalId=@HospitalId", Pars) == 1 ? 1 : 2;
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
                Pars.Add("@HospitalId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from HospitalReg Where  HospitalId=@HospitalId", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_HospitalReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, HospitalReg oHospitalReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalName", oHospitalReg.HospitalName);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oHospitalReg.StateId);
                Pars.Add("@DistrictId", oHospitalReg.DistrictId);
                Pars.Add("@CityId", oHospitalReg.CityId);
                Pars.Add("@HospitalAddress", oHospitalReg.HospitalAddress);
                Pars.Add("@HospitalLicenseNumber", oHospitalReg.HospitalLicenseNumber);
                Pars.Add("@OwnerName", oHospitalReg.OwnerName);
                Pars.Add("@OwnerMobile", oHospitalReg.OwnerMobile);
                Pars.Add("@ContactName", oHospitalReg.ContactName);
                Pars.Add("@ContactMobile", oHospitalReg.ContactMobile);
                Pars.Add("@PinCode", oHospitalReg.PinCode);
                Pars.Add("@HospitalLink", oHospitalReg.HospitalLink);
                Pars.Add("@Status", oHospitalReg.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);

                var res = await DBQuery.ExeSPScaler<int>("SP_HospitalReg_Insert", Pars);
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, HospitalReg oHospitalReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalId", oHospitalReg.HospitalId);
                Pars.Add("@HospitalName", oHospitalReg.HospitalName);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oHospitalReg.StateId);
                Pars.Add("@DistrictId", oHospitalReg.DistrictId);
                Pars.Add("@CityId", oHospitalReg.CityId);
                Pars.Add("@HospitalAddress", oHospitalReg.HospitalAddress);
                Pars.Add("@HospitalLicenseNumber", oHospitalReg.HospitalLicenseNumber);
                Pars.Add("@OwnerName", oHospitalReg.OwnerName);
                Pars.Add("@OwnerMobile", oHospitalReg.OwnerMobile);
                Pars.Add("@ContactName", oHospitalReg.ContactName);
                Pars.Add("@ContactMobile", oHospitalReg.ContactMobile);
                Pars.Add("@PinCode", oHospitalReg.PinCode);
                Pars.Add("@HospitalLink", oHospitalReg.HospitalLink);
                Pars.Add("@Status", oHospitalReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);

                var res = await DBQuery.ExeSPScaler<int>("SP_HospitalReg_Update", Pars);
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
