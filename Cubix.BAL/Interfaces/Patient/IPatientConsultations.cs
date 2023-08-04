using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.Patient
{
    public interface IPatientConsultations
    {
        Task<ResultModel<object>> GetTodayAppointments(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> GetPastConsultations(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> RefundRequest(TokenModel oTokenModel, RefundRequest oRefundRequest);

        Task<ResultModel<SuperPatientTeleConsultation>> GetConsultationPatientDetail(TokenModel oTokenModel, long ConsultationId);

    }
}

