using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
    public interface IDepartment
    {
        Task<ResultModel<object>> Get(TokenModel oTokenModel, long Id);
        Task<ResultModel<object>> GetAll(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> Insert(TokenModel oTokenModel, DepartmentReg oDepartmentReg);
        Task<ResultModel<object>> Update(TokenModel oTokenModel, DepartmentReg oDepartmentReg);
        Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id);
        Task<ResultModel<MasterDepartmentSlotTime>> GetSlots(TokenModel oTokenModel, DepartmentSlotRequest oDepartmentSlotRequest);
        Task<ResultModel<object>> InsertSlots(TokenModel oTokenModel,  List<MasterDepartmentSlotTime> oMasterDepartmentSlotTime);
    }
}
