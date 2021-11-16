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
    [Route("api/website-partner")]

    public class WebsitePartnerController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsitePartnerBusiness _websitepartnerBUS;
        public WebsitePartnerController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsitePartnerBusiness websitepartnerBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websitepartnerBUS = websitepartnerBUS;
        }
        [Route("create-website-partner")]
        [HttpPost]
        public async Task<ResponseMessage<WebsitePartnerModel>> CreateWebsitePartner([FromBody] WebsitePartnerModel model)
        {
            var response = new ResponseMessage<WebsitePartnerModel>();
            try
            {
                if (model.partner_logo != null) { var arrData = model.partner_logo.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_partner\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.partner_logo = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.created_by_user_id = CurrentUserId;


                var resultBUS = await Task.FromResult(_websitepartnerBUS.Create(model));
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
        [Route("update-website-partner")]
        [HttpPost]
        public async Task<ResponseMessage<WebsitePartnerModel>> UpdateWebsitePartner([FromBody] WebsitePartnerModel model)
        {
            var response = new ResponseMessage<WebsitePartnerModel>();
            try
            {
                if (model.partner_logo != null) { var arrData = model.partner_logo.Split(';'); if (arrData.Length == 3) { var savePath = $@"upload\website_partner\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.partner_logo = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.lu_user_id = CurrentUserId;

                var resultBUS = await Task.FromResult(_websitepartnerBUS.Update(model));
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
        public async Task<ResponseListMessage<List<WebsitePartnerModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<WebsitePartnerModel>>();
            try
            {

                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var partner_name = formData.Keys.Contains("partner_name") ? Convert.ToString(formData["partner_name"]) : "";

                long total = 0;
                var data = await Task.FromResult(_websitepartnerBUS.Search(page, pageSize, CurrentLanguage, out total, partner_name));
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
        public async Task<ResponseMessage<WebsitePartnerModel>> GetById(Guid? id)
        {
            var response = new ResponseMessage<WebsitePartnerModel>();
            try
            {
                response.Data = await Task.FromResult(_websitepartnerBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-website-partner")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsitePartner([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { partner_id = ds }).ToList());
                var listItem = await Task.FromResult(_websitepartnerBUS.Delete(json_list_id, CurrentUserId));
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