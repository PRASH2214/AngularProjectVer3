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
    public class DoctorService : IDoctor
    {
        public async Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@DoctorId", Id);
                Result.Status = await DBQuery.ExeQuery("Delete from DoctorReg Where  DoctorId=@DoctorId and CreatedById=@CreatedById", Pars) == 1 ? 1 : 2;
                if (Result.Status == 1)
                {
                    Pars = new Dapper.DynamicParameters();
                    Pars.Add("@UserTypeId", Constants.DOCTOR_USER);
                    Pars.Add("@DoctorId", Id);
                    await DBQuery.ExeQuery("Delete from UserLogin Where  ReferenceId=@DoctorId and UserTypeId=@UserTypeId", Pars);
                    Result.Message = Constants.DELETE_MESSAGE;
                }
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
                Pars.Add("@AdminId", oTokenModel.LoginId);
                Pars.Add("@DoctorId", Id);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from DoctorReg Where  DoctorId=@DoctorId", Pars);
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
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_DoctorReg_Select", Pars);
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

        public async Task<ResultModel<object>> Insert(TokenModel oTokenModel, DoctorReg oDoctorReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@FirstName", oDoctorReg.FirstName);
                Pars.Add("@MiddleName", oDoctorReg.MiddleName);
                Pars.Add("@LastName", oDoctorReg.LastName);
                Pars.Add("@HospitalId", oDoctorReg.HospitalId);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oDoctorReg.StateId);
                Pars.Add("@DistrictId", oDoctorReg.DistrictId);
                Pars.Add("@CityId", oDoctorReg.CityId);
                Pars.Add("@DoctorAddress", oDoctorReg.DoctorAddress);
                Pars.Add("@DoctorLicenseNumber", oDoctorReg.DoctorLicenseNumber);
                Pars.Add("@Mobile", oDoctorReg.Mobile);
                Pars.Add("@MedicalLicenseImage", oDoctorReg.MedicalLicenseImage);
                Pars.Add("@DOB", oDoctorReg.DOB);
                Pars.Add("@Age", oDoctorReg.Age);
                Pars.Add("@GenderId", oDoctorReg.GenderId);
                Pars.Add("@PinCode", oDoctorReg.PinCode);
                Pars.Add("@SpecialityId", oDoctorReg.SpecialityId);
                Pars.Add("@Status", oDoctorReg.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@BranchId", oDoctorReg.BranchId);
                Pars.Add("@DepartmentId", oDoctorReg.DepartmentId);
                Pars.Add("@EmailAddress", oDoctorReg.EmailAddress);
                var res = await DBQuery.ExeSPScaler<int>("SP_DoctorReg_Insert", Pars);
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
                {
                    oDoctorReg.DoctorId = res;
                    await InsertUserLogin(oDoctorReg);
                    Result.Message = Constants.CREATED_MESSAGE;
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

        public async Task<ResultModel<object>> Update(TokenModel oTokenModel, DoctorReg oDoctorReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DoctorId", oDoctorReg.DoctorId);
                Pars.Add("@FirstName", oDoctorReg.FirstName);
                Pars.Add("@MiddleName", oDoctorReg.MiddleName);
                Pars.Add("@LastName", oDoctorReg.LastName);
                Pars.Add("@HospitalId", oDoctorReg.HospitalId);
                Pars.Add("@CountryId", 1);
                Pars.Add("@StateId", oDoctorReg.StateId);
                Pars.Add("@DistrictId", oDoctorReg.DistrictId);
                Pars.Add("@CityId", oDoctorReg.CityId);
                Pars.Add("@DoctorAddress", oDoctorReg.DoctorAddress);
                Pars.Add("@DoctorLicenseNumber", oDoctorReg.DoctorLicenseNumber);
                Pars.Add("@Mobile", oDoctorReg.Mobile);
                Pars.Add("@MedicalLicenseImage", oDoctorReg.MedicalLicenseImage);
                Pars.Add("@DOB", oDoctorReg.DOB);
                Pars.Add("@Age", oDoctorReg.Age);
                Pars.Add("@GenderId", oDoctorReg.GenderId);
                Pars.Add("@PinCode", oDoctorReg.PinCode);
                Pars.Add("@SpecialityId", oDoctorReg.SpecialityId);
                Pars.Add("@Status", oDoctorReg.Status);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CreatedById", oTokenModel.LoginId);
                Pars.Add("@BranchId", oDoctorReg.BranchId);
                Pars.Add("@DepartmentId", oDoctorReg.DepartmentId);
                Pars.Add("@EmailAddress", oDoctorReg.EmailAddress);

                var res = await DBQuery.ExeSPScaler<int>("SP_DoctorReg_Update", Pars);
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
                {
                    await UpdateUserLogin(oDoctorReg);
                    Result.Message = Constants.UPDATED_MESSAGE;
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

        public async Task<ResultModel<object>> InsertUserLogin(DoctorReg oDoctorReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserTypeId", Constants.DOCTOR_USER);
                Pars.Add("@UserMobile", oDoctorReg.Mobile);
                Pars.Add("@Status", oDoctorReg.Status);
                Pars.Add("@ReferenceId", oDoctorReg.DoctorId);
                //   Pars.Add("@UserEmail", oDoctorReg.ma);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@OTP", 1234);
                Pars.Add("@UserEmail", oDoctorReg.EmailAddress);

                var res = await DBQuery.ExeSPScaler<int>("SP_UserLogin_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    Result.Message = Constants.CREATED_MESSAGE;
                    Result.Status = Constants.SUCCESS;
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

        public async Task<ResultModel<object>> UpdateUserLogin(DoctorReg oDoctorReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Status", oDoctorReg.Status);
                Pars.Add("@UserMobile", oDoctorReg.Mobile);
                Pars.Add("@ReferenceId", oDoctorReg.DoctorId);
                Pars.Add("@Otp", 0);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_UserLogin_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    Result.Message = Constants.CREATED_MESSAGE;
                    Result.Status = Constants.SUCCESS;
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
