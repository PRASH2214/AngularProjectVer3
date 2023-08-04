using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.Patient
{
   public  interface IPatientProfile
    {
        Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel);
        Task<ResultModel<object>> GetPatient(TokenModel oTokenModel);
        Task<ResultModel<object>> UpdatePatient(PatientReg oPatientReg, TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload);
    }
}
