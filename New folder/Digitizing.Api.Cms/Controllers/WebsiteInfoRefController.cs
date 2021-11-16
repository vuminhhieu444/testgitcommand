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
    [Route("api/website-info-ref")]

    public class WebsiteInfoRefController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteInfoRefBusiness _websiteinforefBUS;
        public WebsiteInfoRefController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteInfoRefBusiness websiteinforefBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websiteinforefBUS = websiteinforefBUS;
        }
        [Route("create-website-info-ref")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteInfoRefModel>> CreateWebsiteInfoRef([FromBody] WebsiteInfoRefModel model)
        {
            var response = new ResponseMessage<WebsiteInfoRefModel>();
            try
            {
                if (model.web_info_logo_l != null) { var arrData = model.web_info_logo_l.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_info_ref\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.web_info_logo_l = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                if (model.web_info_logo_e != null) { var arrData = model.web_info_logo_e.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_info_ref\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.web_info_logo_e = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.created_by_user_id = CurrentUserId;


                var resultBUS = await Task.FromResult(_websiteinforefBUS.Create(model));
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
        [Route("update-website-info-ref")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteInfoRefModel>> UpdateWebsiteInfoRef([FromBody] WebsiteInfoRefModel model)
        {
            var response = new ResponseMessage<WebsiteInfoRefModel>();
            try
            {
                if (model.web_info_logo_l != null) { var arrData = model.web_info_logo_l.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_info_ref\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.web_info_logo_l = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                if (model.web_info_logo_e != null) { var arrData = model.web_info_logo_e.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_info_ref\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.web_info_logo_e = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.lu_user_id = CurrentUserId;

                var resultBUS = await Task.FromResult(_websiteinforefBUS.Update(model));
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
        public async Task<ResponseListMessage<List<WebsiteInfoRefModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<WebsiteInfoRefModel>>();
            try
            {

                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var web_info_rcd = formData.Keys.Contains("web_info_rcd") ? Convert.ToString(formData["web_info_rcd"]) : "";
                var web_info_faculty = formData.Keys.Contains("web_info_faculty") ? Convert.ToString(formData["web_info_faculty"]) : "";

                long total = 0;
                var data = await Task.FromResult(_websiteinforefBUS.Search(page, pageSize, CurrentLanguage, out total, web_info_rcd, web_info_faculty));
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
        public async Task<ResponseMessage<WebsiteInfoRefModel>> GetById(string id)
        {
            var response = new ResponseMessage<WebsiteInfoRefModel>();
            try
            {
                response.Data = await Task.FromResult(_websiteinforefBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-website-info-ref")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteInfoRef([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { web_info_rcd = ds }).ToList());
                var listItem = await Task.FromResult(_websiteinforefBUS.Delete(json_list_id, CurrentUserId));
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