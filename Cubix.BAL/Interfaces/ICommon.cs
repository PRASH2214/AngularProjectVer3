using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
    public interface ICommon
    {

        Task<ResultModel<object>> GetLandingCount();
        Task<ResultModel<object>> GetMedicineMaster(long CreatedById);
        Task<ResultModel<object>> GetDrugType(long CreatedById);
        Task<ResultModel<object>> GetMasterDosevalue(long CreatedById);
        Task<ResultModel<MasterState>> GetStates();
        Task<ResultModel<MasterDistrict>> GetDistricts();
        Task<ResultModel<MasterCity>> GetCities();
        Task<ResultModel<MasterMaritalData>> GetMasterMaritalStatus();
        Task<ResultModel<object>> GetActiveHospitals(TokenModel oTokenModel);
        Task<ResultModel<object>> GetActiveBranchByHospital(long HospitalId);
        Task<ResultModel<object>> GetActiveDepartmentByBranch(long BranchId);
        Task<ResultModel<object>> GetActiveDoctorByDepartment(long DepartmentId);
        Task<ResultModel<object>> GetActiveSpeciality(long CreatedById);
        Task<ResultModel<object>> GetMasterDays();
        Task<ResultModel<object>> GetDrugMaster(TokenModel oTokenModel);
        Task<ResultModel<object>> GetActiveCompanyMaster(TokenModel oTokenModel);
        Task<ResultModel<object>> GetActiveDrugType(TokenModel oTokenModel);
        Task<ResultModel<object>> GetPatientProfile(long Patientid);
        Task<List<PatientDocumentReg>> GetPatientDocumentRegByConsultationId(long ConsultationId);
        Task<List<PatientTeleConsultationAllergy>> GetPatientTeleConsultationAllergyByConsultationId(long ConsultationId);
        Task<PatientTeleConsultationExamination> GetPatientTeleConsultationExaminationByConsultationId(long ConsultationId);
        Task<List<PatientTeleConsultationDiagnosis>> GetPatientTeleConsultationDiagnosisByConsultationId(long ConsultationId);
        Task<List<PatientTeleConsultationMedicine>> GetPatientTeleConsultationMedicineByConsultationId(long ConsultationId);

        Task<ResultModel<object>> InsertConsultationResponse(TokenModel oTokenModel, PatientTeleConsultationDetail oPatientTeleConsultationDetail);
        Task<ResultModel<object>> InsertPatientTeleConsultationAllergy(TokenModel oTokenModel, PatientTeleConsultationAllergy oPatientTeleConsultationAllergy);
        Task<ResultModel<object>> InsertPatientTeleConsultationExamination(TokenModel oTokenModel, PatientTeleConsultationExamination oPatientTeleConsultationExamination);
        Task<ResultModel<object>> InsertPatientTeleConsultationDiagnosis(TokenModel oTokenModel, PatientTeleConsultationDiagnosis oPatientTeleConsultationDiagnosis);
        Task<ResultModel<object>> InsertPatientTeleConsultationMedicine(TokenModel oTokenModel, PatientTeleConsultationMedicine oPatientTeleConsultationMedicine);
        Task<ResultModel<object>> InsertMRConsultationResponse(TokenModel oTokenModel, MRResponseTeleConsultationReg oMRResponseTeleConsultationReg);

        Task<int> CheckStateName(string StateName);
        Task<int> CheckCityName(string CityName);
        Task<int> CheckDistrictName(string DistrictName);
        Task<string> CheckHospitalValidations(string HospitalLicenseNumber, string OwnerMobile, string ContactMobile, string HospitalName);

        Task<long> CheckHospitalName(string HospitalName, long CreatedById);
        Task<string> CheckBranchValidations(string BranchHospitalLicenseNumber,string ContactMobile, string BranchName);

        Task<long> CheckBranchName(string BranchName, long HospitalId, long CreatedById);
        Task<string> CheckDepartmentValidations(string DepartmentContactMobile, string DepartmentName);

        Task<long> CheckSpecialityName(string SpecialityName, long CreatedById);

        Task<long> CheckDepartmentName(string DepartmentName, long BranchId, long CreatedById);

        Task<string> CheckDoctorValidations(string DoctorLicenseNumber, string Mobile);
        Task<string> CheckDrugValidations(string DrugName, long CreatedById);
        Task<long> CheckCompanyName(string CompanyName, long CreatedById);

        Task<string> CheckMRValidations(string MrLicenseNumber, string Mobile);
        Task<string> CheckCompanyValidations(string CompanyLicenseNumber, string AdminMobile, string SpocMobile, string CompanyName);
        Task<long> CheckDrugName(string DrugName, long CreatedById);
        Task<long> CheckDrugType(string DrugTypeName, long CreatedById);
    }
}
