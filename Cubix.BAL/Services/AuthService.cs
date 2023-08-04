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
    public class AuthService : IAuth
    {

        public async Task<ResultModel<object>> CheckUserExists(LoginModel ByUser, int UserTypeId)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", ByUser.UserName);
                Pars.Add("@UserTypeId", UserTypeId);
                UserLogin oUserLogin = await DBQuery.ExeScalarQuery<UserLogin>("Select  UserMobile from UserLogin Where status=1 and   UserMobile =@UserMobile and UserTypeId=@UserTypeId", Pars);

                if (oUserLogin == null) //If Wrong Username
                {
                    Result.Status = Constants.INVALID;
                    Result.Message = Constants.NOTUSEREXISTS_MESSAGE;
                    return Result;
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
        #region Super Admin
        public async Task<ResultModel<SuperAdminReg>> VerifySuperAdminLogin(LoginModel ByUser)
        {

            ResultModel<SuperAdminReg> Result = new ResultModel<SuperAdminReg>();
            try
            {

                // Secure.DecryptCipherTextToPlainText(oUser.Password));
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", ByUser.UserName);
                Pars.Add("@UserTypeId", Constants.SUPERADMIN_USER);
                UserLogin oUserLogin = await DBQuery.ExeScalarQuery<UserLogin>("Select  * from UserLogin Where  status=1 and UserTypeId=@UserTypeId and   UserMobile =@UserMobile", Pars);

                if (oUserLogin == null) //If Wrong Username/OTP
                {
                    Result.Status = Constants.INVALID;
                    Result.Message = Constants.NOTMATCHED_OPT;
                    return Result;
                }
                else // If User Authenticated
                {
                    Result.Model = await DBQuery.ExeScalarQuery<SuperAdminReg>("Select  * from SuperAdminReg Where  SuperAdminId = " + oUserLogin.ReferenceId, Pars);
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
        #endregion

        #region Admin

        public async Task<ResultModel<AdminReg>> VerifyAdminLogin(LoginModel ByUser)
        {

            ResultModel<AdminReg> Result = new ResultModel<AdminReg>();
            try
            {

                // Secure.DecryptCipherTextToPlainText(oUser.Password));
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", ByUser.UserName);
                Pars.Add("@UserTypeId", Constants.ADMIN_USER);
                // Pars.Add("@Password", Secure.EncryptPlainTextToCipherText(ByUser.Password));
                UserLogin oUserLogin = await DBQuery.ExeScalarQuery<UserLogin>("Select  * from UserLogin Where status=1 and   UserMobile =@UserMobile and UserTypeId=@UserTypeId", Pars);

                if (oUserLogin == null) //If Wrong Username/OTP
                {
                    Result.Status = Constants.INVALID;
                    Result.Message = Constants.NOTMATCHED_OPT;
                    return Result;
                }
                else // If User Authenticated
                {
                    Result.Model = await DBQuery.ExeScalarQuery<AdminReg>("Select  * from AdminReg Where  AdminId = " + oUserLogin.ReferenceId, Pars);
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
        #endregion




        #region Doctor

        public async Task<ResultModel<DoctorReg>> VerifyDoctorLogin(LoginModel ByUser)
        {

            ResultModel<DoctorReg> Result = new ResultModel<DoctorReg>();
            try
            {

                // Secure.DecryptCipherTextToPlainText(oUser.Password));
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", ByUser.UserName);
                Pars.Add("@UserTypeId", Constants.DOCTOR_USER);
                // Pars.Add("@Password", Secure.EncryptPlainTextToCipherText(ByUser.Password));
                UserLogin oUserLogin = await DBQuery.ExeScalarQuery<UserLogin>("Select  * from UserLogin Where status=1 and   UserMobile =@UserMobile and UserTypeId=@UserTypeId", Pars);

                if (oUserLogin == null) //If Wrong Username/OTP
                {
                    Result.Status = Constants.INVALID;
                    Result.Message = Constants.NOTMATCHED_OPT;
                    return Result;
                }
                else // If User Authenticated
                {
                    Result.Model = await DBQuery.ExeScalarQuery<DoctorReg>("Select  * from DoctorReg Where   DoctorId = " + oUserLogin.ReferenceId, Pars);
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
        #endregion



        #region MR

        public async Task<ResultModel<MRReg>> VerifyMRLogin(LoginModel ByUser)
        {

            ResultModel<MRReg> Result = new ResultModel<MRReg>();
            try
            {

                // Secure.DecryptCipherTextToPlainText(oUser.Password));
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", ByUser.UserName);
                Pars.Add("@UserTypeId", Constants.MR_USER);
                // Pars.Add("@Password", Secure.EncryptPlainTextToCipherText(ByUser.Password));
                UserLogin oUserLogin = await DBQuery.ExeScalarQuery<UserLogin>("Select  * from UserLogin Where status=1 and   UserMobile =@UserMobile and UserTypeId=@UserTypeId", Pars);

                if (oUserLogin == null) //If Wrong Username/OTP
                {
                    Result.Status = Constants.INVALID;
                    Result.Message = Constants.NOTMATCHED_OPT;
                    return Result;
                }
                else // If User Authenticated
                {
                    Result.Model = await DBQuery.ExeScalarQuery<MRReg>("Select  * from MRReg Where   MRId = " + oUserLogin.ReferenceId, Pars);
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
        #endregion


        #region Patient Profile
        public async Task<ResultModel<object>> CheckPatientProfileExist(PatientLoginModel oPatientLoginModel)
        {

            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Mobile", oPatientLoginModel.UserName);
                List<object> lstPatientReg = await DBQuery.ExeQueryList<object>("Select  RelationId,FirstName,LastName from PatientReg Where (Mobile =@Mobile or PatientReferenceNumber=@Mobile)", Pars);
                if (lstPatientReg == null || lstPatientReg.Count == 0) //If Wrong Username/OTP
                {
                    Result.Status = Constants.PATIENT_NOT_EXIST;
                    Result.Message = Constants.NOTUSEREXISTS_MESSAGE;
                    return Result;
                }
                else
                {

                    Result.Status = Constants.SUCCESS;
                    Result.Message = Constants.SUCCESS_MESSAGE;
                    Result.LstModel = lstPatientReg;
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
        public async Task<ResultModel<PatientReg>> VerifyPatientProfileLogin(PatientLoginModel oPatientLoginModel)
        {

            ResultModel<PatientReg> Result = new ResultModel<PatientReg>();
            try
            {

                // Secure.DecryptCipherTextToPlainText(oUser.Password));
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", oPatientLoginModel.UserName);
                Pars.Add("@RelationId", oPatientLoginModel.RelationId);
                // Pars.Add("@Password", Secure.EncryptPlainTextToCipherText(ByUser.Password));
                Result.Model = await DBQuery.ExeScalarQuery<PatientReg>("Select  * from PatientReg Where  Mobile =@UserMobile and RelationId=@RelationId", Pars);

                if (Result.Model == null) //If Wrong Username/OTP
                {
                    Result.Status = Constants.INVALID;
                    Result.Message = Constants.NOTMATCHED_OPT;
                    return Result;
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
        #endregion
        #region Patient

        public async Task<ResultModel<PatientReg>> CheckPatientExists(PatientLoginModel oPatientLoginModel)
        {
            ResultModel<PatientReg> Result = new ResultModel<PatientReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", oPatientLoginModel.UserName);
                PatientReg oUserLogin = await DBQuery.ExeScalarQuery<PatientReg>("Select  * from PatientReg Where  Mobile =@UserMobile ", Pars);

                if (oUserLogin == null) //If Wrong Username
                {
                    Result.Model = new PatientReg();
                    // Result.Status = Constants.INVALID;
                    //  Result.Message = Constants.NOTUSEREXISTS_MESSAGE;
                    return Result;
                }
                Result.Model = oUserLogin;
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

        public async Task<ResultModel<PatientTeleConsultationReg>> PatientLogin(PatientLoginModel oPatientLoginModel)
        {


            ResultModel<PatientTeleConsultationReg> Result = new ResultModel<PatientTeleConsultationReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserName", oPatientLoginModel.UserName);
                Pars.Add("@Type", Constants.ONLINE_CONSULTATION);
                Pars.Add("@ConsultationReferenceNumber", oPatientLoginModel.ConsultationReferenceNumber);
                Pars.Add("@Status", Constants.PAYMENT_SUCCESS);
                PatientTeleConsultationReg oPatientTeleConsultationReg = DBQuery.ExeScalarQuery<PatientTeleConsultationReg>("Select  * from PatientTeleConsultationReg Where status=@Status and  ConsultationReferenceNumber =@ConsultationReferenceNumber and Mobile=@UserName and Type=@Type", Pars).Result;

                if (oPatientTeleConsultationReg == null) //If No Consultation Exists
                {
                    Result.Status = Constants.CONSULTATION_NOT_EXIST;
                    Result.Message = Constants.CONSULTATION_NOT_EXIST_MESSAGE;
                    return Result;
                }
                else if (oPatientTeleConsultationReg != null && oPatientTeleConsultationReg.AppointmentDate.Date != DateTime.Now.Date)//If Consultation belongs to other day
                {
                    Result.Status = Constants.CONSULTATION_NOT_EXIST;
                    Result.Message = Constants.CONSULTATION_NOT_TODAY_EXIST_MESSAGE;
                    return Result;
                }
                else if (oPatientTeleConsultationReg != null && (oPatientTeleConsultationReg.SlotFromTime.TotalMinutes > DateTime.Now.TimeOfDay.TotalMinutes))// If slot time is in future
                {
                    Result.Status = Constants.FUTURE_SLOT_TIME;
                    Result.Message = Constants.FUTURE_SLOT_TIME_MESSAGE;
                    return Result;
                }
                else if (oPatientTeleConsultationReg != null && (oPatientTeleConsultationReg.SlotEndTime.TotalMinutes < DateTime.Now.TimeOfDay.TotalMinutes))// If slot  is missed
                {
                    Result.Status = Constants.MISS_SLOT_TIME;
                    Result.Message = Constants.MISS_SLOT_TIME_MESSAGE;
                    return Result;
                }
                else
                {
                    Result.Status = Constants.SUCCESS;
                    Result.Message = Constants.SUCCESS_MESSAGE;
                    Result.Model = oPatientTeleConsultationReg;
                    return Result;
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




        public async Task<ResultModel<PatientTeleConsultationReg>> CheckPendingConsultation(PatientLoginModel oPatientLoginModel)
        {

            ResultModel<PatientTeleConsultationReg> Result = new ResultModel<PatientTeleConsultationReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@UserMobile", oPatientLoginModel.UserName);
                //Pars.Add("@ConsultationConnected", Constants.CALL_CONNECTED);
                // Pars.Add("@PaymentSuccess", Constants.PAYMENT_SUCCESS);
                PatientTeleConsultationReg oPatientReg = await DBQuery.ExeScalarQuery<PatientTeleConsultationReg>("Select  top 1 * from PatientTeleConsultationReg Where  Mobile =@UserMobile and  CAST( AppointmentDate AS Date ) >= CAST( GETDATE() AS Date ) order by PatientTeleConsultationId desc", Pars);
                if (oPatientReg != null && oPatientReg.Status == Constants.PAYMENT_PENDING && oPatientReg.PaymentMode == "OFFLINE") //If Offline consultation already Exists
                {
                    Result.Model = oPatientReg;
                    Result.Status = Constants.CONSULTATION_ALREADY_EXIST;
                    Result.Message = Constants.CONSULTATION_OFFLINE_ALREADY_EXIST_MESSAGE;
                    return Result;
                }
                else if (oPatientReg != null && oPatientReg.Status == Constants.PAYMENT_SUCCESS) //If consultation already Exists
                {
                    Result.Model = oPatientReg;
                    Result.Status = Constants.CONSULTATION_ALREADY_EXIST;
                    Result.Message = Constants.CONSULTATION_ALREADY_EXIST_MESSAGE;
                    return Result;
                }
                else if (oPatientReg != null && oPatientReg.Status == Constants.PAYMENT_PENDING) //If Payment Pending
                {
                    Result.Model = oPatientReg;
                    Result.Status = Constants.PAYMENT_IN_PROCESS;
                    Result.Message = oPatientReg.PaymentLink;
                    return Result;
                }
                else // If Patient already exists
                {
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

        public async Task<ResultModel<ResultPatientReg>> NewPatientRegistration(PatientReg oPatientReg)
        {
            ResultModel<ResultPatientReg> Result = new ResultModel<ResultPatientReg>();
            try
            {
                /////////////////////////////////////////////
                // Generate PatientId/CRNumber with the help of luhn mod 10 Algorithm
                /////////////////////////////////////////////  
                oPatientReg.PatientReferenceNumber = Helper.GenerateCRNumber(oPatientReg);

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientReferenceNumber", oPatientReg.PatientReferenceNumber);
                Pars.Add("@FirstName", oPatientReg.FirstName);
                Pars.Add("@LastName", oPatientReg.LastName);
                Pars.Add("@CountryId", oPatientReg.CountryId);
                Pars.Add("@StateId", oPatientReg.StateId);
                Pars.Add("@DistrictId", oPatientReg.DistrictId);
                Pars.Add("@CityId", oPatientReg.CityId);
                Pars.Add("@PatientAddress", oPatientReg.PatientAddress);
                Pars.Add("@Mobile", oPatientReg.Mobile);
                Pars.Add("@DOB", oPatientReg.DOB);
                Pars.Add("@Age", oPatientReg.Age);
                Pars.Add("@PinCode", oPatientReg.PinCode);
                Pars.Add("@Status", oPatientReg.Status);
                Pars.Add("@GenderId", oPatientReg.GenderId);
                Pars.Add("@EmailAddress", oPatientReg.EmailAddress);
                Pars.Add("@RelationId", oPatientReg.RelationId);
                Pars.Add("@RelationName", oPatientReg.RelationName);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);

                int res = await DBQuery.ExeSPScaler<int>("SP_PatientReg_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    Result.Model = new ResultPatientReg();
                    Result.Model.PatientId = res;
                    Result.Model.PatientReferenceNumber = oPatientReg.PatientReferenceNumber;
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

        public async Task<ResultModel<ResultPatientReg>> ReVisitPatientRegistration(PatientReg oPatientReg)
        {
            ResultModel<ResultPatientReg> Result = new ResultModel<ResultPatientReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientId", oPatientReg.PatientId);
                Pars.Add("@PatientReferenceNumber", oPatientReg.PatientReferenceNumber);
                Pars.Add("@FirstName", oPatientReg.FirstName);
                Pars.Add("@LastName", oPatientReg.LastName);
                Pars.Add("@CountryId", oPatientReg.CountryId);
                Pars.Add("@StateId", oPatientReg.StateId);
                Pars.Add("@DistrictId", oPatientReg.DistrictId);
                Pars.Add("@CityId", oPatientReg.CityId);
                Pars.Add("@PatientAddress", oPatientReg.PatientAddress);
                Pars.Add("@DOB", oPatientReg.DOB);
                Pars.Add("@Age", oPatientReg.Age);
                Pars.Add("@PinCode", oPatientReg.PinCode);
                Pars.Add("@Status", oPatientReg.Status);
                Pars.Add("@GenderId", oPatientReg.GenderId);
                Pars.Add("@EmailAddress", oPatientReg.EmailAddress);
                Pars.Add("@RelationId", oPatientReg.RelationId);
                Pars.Add("@RelationName", oPatientReg.RelationName);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_PatientReg_Update", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTUPDATED_MESSAGE;
                    Result.Status = Constants.NOTUPDATED;
                }
                else
                {
                    Result.Model = new ResultPatientReg();
                    Result.Model.PatientId = res;
                    Result.Model.PatientReferenceNumber = oPatientReg.PatientReferenceNumber;
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


        public async Task<ResultModel<ResultPatientReg>> InsertPatientTeleConsultationReg(PatientTeleConsultationReg oPatientTeleConsultationReg)
        {
            ResultModel<ResultPatientReg> Result = new ResultModel<ResultPatientReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Mobile", oPatientTeleConsultationReg.Mobile);
                await DBQuery.ExeQuery("delete  from PatientTeleConsultationReg where status=1 and mobile=@Mobile", Pars);
                Pars.Add("@PatientId", oPatientTeleConsultationReg.PatientId);
                Pars.Add("@PatientReferenceNumber", oPatientTeleConsultationReg.PatientReferenceNumber);
                Pars.Add("@ConsultationReferenceNumber", oPatientTeleConsultationReg.ConsultationReferenceNumber);
                Pars.Add("@HospitalId", oPatientTeleConsultationReg.HospitalId);
                Pars.Add("@BranchId", oPatientTeleConsultationReg.BranchId);
                Pars.Add("@DepartmentId", oPatientTeleConsultationReg.DepartmentId);
                Pars.Add("@DoctorId", oPatientTeleConsultationReg.DoctorId);
                Pars.Add("@DoctorSlotTimeId", oPatientTeleConsultationReg.DoctorSlotTimeId);
                Pars.Add("@AppointmentDate", oPatientTeleConsultationReg.AppointmentDate);
                Pars.Add("@SlotFromTime", oPatientTeleConsultationReg.SlotFromTime);
                Pars.Add("@SlotEndTime", oPatientTeleConsultationReg.SlotEndTime);
                Pars.Add("@PaymentAmmount", oPatientTeleConsultationReg.PaymentAmmount);
                Pars.Add("@PaymentMode", oPatientTeleConsultationReg.PaymentMode);
                Pars.Add("@Concern", oPatientTeleConsultationReg.Concern);
                Pars.Add("@Type", oPatientTeleConsultationReg.Type);
                Pars.Add("@Status", Constants.PAYMENT_PENDING);
                Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_PENDING);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);

                //if (oPatientTeleConsultationReg.Type == Constants.ONLINE_CONSULTATION)//if online Appointment
                //{
                //    Pars.Add("@Status", Constants.PAYMENT_SUCCESS);
                //}

                var res = await DBQuery.ExeSPScaler<int>("SP_PatientTeleConsultationReg_Insert", Pars);
                if (res == 0)
                {
                    Result.Message = Constants.NOTCREATED_MESSAGE;
                    Result.Status = Constants.NOTCREATED;
                }
                else
                {
                    //update the Booked Slots Number 
                    await DBQuery.ExeQuery("update DoctorSlotTime set NoOfPatientsBooked=NoOfPatientsBooked+1 where ForUserTypeId=" + Constants.PATIENT_USER + " and  DoctorSlotTimeId=" + oPatientTeleConsultationReg.DoctorSlotTimeId, Pars);
                    Result.Model = new ResultPatientReg();
                    Result.Model.PatientTeleConsultationId = res;
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


        public async Task<ResultModel<object>> InsertPatientDocumentReg(PatientDocumentReg oPatientDocumentReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientId", oPatientDocumentReg.PatientId);
                Pars.Add("@PatientReferenceNumber", oPatientDocumentReg.PatientReferenceNumber);
                Pars.Add("@ConsultationReferenceNumber", oPatientDocumentReg.ConsultationReferenceNumber);
                Pars.Add("@PatientTeleConsultationId", oPatientDocumentReg.PatientTeleConsultationId);
                Pars.Add("@FileName", oPatientDocumentReg.FileName);
                Pars.Add("@FilePath", oPatientDocumentReg.FilePath);
                Pars.Add("@FileType", oPatientDocumentReg.FileType);
                Pars.Add("@Status", oPatientDocumentReg.Status);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ModifiedDate", DateTime.Now);

                var res = await DBQuery.ExeSPScaler<int>("SP_PatientDocumentReg_Insert", Pars);
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


        public async Task<string> InsertConsultationReferenceNumber(long PatientTeleConsultationId, int StateId, int DistrictId, int CityId)
        {
            string Result = "";
            try
            {
                string ConsultationReferenceNumber = Helper.GenerateTeleConsultationNumber(PatientTeleConsultationId, StateId, DistrictId, CityId);
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", PatientTeleConsultationId);
                Pars.Add("@ConsultationReferenceNumber", ConsultationReferenceNumber);

                Result = ConsultationReferenceNumber;
                var res = await DBQuery.ExeScalarQuery<int>("update PatientTeleConsultationReg set ConsultationReferenceNumber=@ConsultationReferenceNumber where PatientTeleConsultationId=@PatientTeleConsultationId", Pars);

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }


        public async Task<ResultModel<object>> GetDoctorSlots(DoctorSlotRequest oDoctorSlotRequest, int ForUserTypeId)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DoctorId", oDoctorSlotRequest.DoctorId);
                Pars.Add("@SlotDate", oDoctorSlotRequest.SlotDate.ToString("yyyy-MM-dd 00:00:00"));
                Pars.Add("@ForUserTypeId", ForUserTypeId);
                Result.LstModel = await DBQuery.ExeQueryList<object>(" select* from DoctorSlotTime where ForUserTypeId=@ForUserTypeId and SlotDate = @SlotDate and DoctorId = @DoctorId and  NoOfPatientsAllowed>NoOfPatientsBooked and (CONVERT (date, SlotDate)>CONVERT (date, CURRENT_TIMESTAMP) or (CONVERT(date, SlotDate)=CONVERT (date, CURRENT_TIMESTAMP) and  SlotFromTime>CONVERT (TIME, CURRENT_TIMESTAMP))) order by SlotFromTime", Pars);
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

        public async Task<ResultModel<PatientTeleConsultationReg>> UpdateTeleConsultationStatus(string ConsultationReferenceNumber, int Status)
        {
            ResultModel<PatientTeleConsultationReg> Result = new ResultModel<PatientTeleConsultationReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationReferenceNumber", ConsultationReferenceNumber);
                Pars.Add("@Status", Status);
                var res = await DBQuery.ExeQuery("update PatientTeleConsultationReg set status=@Status  where  ConsultationReferenceNumber=@ConsultationReferenceNumber", Pars);
                //    if (res > 0)
                {
                    PatientTeleConsultationReg oPatientReg = await DBQuery.ExeScalarQuery<PatientTeleConsultationReg>("Select  * from PatientTeleConsultationReg  where  ConsultationReferenceNumber=@ConsultationReferenceNumber", Pars);

                    if (oPatientReg != null) //If Patient not exists
                    {
                        Result.Status = Constants.SUCCESS;
                        Result.Message = Constants.SUCCESS_MESSAGE;
                        Result.Model = oPatientReg;
                        return Result;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }
        public async Task<ResultModel<PatientTeleConsultationReg>> UpdatePaymentStatusBeforeRegistration(string ConsultationReferenceNumber, int Status, string PaymentReferenceId, string PaymentMode)
        {
            ResultModel<PatientTeleConsultationReg> Result = new ResultModel<PatientTeleConsultationReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationReferenceNumber", ConsultationReferenceNumber);
                Pars.Add("@Status", Status);
                Pars.Add("@PaymentReferenceId", PaymentReferenceId);
                Pars.Add("@PaymentMode", PaymentMode);

                Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_PENDING);
                if (Status == Constants.PAYMENT_SUCCESS)
                    Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_COMPLETED);

                var res = await DBQuery.ExeQuery("update PatientTeleConsultationReg set status=@Status,PaymentReferenceId=@PaymentReferenceId,PaymentMode=@PaymentMode,ConsultationsStatus=@ConsultationsStatus  where  ConsultationReferenceNumber=@ConsultationReferenceNumber", Pars);
                //    if (res > 0)

                Result.Status = Constants.SUCCESS;
                Result.Message = Constants.SUCCESS_MESSAGE;
                return Result;


            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }

        public async Task<ResultModel<PatientTeleConsultationReg>> UpdateTeleConsultationAfterPayment(string ConsultationReferenceNumber, int Status, string PaymentReferenceId, string PaymentMode)
        {
            ResultModel<PatientTeleConsultationReg> Result = new ResultModel<PatientTeleConsultationReg>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationReferenceNumber", ConsultationReferenceNumber);
                Pars.Add("@Status", Status);
                Pars.Add("@PaymentReferenceId", PaymentReferenceId);
                Pars.Add("@PaymentMode", PaymentMode);

                Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_PENDING);
                //if (Status == Constants.PAYMENT_SUCCESS)
                //    Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_COMPLETED);

                var res = await DBQuery.ExeQuery("update PatientTeleConsultationReg set status=@Status,PaymentReferenceId=@PaymentReferenceId,PaymentMode=@PaymentMode,ConsultationsStatus=@ConsultationsStatus  where  ConsultationReferenceNumber=@ConsultationReferenceNumber", Pars);
                //    if (res > 0)
                {
                    PatientTeleConsultationReg oPatientReg = await DBQuery.ExeScalarQuery<PatientTeleConsultationReg>("Select  * from PatientTeleConsultationReg  where  ConsultationReferenceNumber=@ConsultationReferenceNumber", Pars);

                    if (oPatientReg != null) //If Patient not exists
                    {
                        Result.Status = Constants.SUCCESS;
                        Result.Message = Constants.SUCCESS_MESSAGE;
                        Result.Model = oPatientReg;
                        return Result;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }
        #endregion


        #region Masters
        public async Task<ResultModel<object>> GetActiveHospitals()
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  HospitalId,HospitalName from HospitalReg  where status=1");
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

        public async Task<ResultModel<object>> GetActiveBranchByHospital(long HospitalId)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  BranchId,BranchName from BranchReg  where status=1 and HospitalId=" + HospitalId);
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

        public async Task<ResultModel<object>> GetActiveDepartmentByBranch(long BranchId)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  DepartmentId,DepartmentName,Amount from DepartmentReg  where status=1 and BranchId=" + BranchId);
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

        public async Task<ResultModel<object>> GetActiveDoctorByDepartment(long DepartmentId)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  DoctorId,FirstName,MiddleName,LastName from DoctorReg  where status=1 and DepartmentId=" + DepartmentId);
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

        public async Task<string> UpdatePaymentPaymentLink(string ConsultationReferenceNumber, string PaymentLink)
        {
            string Result = "";
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PaymentLink", PaymentLink);
                Pars.Add("@ConsultationReferenceNumber", ConsultationReferenceNumber);
                var res = await DBQuery.ExeScalarQuery<int>("update PatientTeleConsultationReg set PaymentLink=@PaymentLink where ConsultationReferenceNumber=@ConsultationReferenceNumber", Pars);

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }

        public async Task<ResultModel<object>> DiscardConsultation(RefundRequest oRefundRequest)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationReferenceNumber", oRefundRequest.ConsultationReferenceNumber);
                Pars.Add("@Status", Constants.CONSULTATION_PENDING);
                var res = await DBQuery.ExeQuery("delete from  PatientTeleConsultationReg where  ConsultationReferenceNumber=@ConsultationReferenceNumber and  status=@Status", Pars);
                Result.Status = Constants.SUCCESS;
                Result.Message = Constants.SUCCESS_MESSAGE;
                return Result;
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
            }
            return Result;
        }


        public async Task<float> GetAmountByDepartment(long DepartmentId)
        {
            float Result = 0;
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DepartmentId", DepartmentId);
                DepartmentReg oDepartmentReg = await DBQuery.ExeScalarQuery<DepartmentReg>("select Amount from DepartmentReg where DepartmentId=@DepartmentId ", Pars);

                if (oDepartmentReg != null) //If Wrong Username
                {
                    Result = (float)oDepartmentReg.Amount;

                    return Result;
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                Result = 0;
            }
            return Result;
        }
        #endregion
    }
}
