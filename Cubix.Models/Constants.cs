using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public static class Constants
    {
        public const int ADMIN_USER = 1;
        public const int HOSPITAL_USER = 2;
        public const int DOCTOR_USER = 3;
        public const int PATIENT_USER = 4;
        public const int PATIENT_PROFILE_USER = 5;
        public const int MR_USER = 6;
        public const int SUPERADMIN_USER = 7;

        #region Consultation/Payment Status
        public const int PAYMENT_PENDING = 1;
        public const int PAYMENT_FAIL = 2;
        public const int PAYMENT_CANCELLED = 3;
        public const int PAYMENT_SUCCESS = 4;
        public const int COMPLETED = 5;
        public const int REFUND_REQUEST = 6;
        public const int REFUND_APPROVED = 7;
        public const int REFUND_REJECTED = 8;
        public const int REFUNDED = 9;

        public const int CONSULTATION_PENDING = 1;
        public const int CONSULTATION_COMPLETED = 2;
        #endregion

        #region
        public const int ONLINE_CONSULTATION = 1;
        public const int OFFLINE_CONSULTATION = 2;
        #endregion

        public const string TOKEN_PREFIX_SUPERADMIN_USER = "SUPERADMIN_";
        public const string TOKEN_PREFIX_ADMIN_USER = "ADMIN_";
        public const string TOKEN_PREFIX_DOCTOR_USER = "DOCTOR_";
        public const string TOKEN_PREFIX_MR_USER = "MR_";
        public const string TOKEN_PREFIX_PATIENT_USER = "PATIENT_";
        public const string TOKEN_PREFIX_PATIENT_PROFILE_USER = "PATIENT_PROFILE_";

        public const int SUCCESS = 1;
        public const int INVALID = 2;
        public const int NOTMATCHED = 3;
        public const int NOTCREATED = 4;
        public const int NOTUPDATED = 5;
        public const int NOTDELETED = 6;
        public const int NOFILEPROVIDED = 7;
        public const int INVALIDFILE = 8;
        public const int INVALIDIMAGE = 9;
        public const int INVALIDFILETYPE = 10;
        public const int EXCEPTION = 11;
        public const int VALIDATION_ERROR = 12;
        public const int INSUFFIECIENT_PRIVILEDGE = 13;
        public const int PATIENT_NOT_EXIST = 14;
        public const int PAYMENT_ISSUE = 15;
        public const int CONSULTATION_NOT_EXIST = 16;
        public const int CONSULTATION_ALREADY_EXIST = 17;
        public const int FUTURE_SLOT_TIME = 18;
        public const int MISS_SLOT_TIME = 19;
        public const int PAYMENT_IN_PROCESS = 20;
        public const int SESSION_EXPIRED = 21;
        public const int INVALID_EXCEL = 22;

        public const string SUCCESS_MESSAGE = "Success";
        public const string OTP_SENT_MESSAGE = "OTP sent successfully";
        public const string OTP_MATCH_MESSAGE = "OTP matched successfully";
        public const string EXCEPTION_MESSAGE = "Please relogin";
        public const string INSUFFIECIENT_PRIVILEDGE_MESSAGE = "Insuffiecient privilidges or logged In through other channel";
        public const string SESSION_EXPIRED_MESSAGE = "Session expired. Please relogin.";
        public const string MATCHED_OPT = "OTP Matched successfully";
        public const string NOTMATCHED_OPT = "Wrong OTP";
        public const string TIMES_UP_MESSAGE = "Times UP";
        public const string WRONG_DATE_MESSAGE = "Wrong date selection";
        public const string NOTMATCHED_MESSAGE = "Invalid UserName/Password";
        public const string CREATED_MESSAGE = "Record created successfully";
        public const string NOTCREATED_MESSAGE = "Unable to create";
        public const string LICENCEALREADYEXISTS_MESSAGE = "License/Mobile already exists";
        public const string UPDATED_MESSAGE = "Record updated successfully";
        public const string NOTUPDATED_MESSAGE = "Unable to update";
        public const string DELETE_MESSAGE = "Record delete successfully";
        public const string NOTDELETE_MESSAGE = "Unable to deleted";
        public const string NOFILEPROVIDED_MESSAGE = "No File Provided";
        public const string INVALIDFILE_MESSAGE = "Invalid Name or no Image Provided";
        public const string INVALIDIMAGE_MESSAGE = "Invalid Image";
        public const string INVALIDFILETYPE_MESSAGE = "Invalid File Type";
        public const string NOTUSEREXISTS_MESSAGE = "User not exists";
        public const string NOT_SLOT_DELETE_MESSAGE = "Unable to delete previous Slots";
        public const string ALREADY_ASSOCIATED_MESSAGE = "Record already associated with other records.";
        public const string PAYMENT_ISSUE_MESSAGE = "Payment Gateway down.";
        public const string CONSULTATION_NOT_EXIST_MESSAGE = "Tele-Consultation not exists.";
        public const string CONSULTATION_NOT_TODAY_EXIST_MESSAGE = "No appointment exists for today.";
        public const string CONSULTATION_ALREADY_EXIST_MESSAGE = "Appointment already exists, Kindly Use Patient Login";
        public const string CONSULTATION_OFFLINE_ALREADY_EXIST_MESSAGE = "Offline Appointment already exists, Kindly Use Patient Login";
        public const string FUTURE_SLOT_TIME_MESSAGE = "Please wait for your Slot";
        public const string MISS_SLOT_TIME_MESSAGE = "You have missed your Slot";
        public const string PAYMENT_IN_PROCESS_MESSAGE = "Your last consultaion payment is  under Process.";
        public const string ALREADY_EXISTS_MESSAGE = "Record already exists.";
        public const string MR_APPOINTMENT_ALREADY_EXIST_MESSAGE = "Today Appointment already exists";

        public const string INVALID_EXCCEL_MESSAGE = "Invalid Excel";
    }
}
