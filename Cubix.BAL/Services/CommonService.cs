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
    public class CommonService : ICommon
    {


        public async Task<ResultModel<object>> GetLandingCount()
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Result.Model = await DBQuery.ExeSPScaler<object>("SP_Get_Landing_Count", Pars);
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


        public async Task<ResultModel<MasterCity>> GetCities()
        {
            ResultModel<MasterCity> Result = new ResultModel<MasterCity>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<MasterCity>("Select  CityId,DistrictId,CityName from MasterCity where status=1");
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

        public async Task<ResultModel<MasterDistrict>> GetDistricts()
        {
            ResultModel<MasterDistrict> Result = new ResultModel<MasterDistrict>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<MasterDistrict>("Select  StateId,DistrictId,DistrictName from MasterDistrict  where status=1");
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


        public async Task<ResultModel<MasterState>> GetStates()
        {
            ResultModel<MasterState> Result = new ResultModel<MasterState>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<MasterState>("Select  StateId,StateName from MasterState  where status=1");
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

        public async Task<ResultModel<MasterMaritalData>> GetMasterMaritalStatus()
        {
            ResultModel<MasterMaritalData> Result = new ResultModel<MasterMaritalData>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<MasterMaritalData>("Select  * from MasterMaritalData  where status=1");
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


        public async Task<ResultModel<object>> GetActiveHospitals(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {


                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  HospitalId,HospitalName from HospitalReg  where status=1 and CreatedById=" + oTokenModel.LoginId);

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
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  DepartmentId,DepartmentName from DepartmentReg  where status=1 and BranchId=" + BranchId);
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

        public async Task<ResultModel<object>> GetActiveSpeciality(long CreatedById)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select SpecialityId,SpecialityName from MasterSpecialityData  where status=1 and CreatedById=" + CreatedById);
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


        public async Task<ResultModel<object>> GetMasterDays()
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>(" SELECT*  FROM MasterDays");
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


        public async Task<ResultModel<object>> GetDrugMaster(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>(" SELECT DrugId,DrugName  FROM MasterDrug where status=1 and CreatedById=" + oTokenModel.LoginId);
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

        public async Task<ResultModel<object>> GetActiveCompanyMaster(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>(" SELECT CompanyId,CompanyName  FROM CompanyReg where status=1  and CreatedById=" + oTokenModel.LoginId);
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

        public async Task<ResultModel<object>> GetActiveDrugType(TokenModel oTokenModel)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Result.LstModel = await DBQuery.ExeQueryList<object>(" SELECT *  FROM DrugType where status=1  and CreatedById=" + oTokenModel.LoginId);
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


        public async Task<ResultModel<object>> GetMedicineMaster(long CreatedById)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", CreatedById);
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  * from MasterMedicine Where CreatedById=@CreatedById", Pars);
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

        public async Task<ResultModel<object>> GetMasterDosevalue(long CreatedById)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", CreatedById);
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  * from MasterDosevalue Where CreatedById=@CreatedById", Pars);
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

        public async Task<ResultModel<object>> GetDrugType(long CreatedById)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CreatedById", CreatedById);
                Result.LstModel = await DBQuery.ExeQueryList<object>("Select  * from DrugType Where CreatedById=@CreatedById", Pars);
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




        public async Task<ResultModel<object>> GetPatientProfile(long PatientId)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientId", PatientId);
                Result.Model = await DBQuery.ExeScalarQuery<object>("Select  * from PatientReg Where status=1  and  PatientId=@PatientId ", Pars);
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


        public async Task<ResultModel<SuperPatientTeleConsultation>> GetConsultationPatientDetail(TokenModel oTokenModel, long ConsultationId)
        {
            ResultModel<SuperPatientTeleConsultation> Result = new ResultModel<SuperPatientTeleConsultation>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@ConsultationId", ConsultationId);
                Result.Model.PatientTeleConsultationDetail = await DBQuery.ExeSPScaler<PatientTeleConsultationDetail>("SP_Get_Consultation_Detail", Pars);


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

        public async Task<List<PatientDocumentReg>> GetPatientDocumentRegByConsultationId(long ConsultationId)
        {
            List<PatientDocumentReg> Result = new List<PatientDocumentReg>();
            try
            {
                Result = await DBQuery.ExeQueryList<PatientDocumentReg>("Select  * from PatientDocumentReg where PatientTeleConsultationId=" + ConsultationId);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return Result;
        }

        public async Task<List<PatientTeleConsultationAllergy>> GetPatientTeleConsultationAllergyByConsultationId(long ConsultationId)
        {
            List<PatientTeleConsultationAllergy> Result = new List<PatientTeleConsultationAllergy>();
            try
            {
                Result = await DBQuery.ExeQueryList<PatientTeleConsultationAllergy>("Select  * from PatientTeleConsultationAllergy where PatientTeleConsultationId=" + ConsultationId);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return Result;
        }

        public async Task<PatientTeleConsultationExamination> GetPatientTeleConsultationExaminationByConsultationId(long ConsultationId)
        {
            PatientTeleConsultationExamination Result = new PatientTeleConsultationExamination();
            try
            {
                Result = await DBQuery.ExeScalarQuery<PatientTeleConsultationExamination>("Select  * from PatientTeleConsultationExamination where PatientTeleConsultationId=" + ConsultationId);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return Result;
        }

        public async Task<List<PatientTeleConsultationDiagnosis>> GetPatientTeleConsultationDiagnosisByConsultationId(long ConsultationId)
        {
            List<PatientTeleConsultationDiagnosis> Result = new List<PatientTeleConsultationDiagnosis>();
            try
            {
                Result = await DBQuery.ExeQueryList<PatientTeleConsultationDiagnosis>("Select  * from PatientTeleConsultationDiagnosis where PatientTeleConsultationId=" + ConsultationId);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return Result;
        }

        public async Task<List<PatientTeleConsultationMedicine>> GetPatientTeleConsultationMedicineByConsultationId(long ConsultationId)
        {
            List<PatientTeleConsultationMedicine> Result = new List<PatientTeleConsultationMedicine>();
            try
            {
                Result = await DBQuery.ExeQueryList<PatientTeleConsultationMedicine>("Select  * from PatientTeleConsultationMedicine where PatientTeleConsultationId=" + ConsultationId);
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return Result;
        }



        public async Task<ResultModel<object>> InsertConsultationResponse(TokenModel oTokenModel, PatientTeleConsultationDetail oPatientTeleConsultationDetail)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", oPatientTeleConsultationDetail.PatientTeleConsultationId);
                Pars.Add("@Advice", oPatientTeleConsultationDetail.Advice);
                Pars.Add("@Status", Constants.COMPLETED);
                Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_COMPLETED);
                Pars.Add("@ModifiedDate", DateTime.Now);
                Pars.Add("@CompletedDateTime", DateTime.Now);
                Pars.Add("@DoctorId", oTokenModel.LoginId);

                var res = await DBQuery.ExeQuery("update  PatientTeleConsultationReg set" +
                    " Status=@Status," +
                    " ConsultationsStatus=@ConsultationsStatus," +
                    " ModifiedDate=@ModifiedDate," +
                    " Advice=@Advice," +
                    " CompletedDateTime=@CompletedDateTime" +
                    " Where DoctorId=@DoctorId  and  PatientTeleConsultationId=@PatientTeleConsultationId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;

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


        public async Task<ResultModel<object>> InsertMRConsultationResponse(TokenModel oTokenModel, MRResponseTeleConsultationReg oMRResponseTeleConsultationReg)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MRTeleConsultationId", oMRResponseTeleConsultationReg.MRTeleConsultationId);
                Pars.Add("@Status", Constants.COMPLETED);
                Pars.Add("@ConsultationsStatus", Constants.CONSULTATION_COMPLETED);
                Pars.Add("@Response", oMRResponseTeleConsultationReg.Response);
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                Pars.Add("@ModifiedDate", DateTime.Now);
                var res = await DBQuery.ExeQuery("update  MRTeleConsultationReg set" +
                    " Status=@Status," +
                    " ConsultationsStatus=@ConsultationsStatus," +
                    " ModifiedDate=@ModifiedDate," +
                    " Response=@Response" +
                    " Where DoctorId=@DoctorId  and  MRTeleConsultationId=@MRTeleConsultationId ", Pars) > 0 ? Constants.SUCCESS : Constants.NOTUPDATED;

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




        public async Task<ResultModel<object>> InsertPatientTeleConsultationAllergy(TokenModel oTokenModel, PatientTeleConsultationAllergy oPatientTeleConsultationAllergy)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", oPatientTeleConsultationAllergy.PatientTeleConsultationId);
                Pars.Add("@Name", oPatientTeleConsultationAllergy.Name);
                Pars.Add("@IsStill", oPatientTeleConsultationAllergy.IsStill);
                Pars.Add("@Notes", oPatientTeleConsultationAllergy.Notes);
                Pars.Add("@SeverityType", oPatientTeleConsultationAllergy.SeverityType);
                Pars.Add("@Duration", oPatientTeleConsultationAllergy.Duration);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ConsultationReferenceNumber", oPatientTeleConsultationAllergy.ConsultationReferenceNumber);
                Pars.Add("@PatientId", oPatientTeleConsultationAllergy.PatientId);
                Pars.Add("@DoctorId", oTokenModel.LoginId);
                var res = await DBQuery.ExeSPScaler<int>("SP_PatientTeleConsultationAllergy_Insert", Pars);
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


        public async Task<ResultModel<object>> InsertPatientTeleConsultationExamination(TokenModel oTokenModel, PatientTeleConsultationExamination oPatientTeleConsultationExamination)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", oPatientTeleConsultationExamination.PatientTeleConsultationId);
                Pars.Add("@GeneralExamination", oPatientTeleConsultationExamination.GeneralExamination);
                Pars.Add("@IsSmoker", oPatientTeleConsultationExamination.IsSmoker);
                Pars.Add("@IsDiabetic", oPatientTeleConsultationExamination.IsDiabetic);
                Pars.Add("@IsAlcoholic", oPatientTeleConsultationExamination.IsAlcoholic);
                Pars.Add("@AdditionalMedicine", oPatientTeleConsultationExamination.AdditionalMedicine);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ConsultationReferenceNumber", oPatientTeleConsultationExamination.ConsultationReferenceNumber);
                Pars.Add("@PatientId", oPatientTeleConsultationExamination.PatientId);
                Pars.Add("@DoctorId", oTokenModel.LoginId);

                var res = await DBQuery.ExeSPScaler<int>("SP_PatientTeleConsultationExamination_Insert", Pars);
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


        public async Task<ResultModel<object>> InsertPatientTeleConsultationDiagnosis(TokenModel oTokenModel, PatientTeleConsultationDiagnosis oPatientTeleConsultationDiagnosis)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", oPatientTeleConsultationDiagnosis.PatientTeleConsultationId);
                Pars.Add("@Name", oPatientTeleConsultationDiagnosis.Name);
                Pars.Add("@Code", oPatientTeleConsultationDiagnosis.Code);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ConsultationReferenceNumber", oPatientTeleConsultationDiagnosis.ConsultationReferenceNumber);
                Pars.Add("@PatientId", oPatientTeleConsultationDiagnosis.PatientId);
                Pars.Add("@DoctorId", oTokenModel.LoginId);

                var res = await DBQuery.ExeSPScaler<int>("SP_PatientTeleConsultationDiagnosis_Insert", Pars);
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

        public async Task<ResultModel<object>> InsertPatientTeleConsultationMedicine(TokenModel oTokenModel, PatientTeleConsultationMedicine oPatientTeleConsultationMedicine)
        {
            ResultModel<object> Result = new ResultModel<object>();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@PatientTeleConsultationId", oPatientTeleConsultationMedicine.PatientTeleConsultationId);
                Pars.Add("@MedicineId", oPatientTeleConsultationMedicine.MedicineId);
                Pars.Add("@MedicineName", oPatientTeleConsultationMedicine.MedicineName);
                Pars.Add("@Frequency", oPatientTeleConsultationMedicine.Frequency);
                Pars.Add("@Dosage", oPatientTeleConsultationMedicine.Dosage);
                Pars.Add("@DosageType", oPatientTeleConsultationMedicine.DosageType);
                Pars.Add("@Duration", oPatientTeleConsultationMedicine.Duration);
                Pars.Add("@Notes", oPatientTeleConsultationMedicine.Notes);
                Pars.Add("@CreatedDate", DateTime.Now);
                Pars.Add("@ConsultationReferenceNumber", oPatientTeleConsultationMedicine.ConsultationReferenceNumber);
                Pars.Add("@PatientId", oPatientTeleConsultationMedicine.PatientId);
                Pars.Add("@DoctorId", oTokenModel.LoginId);


                var res = await DBQuery.ExeSPScaler<int>("SP_PatientTeleConsultationMedicine_Insert", Pars);
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


        public async Task<long> CheckDrugName(string DrugName, long CreatedById)
        {
            long result = 0;
            try
            {
                DrugName = DrugName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DrugName", DrugName);
                Pars.Add("@CreatedById", CreatedById);
                MasterDrug oMasterDrug = await DBQuery.ExeScalarQuery<MasterDrug>("Select DrugId from  MasterDrug  where status=1 and lower(DrugName) = @DrugName and CreatedById=@CreatedById", Pars);
                if (oMasterDrug != null && oMasterDrug.DrugId > 0)
                {
                    result = oMasterDrug.DrugId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }
        public async Task<long> CheckDrugType(string DrugTypeName, long CreatedById)
        {
            long result = 0;
            try
            {
                DrugTypeName = DrugTypeName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@Name", DrugTypeName);
                Pars.Add("@CreatedById", CreatedById);
                DrugType oDrugType = await DBQuery.ExeScalarQuery<DrugType>("Select DrugTypeId  FROM DrugType where status=1 and lower(Name) = @DrugTypeName and CreatedById=@CreatedById", Pars);
                if (oDrugType != null && oDrugType.DrugTypeId > 0)
                {
                    result = oDrugType.DrugTypeId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<long> CheckCompanyName(string CompanyName, long CreatedById)
        {
            long result = 0;
            try
            {
                CompanyName = CompanyName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CompanyName", CompanyName);
                Pars.Add("@CreatedById", CreatedById);
                CompanyReg oCompanyReg = await DBQuery.ExeScalarQuery<CompanyReg>("Select CompanyId  from CompanyReg where status=1 and lower(CompanyName) = @CompanyName and CreatedById=@CreatedById", Pars);
                if (oCompanyReg != null && oCompanyReg.CompanyId > 0)
                {
                    result = oCompanyReg.CompanyId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }

        public async Task<int> CheckStateName(string StateName)
        {
            int result = 0;
            try
            {
                StateName = StateName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@StateName", StateName);
                MasterState oMasterState = await DBQuery.ExeScalarQuery<MasterState>("Select *  from MasterState where status=1 and lower(StateName) = @StateName", Pars);
                if (oMasterState != null && oMasterState.StateId > 0)
                {
                    result = oMasterState.StateId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<int> CheckCityName(string CityName)
        {
            int result = 0;
            try
            {
                CityName = CityName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CityName", CityName);
                MasterCity oMasterCity = await DBQuery.ExeScalarQuery<MasterCity>("Select *  from MasterCity where status=1 and lower(CityName) = @CityName", Pars);
                if (oMasterCity != null && oMasterCity.CityId > 0)
                {
                    result = oMasterCity.CityId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<int> CheckDistrictName(string DistrictName)
        {
            int result = 0;
            try
            {
                DistrictName = DistrictName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DistrictName", DistrictName);
                MasterDistrict oMasterDistrict = await DBQuery.ExeScalarQuery<MasterDistrict>("Select *  from MasterDistrict where status=1 and lower(DistrictName) = @DistrictName", Pars);
                if (oMasterDistrict != null && oMasterDistrict.DistrictId > 0)
                {
                    result = oMasterDistrict.DistrictId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<string> CheckHospitalValidations(string HospitalLicenseNumber, string OwnerMobile, string ContactMobile, string HospitalName)
        {
            string result = "";
            try
            {
                HospitalLicenseNumber = HospitalLicenseNumber.ToLower();
                HospitalName = HospitalName.ToLower();

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalLicenseNumber", HospitalLicenseNumber);

                HospitalReg oHospitalReg = await DBQuery.ExeScalarQuery<HospitalReg>("Select HospitalId from HospitalReg where  lower(HospitalLicenseNumber) = @HospitalLicenseNumber", Pars);
                if (oHospitalReg != null && oHospitalReg.HospitalId > 0)
                {
                    result = " Hospital License Number already exists. | ";
                }


                //Pars = new Dapper.DynamicParameters();
                //Pars.Add("@OwnerMobile", OwnerMobile);
                //oHospitalReg = await DBQuery.ExeScalarQuery<HospitalReg>("Select HospitalId  from HospitalReg where  OwnerMobile = @OwnerMobile", Pars);
                //if (oHospitalReg != null && oHospitalReg.HospitalId > 0)
                //{
                //    result = result + " Owner Mobile already exists. | ";
                //}

                Pars = new Dapper.DynamicParameters();
                Pars.Add("@ContactMobile", ContactMobile);
                oHospitalReg = await DBQuery.ExeScalarQuery<HospitalReg>("Select HospitalId  from HospitalReg where  ContactMobile = @ContactMobile", Pars);
                if (oHospitalReg != null && oHospitalReg.HospitalId > 0)
                {
                    result = result + "Contact Mobile already exists. | ";
                }


                Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalName", HospitalName);
                oHospitalReg = await DBQuery.ExeScalarQuery<HospitalReg>("Select HospitalId from HospitalReg where  lower(HospitalName) = @HospitalName", Pars);
                if (oHospitalReg != null && oHospitalReg.HospitalId > 0)
                {
                    result = result + "Hospital Name already exists. | ";
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }

        public async Task<long> CheckHospitalName(string HospitalName, long CreatedById)
        {
            long result = 0;
            try
            {
                HospitalName = HospitalName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@HospitalName", HospitalName);
                Pars.Add("@CreatedById", CreatedById);
                HospitalReg oHospitalReg = await DBQuery.ExeScalarQuery<HospitalReg>("Select HospitalId  from HospitalReg  where status=1 and lower(HospitalName) = @HospitalName and CreatedById=@CreatedById", Pars);
                if (oHospitalReg != null && oHospitalReg.HospitalId > 0)
                {
                    result = oHospitalReg.HospitalId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }



        public async Task<string> CheckBranchValidations(string BranchHospitalLicenseNumber, string ContactMobile, string BranchName)
        {
            string result = "";
            try
            {
                BranchHospitalLicenseNumber = BranchHospitalLicenseNumber.ToLower();
                BranchName = BranchName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@BranchHospitalLicenseNumber", BranchHospitalLicenseNumber);

                BranchReg oBranchReg = await DBQuery.ExeScalarQuery<BranchReg>("Select BranchId from BranchReg where  lower(BranchHospitalLicenseNumber) = @BranchHospitalLicenseNumber", Pars);
                if (oBranchReg != null && oBranchReg.BranchId > 0)
                {
                    result = " Branch License Number already exists. | ";
                }

                Pars = new Dapper.DynamicParameters();
                Pars.Add("@ContactMobile", ContactMobile);
                oBranchReg = await DBQuery.ExeScalarQuery<BranchReg>("Select BranchId  from BranchReg where  ContactMobile = @ContactMobile", Pars);
                if (oBranchReg != null && oBranchReg.BranchId > 0)
                {
                    result = result + "Contact Mobile already exists. | ";
                }

                Pars = new Dapper.DynamicParameters();
                Pars.Add("@BranchName", BranchName);
                oBranchReg = await DBQuery.ExeScalarQuery<BranchReg>("Select BranchId from BranchReg where  lower(BranchName) = @BranchName", Pars);
                if (oBranchReg != null && oBranchReg.BranchId > 0)
                {
                    result = result + "Branch Name already exists. | ";
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }



        public async Task<long> CheckBranchName(string BranchName, long HospitalId, long CreatedById)
        {
            long result = 0;
            try
            {
                BranchName = BranchName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@BranchName", BranchName);
                Pars.Add("@HospitalId", HospitalId);
                Pars.Add("@CreatedById", CreatedById);
                BranchReg oBranchReg = await DBQuery.ExeScalarQuery<BranchReg>("Select BranchId  from BranchReg  where HospitalId=@HospitalId and  status=1 and lower(BranchName) = @BranchName and CreatedById=@CreatedById", Pars);
                if (oBranchReg != null && oBranchReg.BranchId > 0)
                {
                    result = oBranchReg.BranchId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<string> CheckDepartmentValidations(string DepartmentContactMobile, string DepartmentName)
        {
            string result = "";
            DepartmentName = DepartmentName.ToLower();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DepartmentContactMobile", DepartmentContactMobile);

                DepartmentReg oDepartmentReg = await DBQuery.ExeScalarQuery<DepartmentReg>("Select DepartmentId  from DepartmentReg where  DepartmentContactMobile = @DepartmentContactMobile", Pars);

                if (oDepartmentReg != null && oDepartmentReg.DepartmentId > 0)
                {
                    result = result + "Department Contact Mobile already exists. | ";
                }

                Pars = new Dapper.DynamicParameters();
                Pars.Add("@DepartmentName", DepartmentName);
                oDepartmentReg = await DBQuery.ExeScalarQuery<DepartmentReg>("Select DepartmentId  from DepartmentReg where  lower(DepartmentName) = @DepartmentName", Pars);

                if (oDepartmentReg != null && oDepartmentReg.DepartmentId > 0)
                {
                    result = result + "Department Name already exists. | ";
                }


            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }



        public async Task<long> CheckSpecialityName(string SpecialityName, long CreatedById)
        {
            int result = 0;
            try
            {
                SpecialityName = SpecialityName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@SpecialityName", SpecialityName);
                Pars.Add("@CreatedById", CreatedById);
                MasterSpecialityData oMasterSpecialityData = await DBQuery.ExeScalarQuery<MasterSpecialityData>("Select SpecialityId from MasterSpecialityData where status=1 and lower(SpecialityName) = @SpecialityName and CreatedById=@CreatedById", Pars);
                if (oMasterSpecialityData != null && oMasterSpecialityData.SpecialityId > 0)
                {
                    result = oMasterSpecialityData.SpecialityId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }



        public async Task<long> CheckDepartmentName(string DepartmentName, long BranchId, long CreatedById)
        {
            long result = 0;
            try
            {
                DepartmentName = DepartmentName.ToLower();
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DepartmentName", DepartmentName);
                Pars.Add("@BranchId", BranchId);
                Pars.Add("@CreatedById", CreatedById);
                DepartmentReg oDepartmentReg = await DBQuery.ExeScalarQuery<DepartmentReg>("Select DepartmentId  from DepartmentReg  where BranchId=@BranchId and  status=1 and lower(DepartmentName) = @DepartmentName and CreatedById=@CreatedById", Pars);
                if (oDepartmentReg != null && oDepartmentReg.DepartmentId > 0)
                {
                    result = oDepartmentReg.DepartmentId;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }



        public async Task<string> CheckMRValidations(string MrLicenseNumber, string Mobile)
        {
            string result = "";
            MrLicenseNumber = MrLicenseNumber.ToLower();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@MrLicenseNumber", MrLicenseNumber);

                MRReg oMRReg = await DBQuery.ExeScalarQuery<MRReg>("Select MrId  from MRReg where lower(MrLicenseNumber) = @MrLicenseNumber", Pars);
                if (oMRReg != null && oMRReg.MrId > 0)
                {
                    result = result + "License Number already exists. | ";
                }


                Pars = new Dapper.DynamicParameters();
                Pars.Add("@Mobile", Mobile);

                oMRReg = await DBQuery.ExeScalarQuery<MRReg>("Select MrId  from MRReg where Mobile = @Mobile", Pars);
                if (oMRReg != null && oMRReg.MrId > 0)
                {
                    result = result + "MR Mobile already exists. | ";
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }

        public async Task<string> CheckDoctorValidations(string DoctorLicenseNumber, string Mobile)
        {
            string result = "";
            DoctorLicenseNumber = DoctorLicenseNumber.ToLower();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DoctorLicenseNumber", DoctorLicenseNumber);

                DoctorReg oDoctorReg = await DBQuery.ExeScalarQuery<DoctorReg>("Select DoctorId  from DoctorReg where lower(DoctorLicenseNumber) = @DoctorLicenseNumber", Pars);
                if (oDoctorReg != null && oDoctorReg.DoctorId > 0)
                {
                    result = result + "License Number already exists. | ";
                }


                Pars = new Dapper.DynamicParameters();
                Pars.Add("@Mobile", Mobile);

                oDoctorReg = await DBQuery.ExeScalarQuery<DoctorReg>("Select DoctorId  from DoctorReg where Mobile = @Mobile", Pars);
                if (oDoctorReg != null && oDoctorReg.DoctorId > 0)
                {
                    result = result + "Doctor Mobile already exists. | ";
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<string> CheckDrugValidations(string DrugName, long CreatedById)
        {
            string result = "";
            DrugName = DrugName.ToLower();
            try
            {
                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@DrugName", DrugName);
                Pars.Add("@CreatedById", CreatedById);
                MasterDrug oMasterDrug = await DBQuery.ExeScalarQuery<MasterDrug>("Select DrugId from MasterDrug Where  DrugName = @DrugName and CreatedById = @CreatedById", Pars);
                if (oMasterDrug != null && oMasterDrug.DrugId > 0)
                {
                    result = result + "License Number already exists. | ";
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }


        public async Task<string> CheckCompanyValidations(string CompanyLicenseNumber, string AdminMobile, string SpocMobile, string CompanyName)
        {
            string result = "";
            try
            {
                CompanyLicenseNumber = CompanyLicenseNumber.ToLower();
                CompanyName = CompanyName.ToLower();

                Dapper.DynamicParameters Pars = new Dapper.DynamicParameters();
                Pars.Add("@CompanyLicenseNumber", CompanyLicenseNumber);

                CompanyReg oCompanyReg = await DBQuery.ExeScalarQuery<CompanyReg>("Select CompanyId from CompanyReg where  lower(CompanyLicenseNumber) = @CompanyLicenseNumber", Pars);
                if (oCompanyReg != null && oCompanyReg.CompanyId > 0)
                {
                    result = " Company License Number already exists. | ";
                }


                Pars = new Dapper.DynamicParameters();
                Pars.Add("@AdminMobile", AdminMobile);
                oCompanyReg = await DBQuery.ExeScalarQuery<CompanyReg>("Select CompanyId  from CompanyReg where  AdminMobile = @AdminMobile", Pars);
                if (oCompanyReg != null && oCompanyReg.CompanyId > 0)
                {
                    result = result + " Admin Mobile already exists. | ";
                }

                Pars = new Dapper.DynamicParameters();
                Pars.Add("@SpocMobile", SpocMobile);
                oCompanyReg = await DBQuery.ExeScalarQuery<CompanyReg>("Select CompanyId  from CompanyReg where  SpocMobile = @SpocMobile", Pars);
                if (oCompanyReg != null && oCompanyReg.CompanyId > 0)
                {
                    result = result + " Spoc Mobile already exists. | ";
                }


                Pars = new Dapper.DynamicParameters();
                Pars.Add("@CompanyName", CompanyName);
                oCompanyReg = await DBQuery.ExeScalarQuery<CompanyReg>("Select CompanyId from CompanyReg where  lower(CompanyName) = @CompanyName", Pars);
                if (oCompanyReg != null && oCompanyReg.CompanyId > 0)
                {
                    result = result + " Company Name already exists. | ";
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex);

            }
            return result;
        }

    }
}
