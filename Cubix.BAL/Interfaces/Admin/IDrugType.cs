﻿using Cubix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cubix.BAL.Interfaces
{
    public interface IDrugType
    {
        Task<ResultModel<object>> Get(TokenModel oTokenModel, long Id);
        Task<ResultModel<object>> GetAll(TokenModel oTokenModel, SearchModel oSearchModel);
        Task<ResultModel<object>> Insert(TokenModel oTokenModel, DrugType oDrugType);
        Task<ResultModel<object>> Update(TokenModel oTokenModel, DrugType oDrugType);
        Task<ResultModel<object>> Delete(TokenModel oTokenModel, long Id);
    }
}
