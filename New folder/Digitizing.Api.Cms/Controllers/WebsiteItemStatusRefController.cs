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
    [Route("api/website-item-status-ref")]

    public class WebsiteItemStatusRefController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteItemStatusRefBusiness _websiteitemstatusrefBUS;
        public WebsiteItemStatusRefController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteItemStatusRefBusiness websiteitemstatusrefBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websiteitemstatusrefBUS = websiteitemstatusrefBUS;
        }
        [Route("create-website-item-status-ref")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteItemStatusRefModel>> CreateWebsiteItemStatusRef([FromBody] WebsiteItemStatusRefModel model)
        {
            var response = new ResponseMessage<WebsiteItemStatusRefModel>();
            try
            {

                model.created_by_user_id = CurrentUserId;


                var resultBUS = await Task.FromResult(_websiteitemstatusrefBUS.Create(model));
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
        [Route("update-website-item-status-ref")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteItemStatusRefModel>> UpdateWebsiteItemStatusRef([FromBody] WebsiteItemStatusRefModel model)
        {
            var response = new ResponseMessage<WebsiteItemStatusRefModel>();
            try
            {

                model.lu_user_id = CurrentUserId;

                var resultBUS = await Task.FromResult(_websiteitemstatusrefBUS.Update(model));
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
        public async Task<ResponseListMessage<List<WebsiteItemStatusRefModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<WebsiteItemStatusRefModel>>();
            try
            {

                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var item_status_name = formData.Keys.Contains("item_status_name") ? Convert.ToString(formData["item_status_name"]) : "";
                long total = 0;
                var data = await Task.FromResult(_websiteitemstatusrefBUS.Search(page, pageSize, CurrentLanguage, out total, item_status_name));
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
        public async Task<ResponseMessage<WebsiteItemStatusRefModel>> GetById(string id)
        {
            var response = new ResponseMessage<WebsiteItemStatusRefModel>();
            try
            {
                response.Data = await Task.FromResult(_websiteitemstatusrefBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-website-item-status-ref")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteItemStatusRef([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { item_status_rcd = ds }).ToList());
                var listItem = await Task.FromResult(_websiteitemstatusrefBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("get-dropdown")]

        [HttpGet]
        public async Task<ResponseMessage<IList<DropdownOptionModel>>> GetListDropdown()
        {
            var response = new ResponseMessage<IList<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_websiteitemstatusrefBUS.GetListDropdown(CurrentLanguage));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }


    }
}