using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.MR
{
    public interface IMRProfile
    {
        Task<ResultModel<object>> GetMR(TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateMR(MRReg oMRReg, TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload);
        Task<ResultModel<ResultMRReg>> InsertMRTeleConsultationReg(MRTeleConsultationReg oMRTeleConsultationReg);
        Task<ResultModel<object>> GetTodayAppointments(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> GetPastConsultations(TokenModel oTokenModel, SearchModel oSearchModel);
    }
}
