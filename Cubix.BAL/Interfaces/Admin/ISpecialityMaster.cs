using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
   public  interface ISpecialityMaster
    {
        Task<ResultModel<object>> Get(TokenModel oTokenModel, long Id);
        Task<ResultModel<object>> GetAll(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> Insert(TokenModel oTokenModel, MasterSpecialityData oSpecialityReg);
        Task<ResultModel<object>> Update(TokenModel oTokenModel, MasterSpecialityData oSpecialityReg);
        Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id);
    }
}
