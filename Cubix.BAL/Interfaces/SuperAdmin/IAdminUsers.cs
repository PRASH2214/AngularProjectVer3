using Cubix.Models;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces.SuperAdmin
{
    public interface IAdminUsers
    {
        Task<ResultModel<object>> Get(TokenModel oTokenModel, long Id);
        Task<ResultModel<object>> GetAll(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> Insert(TokenModel oTokenModel, AdminReg oAdminReg);
        Task<ResultModel<object>> Update(TokenModel oTokenModel, AdminReg oAdminReg);
        Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id);
    }
}
