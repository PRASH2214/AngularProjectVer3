using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.Doctor
{
    public   interface IDoctorConsultations
    {
        Task<ResultModel<object>> GetTodayAppointments(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> GetPastConsultations(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<PatientTeleConsultationReg>> RefundResponse(TokenModel oTokenModel, RefundRequest oRefundRequest);
        Task<ResultModel<SuperPatientTeleConsultation>> GetConsultationPatientDetail(TokenModel oTokenModel, long ConsultationId);
        Task<ResultModel<object>> GetMRPastConsultations(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> GetMRTodayAppointments(TokenModel oTokenModel, SearchModel oSearchModel);
    }
}

