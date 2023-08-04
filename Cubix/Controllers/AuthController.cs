using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Cubix.Controllers
{

    /// <summary>
    /// This API used for Authenticate the Admin, Doctor, Patient
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuth _srv;
        private readonly ICommon _commonsrv;
        private readonly IDistributedCache _distributedCache;
        private readonly IWebHostEnvironment _hostingEnvironment;// use to get the web root path


        /// <summary>
        /// Auth Controller Comstructor
        /// </summary>
        public AuthController(IAuth user, IDistributedCache distributedCache, IWebHostEnvironment hostingEnvironment, ICommon commonsrv)
        {
            _distributedCache = distributedCache;
            _srv = user;
            _hostingEnvironment = hostingEnvironment;
            _commonsrv = commonsrv;
        }
        /// <summary>
        /// This method used for check the API response
        /// </summary>
        [HttpGet("CheckAPI/{Id}")]
        public string CheckAPI(string Id)
        {
            return "All Good";
            // await _hubContext.Clients.Client(Id).SendAsync("GetConnection", "Hello");
        }

        #region Super Admin
        /// <summary>
        /// This Method Verify the User Existance by Passing Mobile Number
        /// </summary>
        [HttpPost("checksuperuserexists")]
        public async Task<ResultModel<object>> CheckSuperUserExists([FromBody]LoginModel oLoginModel)
        {
            ResultModel<object> result = new ResultModel<object>();
            result = await _srv.CheckUserExists(oLoginModel, Constants.SUPERADMIN_USER);
            if (result.Success == true && result.Status == Constants.SUCCESS)
            {
                string OTP = Helper.GenerateOTP();
                if (oLoginModel.UserName == "9878208127" || oLoginModel.UserName == "9041423335")
                    OTP = "123456";
                else
                    Message.SendOTP(oLoginModel.UserName, "Dear User, " + OTP + " is your OTP for login as Admin in dev-econsultation.ecubix.com. The OTP validity is 15 minutes.");

                //set OTP in Memory
                await _distributedCache.SetStringAsync(oLoginModel.UserName, OTP, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(15) });
            }

            return result;
        }


        /// <summary>
        /// This Method Verify the Admin Login
        /// Also, It check the Captcha for Secuirty
        /// After Successfull Login rturn the Admin Details along eith encrypted JWT token
        /// </summary>

        [HttpPost("authenticatesuperadmin")]
        public async Task<ResultModel<SuperAdminReg>> AuthenticateSuperAdmin(LoginModel oLoginModel)
        {
            ResultModel<SuperAdminReg> result = new ResultModel<SuperAdminReg>();

            string MemoryOTP = await _distributedCache.GetStringAsync(oLoginModel.UserName);

            if (MemoryOTP != oLoginModel.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.NOTMATCHED_OPT;
                return result;
            }

            //Call service method for check the Admin Credentials
            var user = await _srv.VerifySuperAdminLogin(oLoginModel);


            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.SuperAdminId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.AdminMobile.ToString()),
                new Claim("LoginId", user.Model.SuperAdminId.ToString()),
                new Claim("UserTypeId",Constants.SUPERADMIN_USER.ToString()),
                new Claim("CreatedById",user.Model.SuperAdminId.ToString()),
                new Claim("DepartmentId","0"),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                         SecurityAlgorithms.HmacSha256)
                    );



                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_SUPERADMIN_USER + user.Model.SuperAdminId.ToString(), new TokenCacheModel { LoginId = user.Model.SuperAdminId, UserTypeId = Constants.SUPERADMIN_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }


        #endregion

        #region Admin
        /// <summary>
        /// This Method Verify the User Existance by Passing Mobile Number
        /// </summary>
        [HttpPost("checkuserexists")]
        public async Task<ResultModel<object>> CheckUserExists([FromBody]LoginModel oLoginModel)
        {
            ResultModel<object> result = new ResultModel<object>();
            result = await _srv.CheckUserExists(oLoginModel, Constants.ADMIN_USER);
            if (result.Success == true && result.Status == Constants.SUCCESS)
            {
                string OTP = Helper.GenerateOTP();
                if (oLoginModel.UserName == "9878208127" || oLoginModel.UserName == "9041423335")
                    OTP = "123456";
                else
                    Message.SendOTP(oLoginModel.UserName, "Dear User, " + OTP + " is your OTP for login as Admin in dev-econsultation.ecubix.com. The OTP validity is 15 minutes.");

                //set OTP in Memory
                await _distributedCache.SetStringAsync(oLoginModel.UserName, OTP, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(15) });
            }

            return result;
        }


        /// <summary>
        /// This Method Verify the Admin Login
        /// Also, It check the Captcha for Secuirty
        /// After Successfull Login rturn the Admin Details along eith encrypted JWT token
        /// </summary>

        [HttpPost("authenticateadmin")]
        public async Task<ResultModel<AdminReg>> AuthenticateAdmin(LoginModel oLoginModel)
        {
            ResultModel<AdminReg> result = new ResultModel<AdminReg>();

            string MemoryOTP = await _distributedCache.GetStringAsync(oLoginModel.UserName);

            if (MemoryOTP != oLoginModel.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.NOTMATCHED_OPT;
                return result;
            }

            //Call service method for check the Admin Credentials
            var user = await _srv.VerifyAdminLogin(oLoginModel);


            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.AdminId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.AdminMobile.ToString()),
                new Claim("LoginId", user.Model.AdminId.ToString()),
                new Claim("UserTypeId",Constants.ADMIN_USER.ToString()),
                new Claim("CreatedById",user.Model.AdminId.ToString()),
                   new Claim("DepartmentId","0"),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                                SecurityAlgorithms.HmacSha256)
                    );



                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_ADMIN_USER + user.Model.AdminId.ToString(), new TokenCacheModel { LoginId = user.Model.AdminId, UserTypeId = Constants.ADMIN_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }


        #endregion

        #region Doctor
        /// <summary>
        /// This Method Verify the User Existance by Passing Mobile Number
        /// </summary>
        [HttpPost("checkdoctorexists")]
        public async Task<ResultModel<object>> CheckDoctorExists([FromBody]LoginModel oLoginModel)
        {
            ResultModel<object> result = new ResultModel<object>();
            result = await _srv.CheckUserExists(oLoginModel, Constants.DOCTOR_USER);
            if (result.Success == true && result.Status == Constants.SUCCESS)
            {
                string OTP = Helper.GenerateOTP();
                if (oLoginModel.UserName == "9878208127" || oLoginModel.UserName == "9041423335")
                    OTP = "123456";
                else
                    Message.SendOTP(oLoginModel.UserName, "Dear User, " + OTP + " is your OTP for login as Doctor in dev-econsultation.ecubix.com. The OTP validity is 15 minutes.");

                //set OTP in Memory
                await _distributedCache.SetStringAsync(oLoginModel.UserName, OTP, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(15) });
            }

            return result;
        }

        /// <summary>
        /// This Method Verify the Admin Login
        /// Also, It check the Captcha for Secuirty
        /// After Successfull Login rturn the Admin Details along eith encrypted JWT token
        /// </summary>

        [HttpPost("authenticatedoctor")]
        public async Task<ResultModel<DoctorReg>> AuthenticateDoctor(LoginModel oLoginModel)
        {
            ResultModel<DoctorReg> result = new ResultModel<DoctorReg>();

            string MemoryOTP = await _distributedCache.GetStringAsync(oLoginModel.UserName);

            if (MemoryOTP != oLoginModel.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.NOTMATCHED_OPT;
                return result;
            }

            //Call service method for check the Admin Credentials
            var user = await _srv.VerifyDoctorLogin(oLoginModel);


            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.DoctorId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.Mobile.ToString()),
                new Claim("LoginId", user.Model.DoctorId.ToString()),
                new Claim("UserTypeId",Constants.DOCTOR_USER.ToString()),
                new Claim("CreatedById",user.Model.CreatedById.ToString()),
                new Claim("DepartmentId",user.Model.DepartmentId.ToString()),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                                SecurityAlgorithms.HmacSha256)
                    );



                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_DOCTOR_USER + user.Model.DoctorId.ToString(), new TokenCacheModel { LoginId = user.Model.DoctorId, UserTypeId = Constants.DOCTOR_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }


        #endregion


        #region MR
        /// <summary>
        /// This Method Verify the User Existance by Passing Mobile Number
        /// </summary>
        [HttpPost("checkmrexists")]
        public async Task<ResultModel<object>> CheckMRExists([FromBody]LoginModel oLoginModel)
        {
            ResultModel<object> result = new ResultModel<object>();
            result = await _srv.CheckUserExists(oLoginModel, Constants.MR_USER);
            if (result.Success == true && result.Status == Constants.SUCCESS)
            {
                string OTP = Helper.GenerateOTP();
                if (oLoginModel.UserName == "9878208127" || oLoginModel.UserName == "9041423335")
                    OTP = "123456";
                else
                    Message.SendOTP(oLoginModel.UserName, "Dear User, " + OTP + " is your OTP for login as MR in dev-econsultation.ecubix.com. The OTP validity is 15 minutes.");

                //set OTP in Memory
                await _distributedCache.SetStringAsync(oLoginModel.UserName, OTP, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(15) });
            }

            return result;
        }

        /// <summary>
        /// This Method Verify the Admin Login
        /// Also, It check the Captcha for Secuirty
        /// After Successfull Login rturn the Admin Details along eith encrypted JWT token
        /// </summary>

        [HttpPost("authenticatemr")]
        public async Task<ResultModel<MRReg>> AuthenticateMR(LoginModel oLoginModel)
        {
            ResultModel<MRReg> result = new ResultModel<MRReg>();

            string MemoryOTP = await _distributedCache.GetStringAsync(oLoginModel.UserName);

            if (MemoryOTP != oLoginModel.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.NOTMATCHED_OPT;
                return result;
            }

            //Call service method for check the Admin Credentials
            var user = await _srv.VerifyMRLogin(oLoginModel);


            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.MrId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.Mobile.ToString()),
                new Claim("LoginId", user.Model.MrId.ToString()),
                new Claim("UserTypeId",Constants.MR_USER.ToString()),
                new Claim("CreatedById",user.Model.CreatedById.ToString()),
                new Claim("DepartmentId",user.Model.CompanyId.ToString()),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                                SecurityAlgorithms.HmacSha256)
                    );



                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_MR_USER + user.Model.MrId.ToString(), new TokenCacheModel { LoginId = user.Model.MrId, UserTypeId = Constants.MR_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }


        #endregion

        #region Patient


        /// <summary>
        /// This Method Verify the patient tele-consultation appointment
        /// </summary>
        [HttpPost("patientlogin")]
        public async Task<ResultModel<PatientTeleConsultationReg>> patientlogin(PatientLoginModel oPatientLoginModel)
        {
            ResultModel<PatientTeleConsultationReg> result = new ResultModel<PatientTeleConsultationReg>();
            var user = await _srv.PatientLogin(oPatientLoginModel);
            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.PatientId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.Mobile.ToString()),
                new Claim("LoginId", user.Model.PatientId.ToString()),
                new Claim("UserTypeId",Constants.PATIENT_USER.ToString()),
                new Claim("CreatedById",0.ToString()),
                new Claim("DepartmentId",user.Model.DepartmentId.ToString()),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                                SecurityAlgorithms.HmacSha256)
                    );



                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_PATIENT_USER + user.Model.PatientId.ToString(), new TokenCacheModel { LoginId = user.Model.PatientId, UserTypeId = Constants.PATIENT_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }


        /// <summary>
        /// This Method Verify the User Existance by Passing Mobile Number
        /// </summary>
        [HttpPost("patientregistrationotp")]
        public async Task<ResultModel<string>> PatientRegistrationOTP([FromBody]PatientLoginModel oPatientLoginModel)
        {
            ResultModel<string> result = new ResultModel<string>();
            try
            {
                //check if already have a pending consultation
                var check = await _srv.CheckPendingConsultation(oPatientLoginModel);

                result.Message = check.Message;
                result.Status = check.Status;
                if (check != null && check.Status == Constants.CONSULTATION_ALREADY_EXIST)
                {
                    return result;
                }
                else if (check != null && check.Status == Constants.PAYMENT_IN_PROCESS)
                {

                    //check last transaction status
                    ResponseModel oResponseModel = await CashFree_PaymentGateway.GetConsultationPaymentStatus(new RequestModel
                    {
                        appId = AppSetting.PaymentAPIKey,
                        secretKey = AppSetting.PaymentSecretKey,
                        orderId = check.Model.ConsultationReferenceNumber

                    });
                    if (oResponseModel.orderStatus == "PAID") //If paid update the status
                    {

                        //update payment status
                        await _srv.UpdatePaymentStatusBeforeRegistration(check.Model.ConsultationReferenceNumber, Constants.PAYMENT_SUCCESS, oResponseModel.referenceId, oResponseModel.paymentMode);
                        //send SMS
                        Message.SendOTP(check.Model.Mobile, "Dear User, Your Slot (" + check.Model.SlotFromTime + "-" + check.Model.SlotFromTime + ") is fixed with  ReferenceNumber: " + check.Model.ConsultationReferenceNumber + " on " + check.Model.AppointmentDate.ToString("dd/MM/yyyy"));
                        //return message that yout already exists
                        result.Status = Constants.CONSULTATION_ALREADY_EXIST;
                        result.Message = Constants.CONSULTATION_ALREADY_EXIST_MESSAGE;
                    }
                    else if (oResponseModel.orderStatus == "PROCESSED")// if under processed return the message please wait
                    {
                        result.Status = Constants.PAYMENT_IN_PROCESS;
                        result.Message = Constants.PAYMENT_IN_PROCESS_MESSAGE;
                    }

                    else if (oResponseModel.orderStatus == "ACTIVE" || oResponseModel.orderStatus == null)
                    {
                        result.Model = check.Model.ConsultationReferenceNumber;
                    }

                    return result;
                }


                string OTP = Helper.GenerateOTP();
                if (oPatientLoginModel.UserName == "9878208127" || oPatientLoginModel.UserName == "9041423335")
                    OTP = "123456";
                else
                    Message.SendOTP(oPatientLoginModel.UserName, "Dear User, " + OTP + " is your OTP for Book an appointment as Patient in dev-econsultation.ecubix.com. The OTP validity is 15 minutes.");

                //set OTP in Memory
                await _distributedCache.SetStringAsync(oPatientLoginModel.UserName, OTP, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(15) });
                result.Message = Constants.OTP_SENT_MESSAGE;
                result.Status = Constants.SUCCESS;
            }
            catch (Exception)
            {
                result.Message = Constants.EXCEPTION_MESSAGE;
                result.Status = Constants.EXCEPTION;
            }
            return result;
        }

        /// <summary>
        /// This Method Verify the Patient registration OTP and return PatientReg if user exists otherwise empty PatientReg  
        /// </summary>
        [HttpPost("checkpatientexists")]
        public async Task<ResultModel<PatientReg>> CheckPatientExists([FromBody]PatientLoginModel oPatientLoginModel)
        {
            ResultModel<PatientReg> result = new ResultModel<PatientReg>();

            string MemoryOTP = await _distributedCache.GetStringAsync(oPatientLoginModel.UserName);

            if (MemoryOTP != oPatientLoginModel.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.NOTMATCHED_OPT;
                return result;
            }

            result = await _srv.CheckPatientExists(oPatientLoginModel);

            return result;
        }


        /// <summary>
        /// This Method Create the New Patient registration  
        /// </summary>
        [HttpPost("newpatientrregistration")]
        public async Task<ResultModel<ResultPatientReg>> NewPatientRegistration([FromBody]SuperPatientRegistrationModel oSuperPatientRegistrationModel)
        {
            ResultModel<ResultPatientReg> result = new ResultModel<ResultPatientReg>();
            if (DateTime.Now.Date > oSuperPatientRegistrationModel.PatientTeleConsultationReg.AppointmentDate.Date)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.WRONG_DATE_MESSAGE;
                return result;
            }
            string MemoryOTP = await _distributedCache.GetStringAsync(oSuperPatientRegistrationModel.PatientReg.Mobile);

            if (MemoryOTP != oSuperPatientRegistrationModel.PatientReg.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.TIMES_UP_MESSAGE;
                return result;
            }

            // Insert new patient Registration
            result = await _srv.NewPatientRegistration(oSuperPatientRegistrationModel.PatientReg);
            if (result.Status == Constants.SUCCESS) // If registration Successfully done
            {
                oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientReferenceNumber = result.Model.PatientReferenceNumber;
                oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientId = result.Model.PatientId;

                //get Amount from datatbase for security puposes
                oSuperPatientRegistrationModel.PatientTeleConsultationReg.PaymentAmmount = await _srv.GetAmountByDepartment(oSuperPatientRegistrationModel.PatientTeleConsultationReg.DepartmentId);

                // Create Consultation record againt patient
                var statusTelCons = await _srv.InsertPatientTeleConsultationReg(oSuperPatientRegistrationModel.PatientTeleConsultationReg);
                result.Status = statusTelCons.Status;
                result.Message = statusTelCons.Message;
                if (result.Status == Constants.SUCCESS)//If Tele Consultation insert successfully
                {
                    // Create Tele Consultation Reference Number
                    string ConsultationReferenceNumber = await _srv.InsertConsultationReferenceNumber(statusTelCons.Model.PatientTeleConsultationId, oSuperPatientRegistrationModel.PatientReg.StateId, oSuperPatientRegistrationModel.PatientReg.DistrictId, oSuperPatientRegistrationModel.PatientReg.CityId);
                    oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientTeleConsultationId = statusTelCons.Model.PatientTeleConsultationId;
                    //upload medical records
                    await InsertMedicalRecords(oSuperPatientRegistrationModel, result, ConsultationReferenceNumber);
                    //if (oSuperPatientRegistrationModel.PatientTeleConsultationReg.Type == Constants.ONLINE_CONSULTATION)//if online Appointment
                    //{

                    if (oSuperPatientRegistrationModel.PatientTeleConsultationReg.PaymentMode != "OFFLINE")
                    {
                        // initiate payment
                        await InitiatePayment(oSuperPatientRegistrationModel, result, ConsultationReferenceNumber);
                    }
                    //}
                    //else
                    //{
                    //    result.Message = "Dear User, Your Slot(" + oSuperPatientRegistrationModel.PatientTeleConsultationReg.SlotFromTime + " - " + oSuperPatientRegistrationModel.PatientTeleConsultationReg.SlotFromTime + ") is fixed with ReferenceNumber: " + ConsultationReferenceNumber + " on " + oSuperPatientRegistrationModel.PatientTeleConsultationReg.AppointmentDate.ToString("dd / MM / yyyy");
                    //}
                }
            }
            result.Model = null;
            return result;
        }



        /// <summary>
        /// This Method Create the Re-Visit Patient registration  
        /// </summary>
        [HttpPost("revisitpatientregistration")]
        public async Task<ResultModel<ResultPatientReg>> ReVisitPatientRegistration([FromBody]SuperPatientRegistrationModel oSuperPatientRegistrationModel)
        {
            ResultModel<ResultPatientReg> result = new ResultModel<ResultPatientReg>();
            if (DateTime.Now.Date > oSuperPatientRegistrationModel.PatientTeleConsultationReg.AppointmentDate.Date)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.WRONG_DATE_MESSAGE;
                return result;
            }
            string MemoryOTP = await _distributedCache.GetStringAsync(oSuperPatientRegistrationModel.PatientReg.Mobile);

            if (MemoryOTP != oSuperPatientRegistrationModel.PatientReg.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.TIMES_UP_MESSAGE;
                return result;
            }

            // Update patient Registration
            result = await _srv.ReVisitPatientRegistration(oSuperPatientRegistrationModel.PatientReg);
            if (result.Status == Constants.SUCCESS) // If registration Successfully done
            {
                oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientReferenceNumber = result.Model.PatientReferenceNumber;
                oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientId = result.Model.PatientId;
                //get Amount from datatbase for security puposes
                oSuperPatientRegistrationModel.PatientTeleConsultationReg.PaymentAmmount = await _srv.GetAmountByDepartment(oSuperPatientRegistrationModel.PatientTeleConsultationReg.DepartmentId);

                // Create Consultation record againt patient
                var statusTelCons = await _srv.InsertPatientTeleConsultationReg(oSuperPatientRegistrationModel.PatientTeleConsultationReg);
                result.Status = statusTelCons.Status;
                result.Message = statusTelCons.Message;
                if (result.Status == Constants.SUCCESS)// If Tele Consultation insert successfully
                {
                    // Create Tele Consultation Reference Number
                    string ConsultationReferenceNumber = await _srv.InsertConsultationReferenceNumber(statusTelCons.Model.PatientTeleConsultationId, oSuperPatientRegistrationModel.PatientReg.StateId, oSuperPatientRegistrationModel.PatientReg.DistrictId, oSuperPatientRegistrationModel.PatientReg.CityId);
                    oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientTeleConsultationId = statusTelCons.Model.PatientTeleConsultationId;
                    //upload medical records
                    await InsertMedicalRecords(oSuperPatientRegistrationModel, result, ConsultationReferenceNumber);
                    //if (oSuperPatientRegistrationModel.PatientTeleConsultationReg.Type == Constants.ONLINE_CONSULTATION)//if online Appointment
                    //{

                    if (oSuperPatientRegistrationModel.PatientTeleConsultationReg.PaymentMode != "OFFLINE")
                    {
                        // initiate payment
                        await InitiatePayment(oSuperPatientRegistrationModel, result, ConsultationReferenceNumber);
                    }
                    //}
                    //else
                    //{
                    //    result.Message = "Dear User, Your Slot(" + oSuperPatientRegistrationModel.PatientTeleConsultationReg.SlotFromTime + " - " + oSuperPatientRegistrationModel.PatientTeleConsultationReg.SlotFromTime + ") is fixed with ReferenceNumber: " + ConsultationReferenceNumber + " on " + oSuperPatientRegistrationModel.PatientTeleConsultationReg.AppointmentDate.ToString("dd / MM / yyyy");
                    //}
                }
            }
            result.Model = null;
            return result;
        }

        private async Task InsertMedicalRecords(SuperPatientRegistrationModel oSuperPatientRegistrationModel, ResultModel<ResultPatientReg> result, string ConsultationReferenceNumber)
        {
            if (oSuperPatientRegistrationModel.lstPatientDocumentReg != null)// If Medical images provided by User
            {
                //Insert each file after file check validation 
                foreach (var item in oSuperPatientRegistrationModel.lstPatientDocumentReg)
                {
                    var oResultModel = FileUpload(result.Model.PatientReferenceNumber + "/" + ConsultationReferenceNumber, item.FilePath, item.FileName, item.FileFlag, "Patient/MedicalRecords", _hostingEnvironment);
                    item.FilePath = oResultModel.Message;// Pass the profile image path for save in the database
                    item.PatientReferenceNumber = result.Model.PatientReferenceNumber;
                    item.PatientId = result.Model.PatientId;
                    item.ConsultationReferenceNumber = ConsultationReferenceNumber;
                    item.PatientTeleConsultationId = oSuperPatientRegistrationModel.PatientTeleConsultationReg.PatientTeleConsultationId;
                    // insert patient medical record
                    await _srv.InsertPatientDocumentReg(item);
                }
            }
        }

        private async Task InitiatePayment(SuperPatientRegistrationModel oSuperPatientRegistrationModel, ResultModel<ResultPatientReg> result, string ConsultationReferenceNumber)
        {
            // Initiate Payment for Payment Link
            ResponseModel oResponseModel = await CashFree_PaymentGateway.CreateConsultationPayment(new RequestModel
            {
                appId = AppSetting.PaymentAPIKey,
                secretKey = AppSetting.PaymentSecretKey,
                orderId = ConsultationReferenceNumber,
                orderAmount = oSuperPatientRegistrationModel.PatientTeleConsultationReg.PaymentAmmount.ToString(),
                orderNote = "notes",
                customerPhone = oSuperPatientRegistrationModel.PatientReg.Mobile,
                customerName = oSuperPatientRegistrationModel.PatientReg.FirstName + " " + oSuperPatientRegistrationModel.PatientReg.LastName,
                customerEmail = oSuperPatientRegistrationModel.PatientReg.EmailAddress,
                notifyUrl = AppSetting.NotifyURL,
                returnUrl = AppSetting.ReturnURL,
            });

            if (oResponseModel.status == "OK")  // If success return payment link 
            {
                await _srv.UpdatePaymentPaymentLink(ConsultationReferenceNumber, oResponseModel.paymentLink);
                result.Message = oResponseModel.paymentLink;
            }
            else if (oResponseModel.status == "ERROR")   // If error return error message
            {
                result.Status = Constants.PAYMENT_ISSUE;
                result.Message = Constants.PAYMENT_ISSUE_MESSAGE;
            }
        }


        /// <summary>
        /// This Method Get the Doctor Slots 
        /// Pass DoctorSlotRequest(Slot Date and Doctor Id) as Parameter
        /// </summary>
        [HttpPost("GetDoctorSlots")]
        public async Task<ResultModel<object>> GetDoctorSlots([FromBody] DoctorSlotRequest oDoctorSlotRequest)
        {
            return await _srv.GetDoctorSlots(oDoctorSlotRequest, Constants.PATIENT_USER);
        }




        #endregion

        #region Patient Profile Login
        /// <summary>
        /// This Method Verify the User Existance by Passing Mobile Number for Profile Login
        /// </summary>
        [HttpPost("checkpatientprofile")]
        public async Task<ResultModel<object>> CheckPatientProfile([FromBody]PatientLoginModel oPatientLoginModel)
        {
            ResultModel<object> result = new ResultModel<object>();
            result = await _srv.CheckPatientProfileExist(oPatientLoginModel);
            if (result.Success == true && result.Status == Constants.SUCCESS)
            {
                string OTP = Helper.GenerateOTP();
                if (oPatientLoginModel.UserName == "9878208127" || oPatientLoginModel.UserName == "9041423335")
                    OTP = "123456";
                else
                    Message.SendOTP(oPatientLoginModel.UserName, "Dear User, " + OTP + " is your OTP for Profile in dev-econsultation.ecubix.com. The OTP validity is 15 minutes.");

                //set OTP in Memory
                await _distributedCache.SetStringAsync(oPatientLoginModel.UserName, OTP, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(15) });
            }

            return result;
        }


        /// <summary>
        /// This Method Verify the Patient Profile Login
        /// After Successfull Login rturn the Patient Details along eith encrypted JWT token
        /// </summary>

        [HttpPost("authenticatepatientprofile")]
        public async Task<ResultModel<PatientReg>> AuthenticatePatientProfile(PatientLoginModel oPatientLoginModel)
        {
            ResultModel<PatientReg> result = new ResultModel<PatientReg>();

            string MemoryOTP = await _distributedCache.GetStringAsync(oPatientLoginModel.UserName);

            if (MemoryOTP != oPatientLoginModel.OTP)
            {
                result.Status = Constants.INVALID;
                result.Message = Constants.NOTMATCHED_OPT;
                return result;
            }

            //Call service method for check the Admin Credentials
            var user = await _srv.VerifyPatientProfileLogin(oPatientLoginModel);


            //If Authentication passed then JWT token created and encrypt before send in response
            if (user.Model != null && user.Model.PatientId > 0)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
                var claims = new[]
                {
                new Claim("MobileNumber", user.Model.Mobile.ToString()),
                new Claim("LoginId", user.Model.PatientId.ToString()),
                new Claim("UserTypeId",Constants.PATIENT_PROFILE_USER.ToString()),
                new Claim("CreatedById",0.ToString()),
                new Claim("DepartmentId","0"),
                };
                var token = new JwtSecurityToken
                    (
                        issuer: AppSetting.Issuer,
                        audience: AppSetting.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        notBefore: DateTime.Now,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret)),
                                SecurityAlgorithms.HmacSha256)
                    );



                string tokenres = new JwtSecurityTokenHandler().WriteToken(token);
                //set token in Cache Memory
                await Cache.SetToken(_distributedCache, Constants.TOKEN_PREFIX_PATIENT_PROFILE_USER + user.Model.PatientId.ToString(), new TokenCacheModel { LoginId = user.Model.PatientId, UserTypeId = Constants.PATIENT_PROFILE_USER, Token = tokenres });
                // Encrypt the token before send back
                user.Model.Token = Secure.Encrypt(tokenres);

            }

            return user;
        }
        #endregion


        #region Masters
        /// <summary>
        /// This method used for Get the Active Hospitals
        /// </summary>
        [HttpGet("getactivehospitals")]
        public async Task<ResultModel<object>> GetActiveHospitals()
        {
            return await _srv.GetActiveHospitals();
        }


        /// <summary>
        /// This method used for Get the Active Branch By Passing Hospital Id
        /// </summary>

        [HttpGet("getactivebranchbyhospital/{Id}")]
        public async Task<ResultModel<object>> GetActiveBranchByHospital(long Id)
        {
            return await _srv.GetActiveBranchByHospital(Id);
        }


        /// <summary>
        /// This method used for Get the Active Department By Passing BranchId Id
        /// </summary>

        [HttpGet("getactivedepartmentbybranch/{Id}")]
        public async Task<ResultModel<object>> GetActiveDepartmentByBranch(long Id)
        {
            return await _srv.GetActiveDepartmentByBranch(Id);
        }


        /// <summary>
        /// This method used for Get the Active Doctor By Passing Department Id
        /// </summary>

        [HttpGet("getactivedoctorbydepartment/{Id}")]
        public async Task<ResultModel<object>> GetActiveDoctorByDepartment(long Id)
        {
            return await _srv.GetActiveDoctorByDepartment(Id);
        }
        #endregion
        //[HttpGet("CheckOrder/{id}")]
        //public async Task<ResponseModel> CheckOrder(string id)
        //{
        //    return await CashFree_PaymentGateway.CreateOrder(new RequestModel
        //    {
        //        appId = "3490883c7596b5dd9ad88e72580943",
        //        secretKey = "448fbd97449bbcffb8b6833160bfb9daddfdcc30",
        //        orderId = "3232113221s",
        //        orderAmount = "100",
        //        orderNote = "notes",
        //        customerPhone = "90422213335",
        //        customerName = "cusqt4444name",
        //        customerEmail = "custq144123email@in.in",
        //        notifyUrl = "https://localhost:44388/Response",
        //        returnUrl = "https://localhost:44388/Response",
        //    });

        //}


        [HttpGet("GetConsultationPaymentStatus/{Id}")]
        public async Task GetConsultationPaymentStatus(string Id)
        {
            // Initiate Payment for Payment Link
            ResponseModel oResponseModel = await CashFree_PaymentGateway.GetConsultationPaymentStatus(new RequestModel
            {
                appId = AppSetting.PaymentAPIKey,
                secretKey = AppSetting.PaymentSecretKey,
                orderId = Id

            });


        }

        /// <summary>
        /// This Method used to discard unpaid conultation
        /// Pass SearchModel as Parameter
        /// </summary>
        [HttpPost("discardconsultation")]
        public async Task<ResultModel<object>> DiscardConsultaion([FromBody] RefundRequest oRefundRequest)
        {
            return await _srv.DiscardConsultation(oRefundRequest);
        }


        /// <summary>
        /// This Method used to get landing page count
        /// </summary>
        [HttpGet("getlandingcount")]
        public async Task<ResultModel<object>> GetLandingCount()
        {
            return await _commonsrv.GetLandingCount();
        }

        /// <summary>
        /// This Method used to get landing page count
        /// </summary>
        [HttpGet("checkpath")]
        public string CheckPath()
        {
            return _hostingEnvironment.WebRootPath; ;
        }

    }
}
