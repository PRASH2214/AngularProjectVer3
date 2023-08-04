using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.Reports
{
   public interface IReports
    {
        Task<ResultModel<ReportPatientResultModel>> GetPatientAppointmentSummary(TokenModel oTokenModel, ReportSearchModel oReportSearchModel);
        Task<ResultModel<ReportMRResultModel>> GetMRAppointmentSummary(TokenModel oTokenModel, ReportSearchModel oReportSearchModel);

        Task<ResultModel<object>> GetPatientAppointmentDetailByAdmin(TokenModel oTokenModel, ReportSearchModel oReportSearchModel);
        Task<ResultModel<object>> GetPatientAppointmentDetailByDoctor(TokenModel oTokenModel, ReportSearchModel oReportSearchModel);


        Task<ResultModel<object>> GetMRAppointmentDetailByAdmin(TokenModel oTokenModel, ReportSearchModel oReportSearchModel);
        Task<ResultModel<object>> GetMRAppointmentDetailByDoctor(TokenModel oTokenModel, ReportSearchModel oReportSearchModel);
    }
}

//SP_Report_Patient_Appointment_Detail
//SP_Report_MR_Appointment_Detail