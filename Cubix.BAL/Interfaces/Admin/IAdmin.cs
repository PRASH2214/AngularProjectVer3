using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
   public interface IAdmin
    {
        Task<ResultModel<object>> GetDashBoardCounters(TokenModel oTokenModel);
        Task<ResultModel<object>> GetAdmin( TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateAdmin(AdminReg oAdminReg, TokenModel oTokenModel);
        Task<ResultModel<object>> UpdateProfileImage(TokenModel oTokenModel, FileUpload oFileUpload);

    }
}
