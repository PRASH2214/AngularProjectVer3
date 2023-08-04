using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers
{

    /// <summary>
    /// Base Controller used for Get the current token values from context header 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {

        /// <summary>
        /// This method used to upload the documents after converting from base64  to server and return the path
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public ResultModel<object> FileUpload(string FolderId, string ImagePath, string FileName, string FileFlag, string FolderName, IWebHostEnvironment _hostingEnvironment)
        {
            Random oRandom = new Random();
            string webRootPath = _hostingEnvironment.WebRootPath;//Get Root Path
            string filepath = AppSetting.DocPath + FolderName + "/" + FolderId;
            webRootPath = webRootPath + "/" + filepath;

            ResultModel<object> res = Helper.CheckValidFileUplaod(ImagePath, FileName, FileFlag);
            if (res.Status > Constants.SUCCESS)// If file validation failed
            {
                res.Message = res.Message;
                res.Status = res.Status;
                return res;
            }
            if (!Directory.Exists(webRootPath))
                Directory.CreateDirectory(webRootPath);

            string file = Path.GetFileNameWithoutExtension(FileName);
            FileName = FileName.Replace(file, oRandom.Next(0, 99999).ToString());

            System.IO.File.WriteAllBytes(webRootPath + "/" + FileName, Convert.FromBase64String(ImagePath));
            res.Message = filepath + "/" + FileName;
            return res;

        }

        /// <summary>
        /// Return the Current User Data from Reading the JWT Token
        /// </summary>
        public TokenModel Me
        {
            get
            {
                try
                {
                    TokenModel oRequestMembersModel = new TokenModel();
                    JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
                    oRequestMembersModel.MobileNumber = jwt.Claims.FirstOrDefault(c => c.Type == "MobileNumber").Value;
                    oRequestMembersModel.LoginId = jwt.Claims.FirstOrDefault(c => c.Type == "LoginId").Value;
                    oRequestMembersModel.UserTypeId = jwt.Claims.FirstOrDefault(c => c.Type == "UserTypeId").Value;
                    oRequestMembersModel.CreatedById = jwt.Claims.FirstOrDefault(c => c.Type == "CreatedById").Value;
                    oRequestMembersModel.DepartmentId = jwt.Claims.FirstOrDefault(c => c.Type == "DepartmentId").Value;
                    
                    return oRequestMembersModel;
                }
                catch (Exception)
                {
                    return new TokenModel();

                }
                //  }
            }



        }

    }
}
