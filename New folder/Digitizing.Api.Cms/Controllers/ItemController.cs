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
    [Route("api/item")]
    public class ItemController : BaseController
    {
        private IWebHostEnvironment _env;
        private IItemBusiness _itemBusiness;
        public ItemController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IItemBusiness itemBusiness) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _itemBusiness = itemBusiness;
        }

        [Route("create-item")]
        [HttpPost]
        public ResponseMessage<ItemModel> CreateItem([FromBody] ItemModel model)
        {
            var response = new ResponseMessage<ItemModel>();
            try
            {
                model.created_by_user_id = CurrentUserId;
                if (model.image_url != null)
                {
                    var arrData = model.image_url.Split(';');
                    if (arrData.Length == 3)
                    {
                        var savePath = $@"upload/news/image_title/{model.item_id.ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}";
                        model.image_url = $"{savePath};{arrData[1]}";
                        FileHelper.SaveFileFromBase64String(savePath, arrData[2]);
                    }
                }
                if (model.item_status_rcd == "PUP")
                {
                    model.published_by_user_id = CurrentUserId;
                    model.published_date_time = model.published_date_time == null ? DateTime.Now : model.published_date_time;
                }

                var resultBUS = _itemBusiness.Create(model);
                if (resultBUS)
                {
                    model.created_date_time = DateTime.Now;
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
        [Route("update-item")]
        [HttpPost]
        public ResponseMessage<ItemModel> UpdateItem([FromBody] ItemModel model)
        {
            var response = new ResponseMessage<ItemModel>();
            try
            {

                model.lu_user_id = CurrentUserId;
                if (model.image_url != null)
                {
                    var arrData = model.image_url.Split(';');
                    if (arrData.Length == 3)
                    {
                        var savePath = $@"upload/news/image_title/{model.item_id.ToString().Replace("-", "")}_{StringHelper.CorrectFilePath(arrData[0])}";
                        model.image_url = $"{savePath};{arrData[1]}";
                        FileHelper.SaveFileFromBase64String(savePath, arrData[2]);
                    }
                }
                if (model.item_status_rcd == "PUP")
                {
                    model.published_by_user_id = CurrentUserId;
                    model.published_date_time = model.published_date_time == null ? DateTime.Now : model.published_date_time;
                }
                var resultBUS = _itemBusiness.Update(model);
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
        [Route("update-item-down")]
        [HttpPost]
        public ResponseMessage<ItemModel> UpdateItemDown([FromBody] ItemModel model)
        {
            var response = new ResponseMessage<ItemModel>();
            try
            {
                var resultBUS = _itemBusiness.UpdateDown(model.item_id, CurrentUserId);
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

        [Route("update-item-undo")]
        [HttpPost]
        public ResponseMessage<ItemModel> UpdateItemUndo([FromBody] ItemModel model)
        {
            var response = new ResponseMessage<ItemModel>();
            try
            {
                var resultBUS = _itemBusiness.UpdateUndo(model.item_id, CurrentUserId);
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
        public ResponseListMessage<List<ItemModel>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<ItemModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var item_status_rcd = formData.Keys.Contains("item_status_rcd") ? Convert.ToString(formData["item_status_rcd"]) : "";
                var item_type_rcd = formData.Keys.Contains("item_type_rcd") ? Convert.ToString(formData["item_type_rcd"]) : "";
                var content_search = formData.Keys.Contains("content_search") ? Convert.ToString(formData["content_search"]) : "";
                Guid? item_group_id = null;
                if (formData.Keys.Contains("item_group_id") && !string.IsNullOrEmpty(Convert.ToString(formData["item_group_id"]))) { item_group_id = Guid.Parse(Convert.ToString(formData["item_group_id"])); }
                Guid? published_by_user_id = null;
                if (formData.Keys.Contains("published_by_user_id") && !string.IsNullOrEmpty(Convert.ToString(formData["published_by_user_id"]))) { published_by_user_id = Guid.Parse(Convert.ToString(formData["published_by_user_id"])); }
                Guid? user_id = null;
                if (formData.Keys.Contains("search_all") && !string.IsNullOrEmpty(Convert.ToString(formData["search_all"])) && Convert.ToString(formData["search_all"]) == "1")
                    user_id = null;
                else
                    user_id = CurrentUserId;

                DateTime? fr_published_date_time = null;
                if (formData.Keys.Contains("fr_published_date_time") && formData["fr_published_date_time"] != null && formData["fr_published_date_time"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["fr_published_date_time"].ToString());
                    fr_published_date_time = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                }
                DateTime? to_published_date_time = null;
                if (formData.Keys.Contains("to_published_date_time") && formData["to_published_date_time"] != null && formData["to_published_date_time"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["to_published_date_time"].ToString());
                    to_published_date_time = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
                }
                DateTime? fr_created_date_time = null;
                if (formData.Keys.Contains("fr_created_date_time") && formData["fr_created_date_time"] != null && formData["fr_created_date_time"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["fr_created_date_time"].ToString());
                    fr_created_date_time = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                }
                DateTime? to_created_date_time = null;
                if (formData.Keys.Contains("to_created_date_time") && formData["to_created_date_time"] != null && formData["to_created_date_time"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["to_created_date_time"].ToString());
                    to_created_date_time = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
                }
                long total = 0;
                var data = _itemBusiness.Search(page, pageSize, CurrentLanguage, out total, item_status_rcd, item_type_rcd, content_search, fr_published_date_time, to_published_date_time, published_by_user_id, fr_created_date_time, to_created_date_time, CurrentUserId, item_group_id, user_id);
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
        public ResponseMessage<ItemModel> GetById(Guid id)
        {
            var response = new ResponseMessage<ItemModel>();
            try
            {
                response.Data = _itemBusiness.GetById(id);
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-item")]
        [HttpPost]
        public ResponseListMessage<bool> DeleteItemp([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { item_id = ds }).ToList());
                var listItem = _itemBusiness.Delete(json_list_id, CurrentUserId);
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("remove-item")]
        [HttpPost]
        public ResponseListMessage<bool> RemoveItemp([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { item_id = ds }).ToList());
                var listItem = _itemBusiness.Remove(json_list_id);
                response.Data = true;
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