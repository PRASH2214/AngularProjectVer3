using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
    public interface IAuth
    {
        Task<ResultModel<object>> CheckUserExists(LoginModel ByUser, int UserTypeId);
        Task<ResultModel<SuperAdminReg>> VerifySuperAdminLogin(LoginModel ByUser);
        Task<ResultModel<AdminReg>> VerifyAdminLogin(LoginModel ByUser);

        Task<ResultModel<DoctorReg>> VerifyDoctorLogin(LoginModel ByUser);
        Task<ResultModel<MRReg>> VerifyMRLogin(LoginModel ByUser);
        Task<ResultModel<PatientTeleConsultationReg>> PatientLogin(PatientLoginModel oPatientLoginModel);
        Task<ResultModel<PatientReg>> CheckPatientExists(PatientLoginModel oPatientLoginModel);
        Task<ResultModel<object>> CheckPatientProfileExist(PatientLoginModel oPatientLoginModel);
        Task<ResultModel<PatientReg>> VerifyPatientProfileLogin(PatientLoginModel oPatientLoginModel);


        Task<ResultModel<PatientTeleConsultationReg>> CheckPendingConsultation(PatientLoginModel oPatientLoginModel);
        Task<ResultModel<ResultPatientReg>> NewPatientRegistration(PatientReg oPatientReg);
        Task<ResultModel<ResultPatientReg>> ReVisitPatientRegistration(PatientReg oPatientReg);
        Task<ResultModel<ResultPatientReg>> InsertPatientTeleConsultationReg(PatientTeleConsultationReg oPatientTeleConsultationReg);
        Task<ResultModel<object>> InsertPatientDocumentReg(PatientDocumentReg oPatientDocumentReg);
        Task<string> InsertConsultationReferenceNumber(long PatientTeleConsultationId, int StateId, int DistrictId, int CityId);
        Task<ResultModel<object>> GetDoctorSlots(DoctorSlotRequest oDoctorSlotRequest, int ForUserTypeId);
        Task<ResultModel<PatientTeleConsultationReg>> UpdateTeleConsultationStatus(string ConsultationReferenceNumber, int Status);
        Task<ResultModel<PatientTeleConsultationReg>> UpdateTeleConsultationAfterPayment(string ConsultationReferenceNumber, int Status, string PaymentReferenceId, string PaymentMode);
        Task<ResultModel<PatientTeleConsultationReg>> UpdatePaymentStatusBeforeRegistration(string ConsultationReferenceNumber, int Status, string PaymentReferenceId, string PaymentMode);
        Task<ResultModel<object>> GetActiveHospitals();
        Task<ResultModel<object>> GetActiveBranchByHospital(long HospitalId);
        Task<ResultModel<object>> GetActiveDepartmentByBranch(long BranchId);
        Task<ResultModel<object>> GetActiveDoctorByDepartment(long DepartmentId);
        Task<string> UpdatePaymentPaymentLink(string ConsultationReferenceNumber, string PaymentLink);
        Task<ResultModel<object>> DiscardConsultation(RefundRequest oRefundRequest);


        Task<float> GetAmountByDepartment(long DepartmentId);
    }
}
