using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubix.BAL.Interfaces;
using Cubix.Filters;
using Cubix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using static Cubix.Controllers.BaseController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers
{

    /// <summary>
    /// This API Controller  used for fetch the master tables data , common for All Users
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    // [UserValidationResourceFilter]
    public class CommonController : BaseController
    {
        private readonly ICommon _srv;
        private readonly IMemoryCache _cache;
        MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30), // cache will expire in 300 seconds or 5 minutes
            SlidingExpiration = TimeSpan.FromMinutes(5) // cache will expire if inactive for 60 seconds
        };

        /// <summary>
        ///Comstructor
        /// </summary>
        public CommonController(ICommon user, IMemoryCache cache)
        {
            _srv = user;
            _cache = cache;
        }


        /// <summary>
        /// This method used for Get the States
        /// Store them in memory because this is static data
        /// </summary>
        /// <returns>MasterState Model</returns>
        [HttpGet("getstates")]
        public ResultModel<MasterState> GetState()
        {

            ResultModel<MasterState> oResultModel = new ResultModel<MasterState>();
            if (!_cache.TryGetValue("getstates", out List<MasterState> states))
            {
                states = _srv.GetStates().Result.LstModel;
                if (states != null)
                    _cache.Set("getstates", states, options);
            }

            if (states != null)
            {
                oResultModel.LstModel = states;
            }

            return oResultModel;
        }

        /// <summary>
        /// This method used for Get the Districts by Passing State Id
        /// Store them in memory because this is static data
        /// </summary>
        /// <returns>MasterDistrict Model</returns>
        [HttpGet("getdistricts/{Id}")]
        public ResultModel<MasterDistrict> GetDistricts(long Id)
        {
            ResultModel<MasterDistrict> oResultModel = new ResultModel<MasterDistrict>();
            if (!_cache.TryGetValue("getdistricts", out List<MasterDistrict> districts))
            {
                districts = _srv.GetDistricts().Result.LstModel;
                if (districts != null)
                    _cache.Set("getdistricts", districts, options);
            }

            if (districts != null)
            {
                oResultModel.LstModel = districts.Where(s => s.StateId == Id).ToList();
            }

            return oResultModel;
        }

        /// <summary>
        /// This method used for Get the Cities by Passing District Id
        /// Store them in memory because this is static data
        /// </summary>
        /// <returns>MasterCity Model</returns>
        [HttpGet("getcity/{Id}")]
        public ResultModel<MasterCity> GetCity(long Id)
        {
            ResultModel<MasterCity> oResultModel = new ResultModel<MasterCity>();
            if (!_cache.TryGetValue("getcity", out List<MasterCity> cities))
            {
                cities = _srv.GetCities().Result.LstModel;
                if (cities != null)
                    _cache.Set("getcity", cities, options);
            }

            if (cities != null)
            {
                oResultModel.LstModel = cities.Where(s => s.DistrictId == Id).ToList();
            }

            return oResultModel;
        }


        /// <summary>
        /// This method used for Get the Marital Status
        /// Store them in memory because this is static data
        /// <returns>MasterMaritalData Model</returns>
        /// </summary>

        [HttpGet("getmaritalstatus")]
        public ResultModel<MasterMaritalData> GetMasterMaritalStatus()
        {
            ResultModel<MasterMaritalData> oResultModel = new ResultModel<MasterMaritalData>();
            if (!_cache.TryGetValue("getmaritalstatus", out List<MasterMaritalData> states))
            {
                states = _srv.GetMasterMaritalStatus().Result.LstModel;
                if (states != null)
                    _cache.Set("getmaritalstatus", states, options);
            }

            if (states != null)
            {
                oResultModel.LstModel = states;
            }

            return oResultModel;
        }



        /// <summary>
        /// This method used for Get the Active Hospitals
        /// </summary>
        [ServiceFilter(typeof(AdminTokenFilter))]
        [HttpGet("getactivehospitals")]
        public async Task<ResultModel<object>> GetActiveHospitals()
        {

            return await _srv.GetActiveHospitals(Me);
        }


        /// <summary>
        /// This method used for Get the Active Branch By Passing Hospital Id
        /// </summary>

        [HttpGet("getactivebranchbyhospital/{Id}")]
        public async Task<ResultModel<object>> GetActiveBranchByHospital(long Id)
        {
            return await _srv.GetActiveBranchByHospital(Id);
        }


        /// <summary>
        /// This method used for Get the Active Department By Passing BranchId Id
        /// </summary>

        [HttpGet("getactivedepartmentbybranch/{Id}")]
        public async Task<ResultModel<object>> GetActiveDepartmentByBranch(long Id)
        {
            return await _srv.GetActiveDepartmentByBranch(Id);
        }


        /// <summary>
        /// This method used for Get the Active Doctor By Passing Department Id
        /// </summary>

        [HttpGet("getactivedoctorbydepartment/{Id}")]
        public async Task<ResultModel<object>> GetActiveDoctorByDepartment(long Id)
        {
            return await _srv.GetActiveDoctorByDepartment(Id);
        }


        /// <summary>
        /// This method used for Get the Active Speciality
        /// </summary>

        [HttpGet("getactivespeciality")]
        public async Task<ResultModel<object>> GetActiveSpeciality()
        {
            return await _srv.GetActiveSpeciality(Convert.ToInt64(Me.LoginId));
        }

        /// <summary>
        /// This method used for Get the Active Speciality
        /// </summary>

        [HttpGet("getactivespecialitybydoctor")]
        public async Task<ResultModel<object>> GetActiveSpecialityByDoctor()
        {
            return await _srv.GetActiveSpeciality(Convert.ToInt64(Me.CreatedById));
        }

        /// <summary>
        /// This method used for Get the master days
        /// </summary>

        [HttpGet("getmasterdays")]
        public async Task<ResultModel<object>> GetAMasterDays()
        {
            return await _srv.GetMasterDays();
        }


        /// <summary>
        /// This method used for Get the master drug
        /// </summary>

        [HttpGet("getmasterdrug")]
        public async Task<ResultModel<object>> GetDrugMaster()
        {
            return await _srv.GetDrugMaster(Me);
        }

        /// <summary>
        /// This method used for Get the master companny
        /// </summary>

        [HttpGet("getactivecompany")]
        public async Task<ResultModel<object>> GetActiveCompanyMaster()
        {
            return await _srv.GetActiveCompanyMaster(Me);
        }


        /// <summary>
        /// This method used for Get the Drug Type
        /// </summary>

        [HttpGet("getactivedrugtype")]
        public async Task<ResultModel<object>> GetActiveDrugType()
        {
            return await _srv.GetActiveDrugType(Me);
        }
    }
}
