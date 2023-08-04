using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Filters
{
    /// <summary>
    /// Implement the Middleware for check the Authorised Token
    /// </summary>
    public class MRTokenFilter : IActionFilter
    {

        public IDistributedCache _distributedCache { get; set; }

        ///// <summary>
        ///// Constructor
        ///// </summary>
        public MRTokenFilter(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

        }
        /// <summary>
        /// Resource Executing
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                // Get token from header and store in variable
                string token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                //Read JWT token
                JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                if (jwt == null)// If no token provided in Header
                {
                    ErrorModel oResultModel = new ErrorModel(Constants.INSUFFIECIENT_PRIVILEDGE, Constants.INSUFFIECIENT_PRIVILEDGE_MESSAGE);
                    context.Result = new JsonResult(oResultModel);
                }
                // Get Login Id from Token
                string LoginId = jwt.Claims.FirstOrDefault(c => c.Type == "LoginId").Value;

                //Get Token Details from Memory
                TokenCacheModel oTokenCacheModel = Cache.GetToken(_distributedCache, Constants.TOKEN_PREFIX_MR_USER + LoginId);
                if (oTokenCacheModel == null)// If Token Expired or not found in memory
                {
                    ErrorModel oResultModel = new ErrorModel(Constants.SESSION_EXPIRED, Constants.SESSION_EXPIRED_MESSAGE);
                    context.Result = new JsonResult(oResultModel);
                }
                else if (oTokenCacheModel.UserTypeId != Constants.MR_USER || oTokenCacheModel.Token != token)// If token not matched with current User
                {
                    ErrorModel oResultModel = new ErrorModel(Constants.INSUFFIECIENT_PRIVILEDGE, Constants.INSUFFIECIENT_PRIVILEDGE_MESSAGE);
                    context.Result = new JsonResult(oResultModel);
                }
            }
            else
            {
                ErrorModel oResultModel = new ErrorModel(Constants.INSUFFIECIENT_PRIVILEDGE, Constants.INSUFFIECIENT_PRIVILEDGE_MESSAGE);
                context.Result = new JsonResult(oResultModel);
            }


        }

        /// <summary>
        /// called after Resource Executed
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
