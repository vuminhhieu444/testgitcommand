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
    [Route("api/website-product")]

    public class WebsiteProductController : BaseController
    {
        private IWebHostEnvironment _env;
        private IWebsiteProductBusiness _websiteproductBUS;
        public WebsiteProductController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IWebsiteProductBusiness websiteproductBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _websiteproductBUS = websiteproductBUS;
        }
        [Route("create-website-product")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteProductModel>> CreateWebsiteProduct([FromBody] WebsiteProductModel model)
        {
            var response = new ResponseMessage<WebsiteProductModel>();
            try
            {
                if (model.product_image != null) { var arrData = model.product_image.Split(';');
                    if (arrData.Length == 3) { var savePath = $@"upload\website_product\{Guid.NewGuid().ToString().Replace("-","")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.product_image = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.created_by_user_id = CurrentUserId;


                var resultBUS = await Task.FromResult(_websiteproductBUS.Create(model));
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
        [Route("update-website-product")]
        [HttpPost]
        public async Task<ResponseMessage<WebsiteProductModel>> UpdateWebsiteProduct([FromBody] WebsiteProductModel model)
        {
            var response = new ResponseMessage<WebsiteProductModel>();
            try
            {
                if (model.product_image != null) { var arrData = model.product_image.Split(';'); 
                    if (arrData.Length == 3) { var savePath = $@"upload\website_product\{Guid.NewGuid().ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}"; model.product_image = $"{savePath};{arrData[1]}"; FileHelper.CopyFile(savePath, arrData[2]); } }
                model.lu_user_id = CurrentUserId;

                var resultBUS = await Task.FromResult(_websiteproductBUS.Update(model));
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
        public async Task<ResponseListMessage<List<WebsiteProductModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<WebsiteProductModel>>();
            try
            {

                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var product_name = formData.Keys.Contains("product_name") ? Convert.ToString(formData["product_name"]) : "";

                long total = 0;
                var data = await Task.FromResult(_websiteproductBUS.Search(page, pageSize, CurrentLanguage, out total, product_name));
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
        public async Task<ResponseMessage<WebsiteProductModel>> GetById(Guid? id)
        {
            var response = new ResponseMessage<WebsiteProductModel>();
            try
            {
                response.Data = await Task.FromResult(_websiteproductBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-website-product")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteProduct([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { product_id = ds }).ToList());
                var listItem = await Task.FromResult(_websiteproductBUS.Delete(json_list_id, CurrentUserId));
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