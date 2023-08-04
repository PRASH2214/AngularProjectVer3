using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.Doctor
{
  public interface IDoctorProfile
    {
        Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel);
        Task<ResultModel<object>> GetDoctor(TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateDoctor(DoctorReg oDoctorReg, TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload);
        Task<ResultModel<object>> GetSlots(TokenModel oTokenModel, DoctorSlotRequest oDoctorSlotTime);
        Task<ResultModel<object>> InsertDoctorSlotTime(TokenModel oTokenModel, List<DoctorSlotTime> oDoctorSlotTime);

    }
}
