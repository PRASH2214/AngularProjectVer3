using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
    public interface ISlotTimeMaster
    {
        Task<ResultModel<object>> Get(TokenModel oTokenModel, long Id);
        Task<ResultModel<object>> GetAll(TokenModel oTokenModel);
        Task<ResultModel<object>> Insert(TokenModel oTokenModel, MasterSlots oSlotTime);
        Task<ResultModel<object>> Update(TokenModel oTokenModel, MasterSlots oSlotTime);
        Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id);
    }
}
