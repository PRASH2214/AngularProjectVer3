using Cubix.BAL.Interfaces;
using Cubix.BAL.Interfaces.Doctor;
using Cubix.BAL.Interfaces.MR;
using Cubix.BAL.Interfaces.Patient;
using Cubix.BAL.Interfaces.Reports;
using Cubix.BAL.Interfaces.SuperAdmin;
using Cubix.BAL.Services;
using Cubix.BAL.Services.Doctor;
using Cubix.BAL.Services.MR;
using Cubix.BAL.Services.Patient;
using Cubix.BAL.Services.Reports;
using Cubix.BAL.Services.SuperAdmin;
using Microsoft.Extensions.DependencyInjection;


namespace Cubix
{

    /// <summary>
    ///  This class used to creates a Scoped service
    /// </summary>
    public class ServiceToScope
    {


        /// <summary>
        ///  This method creates a Scoped service
        ///  A new instance of a Scoped service is created once per request within the scope
        /// </summary>
        public void AddToScope(IServiceCollection services)
        {
            services.AddScoped<IAuth, AuthService>();

            #region Super Doctor
            services.AddScoped<ISuperAdminProfile, SuperAdminProfileService>();
            services.AddScoped<IAdminUsers, AdminUsersService>();
            #endregion

            #region Admin
            services.AddScoped<IAdmin, AdminService>();
            services.AddScoped<ICommon, CommonService>();
            services.AddScoped<IHospital, HospitalService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddScoped<IBranch, BranchService>();
            services.AddScoped<IDoctor, DoctorService>();
            services.AddScoped<ISpecialityMaster, SpecialityMasterService>();
            services.AddScoped<IMedicineMaster, MedicineMasterService>();
            services.AddScoped<IDrugMaster, DrugMasterService>();
            services.AddScoped<ICompany, CompanyService>();
            services.AddScoped<IMR, MRService>();
            services.AddScoped<IMasterDosevalue, MasterDosevalueService>();
            services.AddScoped<IDrugType, DrugTypeService>();
            services.AddScoped<ISlotTimeMaster, SlotTimeMasterService>();
            services.AddScoped<IDoctorConsultations, DoctorConsultationsService>();
            #endregion


            #region Doctor
            services.AddScoped<IDoctorProfile, DoctorProfileService>();
            #endregion

            #region MR
            services.AddScoped<IMRProfile, MRProfileService>();
            #endregion

            #region Patient
            services.AddScoped<IPatientProfile, PatientProfileService>();
            services.AddScoped<IPatientConsultations, PatientConsultationsService>();
            #endregion


            #region Reports
            services.AddScoped<IReports, ReportsService>();
            #endregion

        }
    }
}
