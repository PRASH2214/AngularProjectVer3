using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class ResultModel<T>
    {

        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
        public List<T> LstModel { get; set; }


        public ResultModel()
        {
            Success = true;
            Status = Constants.SUCCESS;
            Message = Constants.SUCCESS_MESSAGE;
        }
    }

    public class ErrorModel
    {

        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
      

        public ErrorModel(int ErrorCode,string ErrorMessage)
        {
            Success = false;
            Status = ErrorCode;
            Message = ErrorMessage;
        }
    }



    public class GridResultModel<T>
    {

        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
        public List<T> LstModel { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public GridResultModel()
        {
            Success = true;
            Status = 1;
            Message = "Success";
        }
    }



}
