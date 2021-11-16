using System;
using Library.Common.Response;
using Library.Common.Message;
using Library.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Library.Cms.DataModel;
using Library.Cms.BusinessLogicLayer;
using Microsoft.AspNetCore.Hosting;
using Library.Common.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Digitizing.Api.Cms.Controllers
{
    [Route("api/website-slide")]

    public class WebsiteSlideController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteSlideBusiness _websiteslideBUS;
        public WebsiteSlideController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteSlideBusiness websiteslideBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websiteslideBUS = websiteslideBUS;
        }
        [Route("create-website-slide")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteSlideModel>> CreateWebsiteSlide([FromBody] WebsiteSlideModel model)
        {
            var response = new ResponseMessage<WebsiteSlideModel>();
            try
            {
                if (model.slide_image != null) { var arrData = model.slide_image.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_slide\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.slide_image = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.created_by_user_id = CurrentUserId;


                var resultBUS = await Task.FromResult(_websiteslideBUS.Create(model));
                if (resultBUS)
                {
                    response.Data = model;
                    response.MessageCode = MessageCodes.CreateSuccessfully;


                }
                else
                {
                    response.MessageCode = MessageCodes.CreateFail;
                }

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("update-website-slide")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteSlideModel>> UpdateWebsiteSlide([FromBody] WebsiteSlideModel model)
        {
            var response = new ResponseMessage<WebsiteSlideModel>();
            try
            {
                if (model.slide_image != null) { var arrData = model.slide_image.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_slide\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.slide_image = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.lu_user_id = CurrentUserId;

                var resultBUS = await Task.FromResult(_websiteslideBUS.Update(model));
                if (resultBUS)
                {
                    response.Data = model;
                    response.MessageCode = MessageCodes.UpdateSuccessfully;


                }
                else
                {
                    response.MessageCode = MessageCodes.UpdateFail;
                }
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<WebsiteSlideModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<WebsiteSlideModel>>();
            try
            {

                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var slide_title = formData.Keys.Contains("slide_title") ? Convert.ToString(formData["slide_title"]) : "";

                long total = 0;
                var data = await Task.FromResult(_websiteslideBUS.Search(page, pageSize, CurrentLanguage, out total, slide_title));
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("get-by-id/{id}")]

        [HttpGet]
        public async Task<ResponseMessage<WebsiteSlideModel>> GetById(Guid? id)
        {
            var response = new ResponseMessage<WebsiteSlideModel>();
            try
            {
                response.Data = await Task.FromResult(_websiteslideBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-website-slide")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteSlide([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { slide_id = ds }).ToList());
                var listItem = await Task.FromResult(_websiteslideBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;


            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }


    }
}