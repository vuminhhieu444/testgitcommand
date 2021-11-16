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
    [Route("api/website-image")]

    public class WebsiteImageController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteImageBusiness _websiteimageBUS;
        public WebsiteImageController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteImageBusiness websiteimageBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websiteimageBUS = websiteimageBUS;
        }
        [Route("create-website-image")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteImageModel>> CreateWebsiteImage([FromBody] WebsiteImageModel model)
        {
            var response = new ResponseMessage<WebsiteImageModel>();
            try
            {
                if (model.image_src != null)
                {
                    var arrData = model.image_src.Split(';');
                    if (arrData.Length == 3) { var savePath = $@"upload\website_image\{Guid.NewGuid().ToString().Replace("-","")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.image_src = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); }
                }
                model.created_by_user_id = CurrentUserId;


                var resultBUS = await Task.FromResult(_websiteimageBUS.Create(model));
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
        [Route("update-website-image")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteImageModel>> UpdateWebsiteImage([FromBody] WebsiteImageModel model)
        {
            var response = new ResponseMessage<WebsiteImageModel>();
            try
            {
                if (model.image_src != null) { var arrData = model.image_src.Split(';'); 
                    if (arrData.Length == 3) { var savePath = $@"upload\website_image\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.image_src = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.lu_user_id = CurrentUserId;

                var resultBUS = await Task.FromResult(_websiteimageBUS.Update(model));
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
        public async Task<ResponseListMessage<List<WebsiteImageModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<WebsiteImageModel>>();
            try
            {

                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var image_name = formData.Keys.Contains("image_name") ? Convert.ToString(formData["image_name"]) : "";

                long total = 0;
                var data = await Task.FromResult(_websiteimageBUS.Search(page, pageSize, CurrentLanguage, out total, image_name));
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
        public async Task<ResponseMessage<WebsiteImageModel>> GetById(Guid? id)
        {
            var response = new ResponseMessage<WebsiteImageModel>();
            try
            {
                response.Data = await Task.FromResult(_websiteimageBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-website-image")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteImage([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { image_id = ds }).ToList());
                var listItem = await Task.FromResult(_websiteimageBUS.Delete(json_list_id, CurrentUserId));
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