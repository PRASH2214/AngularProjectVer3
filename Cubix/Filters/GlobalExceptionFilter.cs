using Cubix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cubix.Filters
{

    /// <summary>
    /// This Class used for check the Global Exception
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        //public HttpGlobalExceptionFilter()
        //{
        //}

        /// <summary>
        /// This Method used for customize the global exception message
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            //  var exception = context.Exception;
            //    var code = HttpStatusCode.InternalServerError;
            ErrorModel oResultModel = new ErrorModel(Constants.EXCEPTION, Constants.EXCEPTION_MESSAGE);


            //var ajaxResponse = new AjaxResponse
            //{
            //    IsSuccess = false
            //};

            //if (exception is ValidationException)
            //{
            //    //    code = HttpStatusCode.BadRequest;
            //    oResultModel.Message = "Bad request";
            //}
            //else
            //{
            //       oResultModel.Message = Constants.EXCEPTION_MESSAGE;
            //   }

            context.Result = new JsonResult(oResultModel);
            context.HttpContext.Response.StatusCode = (int)Constants.EXCEPTION;
            context.ExceptionHandled = true;
        }
    }
}
