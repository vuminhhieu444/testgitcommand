using System;
using Library.Common.Response;
using Library.Common.Message;
using Library.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Library.Common.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Library.Cms.BusinessLogicLayer;
using Library.Cms.DataModel;

namespace Digitizing.Api.Cms.Controllers
{
    [Route("api/home")]
    public class HomeController : BaseController
    {
        private IWebHostEnvironment _env;
        private IHomeBusiness _homeBusiness;
        public HomeController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IHomeBusiness homeBusiness) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _homeBusiness = homeBusiness;
        }

        
        [Route("get-home")]
        [HttpGet]
        public ResponseMessage<HomemModel> GetHome()
        {
            var response = new ResponseMessage<HomemModel>();
            try
            {
                response.Data = _homeBusiness.GetHome(CurrentLanguage);
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
         
    }
}