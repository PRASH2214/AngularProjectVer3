using Cubix.BAL.Interfaces.Doctor;
using Cubix.DAL;
using Cubix.Models;
using Cubix.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Services.Doctor
{
    public class DoctorProfileService : IDoctorProfile
    {

        public async Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeSPScaler<object>("SP_Get_Doctor_DashBoard_Count", Pars);
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

        public async Task<ResultModel<object>> GetDoctor(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from DoctorReg Where status=1  and  DoctorId=@DoctorId ", Pars);
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

        public async Task<ResultModel<object>> UpdateDoctor(DoctorReg oDoctorReg, TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();

                Pars.Add("@DoctorId", oTokenModel.LoginId);
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
                Pars.Add("@CreatedById", oTokenModel.CreatedById);
                Pars.Add("@BranchId", oDoctorReg.BranchId);
                Pars.Add("@DepartmentId", oDoctorReg.DepartmentId);
                Pars.Add("@EmailAddress", oDoctorReg.EmailAddress);
                var res = await DBQuery.ExeSPScaler<int>("SP_DoctorReg_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
                    Result.Status = Constants.NOTUPDATED;
                }
                else
                {
                    Result.Message = Constants.UPDATED_MESSAGE;
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

        public async Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ImagePath", oFileUpload.ImagePath);
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Result.Status = await DBQuery.ExeQuery("update  DoctorReg set ProfileImagePath=@ImagePath Where status=1  and  DoctorId=@DoctorId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;
                if (Result.Status == Constants.SUCCESS)
                    Result.Message = oFileUpload.ImagePath;
                else
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
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


        public async Task<ResultModel<object>> GetSlots(TokenModel oTokenModel, DoctorSlotRequest oDoctorSlotRequest)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Pars.Add("@DepartmentId", oTokenModel.DepartmentId);
                Pars.Add("@DayId", oDoctorSlotRequest.DayId);
                Pars.Add("@SlotDate", oDoctorSlotRequest.SlotDate);
                Pars.Add("@ForUserTypeId", oDoctorSlotRequest.ForUserTypeId);
                Result.LstModel = await DBQuery.ExeSPList<object>("SP_DoctorSlotTime_Select", Pars);
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

        public async Task<ResultModel<object>> InsertDoctorSlotTime(TokenModel oTokenModel, List<DoctorSlotTime> oDoctorSlotTime)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DayId", oDoctorSlotTime[0].DayId);
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Pars.Add("@SlotDate", oDoctorSlotTime[0].SlotDate.ToString("yyyy-MM-dd 00:00:00"));
                Result.Status = await DBQuery.ExeQuery("Delete from DoctorSlotTime Where  DoctorId=@DoctorId and SlotDate=@SlotDate", Pars) == 1 ? 1 : 2;


                foreach (var item in oDoctorSlotTime)
                {
                    Pars.Add("@MasterSlotId", item.MasterSlotId);
                    Pars.Add("@SlotDepartmentTimeId", item.SlotDepartmentTimeId);
                    Pars.Add("@DoctorId", oTokenModel.LoginId);
                    Pars.Add("@DayId", item.DayId);
                    Pars.Add("@DayName", item.DayName);
                    Pars.Add("@SlotFromTime", item.SlotFromTime);
                    Pars.Add("@SlotEndTime", item.SlotEndTime);
                    Pars.Add("@SlotDate", oDoctorSlotTime[0].SlotDate.ToString("yyyy-MM-dd 00:00:00"));
                    Pars.Add("@Status", Constants.SUCCESS);
                    Pars.Add("@ForUserTypeId", item.ForUserTypeId);
                    Pars.Add("@NoOfPatientsAllowed", item.NoOfPatientsAllowed);
                    Pars.Add("@SlotTimePerPatient", item.SlotTimePerPatient);
                    Pars.Add("@NoOfPatientsBooked", item.NoOfPatientsBooked);
                    Pars.Add("@CreatedDate", DateTime.Now);
                    Pars.Add("@CreatedById", oTokenModel.LoginId);
                    Pars.Add("@ModifiedDate", DateTime.Now);


                    var res = await DBQuery.ExeSPScaler<int>("SP_DoctorSlotTime_Insert", Pars);
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
